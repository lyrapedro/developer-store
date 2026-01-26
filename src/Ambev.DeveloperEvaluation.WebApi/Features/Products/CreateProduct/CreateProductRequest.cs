namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

public class CreateProductRequest
{
    /// <summary>
    /// Gets or sets the product's name. 
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the product's SKU (Stock Keeping Unit).
    /// </summary>
    public string Sku { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the product's description.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the product's price. 
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the initial stock quantity.
    /// </summary>
    public int StockQuantity { get; set; }

    /// <summary>
    /// Gets or sets the product's category.
    /// </summary>
    public string Category { get; set; } = string.Empty;
}