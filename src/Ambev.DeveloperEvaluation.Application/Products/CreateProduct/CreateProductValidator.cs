using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Validator for CreateProductCommand that defines validation rules for product creation command.
/// </summary>
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateProductCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Name: Required, must be between 3 and 200 characters
    /// - Sku: Required, must be between 3 and 50 characters
    /// - Description: Optional, max 1000 characters
    /// - Price: Must be greater than or equal to 0
    /// - StockQuantity: Must be greater than or equal to 0
    /// - Category: Optional, max 100 characters
    /// </remarks>
    public CreateProductCommandValidator()
    {
        RuleFor(product => product.Name)
            .NotEmpty()
            .Length(3, 200)
            .WithMessage("Product name must be between 3 and 200 characters");

        RuleFor(product => product. Sku)
            .NotEmpty()
            .Length(3, 50)
            .WithMessage("SKU must be between 3 and 50 characters");

        RuleFor(product => product.Description)
            .MaximumLength(1000)
            .When(product => !string.IsNullOrEmpty(product.Description))
            .WithMessage("Description cannot exceed 1000 characters");

        RuleFor(product => product.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Price must be greater than or equal to 0");

        RuleFor(product => product.StockQuantity)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Stock quantity must be greater than or equal to 0");

        RuleFor(product => product.Category)
            .MaximumLength(100)
            .When(product => ! string.IsNullOrEmpty(product.Category))
            .WithMessage("Category cannot exceed 100 characters");
    }
}