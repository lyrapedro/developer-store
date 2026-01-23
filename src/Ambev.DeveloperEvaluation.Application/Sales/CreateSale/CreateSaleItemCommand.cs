using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Command for adding an item to a sale.
/// </summary>
public class CreateSaleItemCommand
{
    /// <summary>
    /// Gets or sets the product ID for this item.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of this product. 
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the unit price for this item.
    /// </summary>
    public decimal UnitPrice { get; set; }
}