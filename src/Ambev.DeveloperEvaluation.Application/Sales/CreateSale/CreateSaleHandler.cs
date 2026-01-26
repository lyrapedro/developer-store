using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Handler for processing CreateSaleCommand requests.
/// Implements External Identities pattern with denormalization.
/// </summary>
public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IBranchRepository _branchRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public CreateSaleHandler(
        ISaleRepository saleRepository,
        ICustomerRepository customerRepository,
        IBranchRepository branchRepository,
        IProductRepository productRepository,
        IMapper mapper)
    {
        _saleRepository = saleRepository;
        _customerRepository = customerRepository;
        _branchRepository = branchRepository;
        _productRepository = productRepository;
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
        
        var customer = await _customerRepository.GetByIdAsync(request.CustomerId, cancellationToken);
        if (customer == null)
            throw new KeyNotFoundException($"Customer with ID {request.CustomerId} not found");

        if (!customer.IsActive)
            throw new InvalidOperationException($"Customer {customer.Name} is not active");
        
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
            CustomerId = customer.Id,
            CustomerName = customer.Name,
            CustomerEmail = customer.Email,
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