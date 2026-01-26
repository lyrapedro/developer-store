using Ambev.DeveloperEvaluation.Domain.Events;
using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
/// Handler for processing CancelSaleCommand requests.
/// </summary>
public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, CancelSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of CancelSaleHandler.
    /// </summary>
    /// <param name="saleRepository">The sale repository.</param>
    /// <param name="productRepository">The product repository.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    public CancelSaleHandler(
        ISaleRepository saleRepository,
        IProductRepository productRepository,
        IMediator mediator,
        IMapper mapper)
    {
        _saleRepository = saleRepository;
        _productRepository = productRepository;
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the CancelSaleCommand request.
    /// Returns stock to inventory when cancelling.
    /// </summary>
    /// <param name="request">The CancelSale command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The cancelled sale details.</returns>
    public async Task<CancelSaleResult> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
    {
        var validator = new CancelSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (sale == null)
            throw new KeyNotFoundException($"Sale with ID {request.Id} not found");

        if (sale.IsCancelled)
            throw new InvalidOperationException("Sale is already cancelled");
        
        foreach (var item in sale.Items)
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId, cancellationToken);
            if (product != null)
            {
                product.AddStock(item.Quantity);
                await _productRepository.UpdateAsync(product, cancellationToken);
            }
        }
        
        sale.Cancel();
        var cancelledSale = await _saleRepository.UpdateAsync(sale, cancellationToken);
        
        var saleCancelledEvent = new SaleCancelledEvent(
            saleId: cancelledSale.Id,
            saleNumber: cancelledSale.SaleNumber,
            userId: cancelledSale.UserId,
            userName: cancelledSale.UserName,
            branchId: cancelledSale.BranchId,
            branchName: cancelledSale.BranchName,
            totalAmount: cancelledSale.TotalAmount,
            cancelledAt: cancelledSale.CancelledAt ?? DateTime.UtcNow,
            originalSaleDate: cancelledSale.SaleDate,
            items: cancelledSale.Items.Select(i => new SaleCancelledEventItem
            {
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                Quantity = i.Quantity,
                TotalAmount = i.TotalAmount
            }).ToList(),
            cancelledBy: null // Could be populated from current user context
        );

        await _mediator.Publish(saleCancelledEvent, cancellationToken);

        return new CancelSaleResult
        {
            Id = cancelledSale.Id,
            SaleNumber = cancelledSale.SaleNumber,
            IsCancelled = cancelledSale.IsCancelled,
            CancelledAt = cancelledSale.CancelledAt
        };
    }
}