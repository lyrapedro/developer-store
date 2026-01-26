namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleItemRequest
{
    /// <summary>
    /// Gets or sets the product ID for this item.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of this product. 
    /// </summary>
    public int Quantity { get; set; }
}