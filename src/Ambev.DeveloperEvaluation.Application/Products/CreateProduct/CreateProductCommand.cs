using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Command for creating a new product.
/// </summary>
/// <remarks>
/// This command captures the required data for creating a product,
/// including name, SKU, pricing, and inventory information.
/// </remarks>
public class CreateProductCommand : IRequest<ProductResult>
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
    
    public ValidationResultDetail Validate()
    {
        var validator = new CreateProductCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}