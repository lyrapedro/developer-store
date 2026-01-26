namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleRequest
{
    /// <summary>
    /// Gets or sets the customer ID for this sale.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the branch ID where the sale is being made.
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// Gets or sets the list of items in this sale.
    /// </summary>
    public List<CreateSaleItemRequest> Items { get; set; } = new();
}