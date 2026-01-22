using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
/// Command for cancelling a sale. 
/// </summary>
public class CancelSaleCommand : IRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the sale to cancel.
    /// </summary>
    public Guid Id { get; set; }
}