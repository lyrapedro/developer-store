using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Handler for processing CreateSaleCommand requests.
/// Implements External Identities pattern with denormalization.
/// </summary>
public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IBranchRepository _branchRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CreateSaleHandler(
        ISaleRepository saleRepository,
        IUserRepository userRepository,
        IBranchRepository branchRepository,
        IProductRepository productRepository,
        IMediator mediator,
        IMapper mapper)
    {
        _saleRepository = saleRepository;
        _userRepository = userRepository;
        _branchRepository = branchRepository;
        _productRepository = productRepository;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var duplicateProducts = request.Items
            .GroupBy(i => i.ProductId)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();

        if (duplicateProducts.Count != 0)
        {
            throw new InvalidOperationException(
                $"Duplicate products found in sale: {string.Join(", ", duplicateProducts)}. " +
                "Each product should appear only once. Use the Quantity property to specify multiple units.");
        }
        
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user == null)
            throw new KeyNotFoundException($"User with ID {request.UserId} not found");
        
        var branch = await _branchRepository.GetByIdAsync(request.BranchId, cancellationToken);
        if (branch == null)
            throw new KeyNotFoundException($"Branch with ID {request.BranchId} not found");

        if (!branch.IsActive)
            throw new InvalidOperationException($"Branch {branch.Name} is not active");
        
        var saleNumber = await GenerateSaleNumberAsync(cancellationToken);
        
        var sale = new Sale
        {
            SaleNumber = saleNumber,
            SaleDate = DateTime.UtcNow,
            UserId = user.Id,
            UserName = user.Username,
            UserEmail = user.Email,
            BranchId = branch.Id,
            BranchName = branch.Name,
            BranchCode = branch.Code,
            IsCancelled = false,
            CreatedAt = DateTime.UtcNow
        };
        
        foreach (var itemCommand in request.Items)
        {
            var product = await _productRepository.GetByIdAsync(itemCommand.ProductId, cancellationToken);
            if (product == null)
                throw new KeyNotFoundException($"Product with ID {itemCommand.ProductId} not found");

            if (!product.IsActive)
                throw new InvalidOperationException($"Product {product.Name} is not active");
            
            if (!product.HasSufficientStock(itemCommand.Quantity))
                throw new InvalidOperationException(
                    $"Insufficient stock for product {product.Name}. Available: {product.StockQuantity}, Requested: {itemCommand.Quantity}");
            
            var saleItem = new SaleItem
            {
                SaleId = sale.Id,
                ProductId = product.Id,
                ProductName = product.Name,
                ProductSku = product.Sku,
                Quantity = itemCommand.Quantity,
                UnitPrice = product.Price
            };
            
            saleItem.ApplyAutomaticDiscount();

            sale.AddItem(saleItem);
            
            product.RemoveStock(itemCommand.Quantity);
            await _productRepository.UpdateAsync(product, cancellationToken);
        }
        
        var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);
        
        var saleCreatedEvent = new SaleCreatedEvent(
            saleId: createdSale.Id,
            saleNumber: createdSale.SaleNumber,
            userId: createdSale.UserId,
            userName: createdSale.UserName,
            userEmail: createdSale.UserEmail,
            branchId: createdSale.BranchId,
            branchName: createdSale.BranchName,
            totalAmount: createdSale.TotalAmount,
            itemCount: createdSale.Items.Count,
            createdAt: createdSale.CreatedAt,
            items: createdSale.Items.Select(i => new SaleCreatedEventItem
            {
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                ProductSku = i.ProductSku,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                Discount = i.Discount,
                TotalAmount = i.TotalAmount
            }).ToList()
        );

        await _mediator.Publish(saleCreatedEvent, cancellationToken);

        return _mapper.Map<CreateSaleResult>(createdSale);
    }

    private async Task<string> GenerateSaleNumberAsync(CancellationToken cancellationToken)
    {
        var today = DateTime.UtcNow;
        var prefix = $"SALE-{today:yyyyMMdd}";
        
        var todaySales = await _saleRepository.GetByDateRangeAsync(
            today.Date, 
            today.Date.AddDays(1).AddTicks(-1), 
            cancellationToken);

        var sequence = todaySales.Count + 1;
        return $"{prefix}-{sequence:D4}";
    }
}