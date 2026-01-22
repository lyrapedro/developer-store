using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Validator for CreateSaleCommand that defines validation rules for sale creation command.
/// </summary>
public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateSaleCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - CustomerId: Must not be empty
    /// - BranchId: Must not be empty
    /// - Items: Must contain at least one item
    /// - Each item must have valid ProductId, Quantity, and UnitPrice
    /// </remarks>
    public CreateSaleCommandValidator()
    {
        RuleFor(sale => sale.CustomerId)
            .NotEmpty()
            .WithMessage("Customer ID is required");

        RuleFor(sale => sale.BranchId)
            .NotEmpty()
            .WithMessage("Branch ID is required");

        RuleFor(sale => sale.Items)
            .NotEmpty()
            .WithMessage("Sale must contain at least one item");

        RuleForEach(sale => sale.Items)
            .SetValidator(new CreateSaleItemValidator());
    }
}

/// <summary>
/// Validator for sale items within a CreateSaleCommand.
/// </summary>
public class CreateSaleItemValidator : AbstractValidator<CreateSaleItemCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateSaleItemValidator with defined validation rules. 
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - ProductId: Must not be empty
    /// - Quantity: Must be greater than 0
    /// - UnitPrice: Must be greater than or equal to 0
    /// - Discount: Must be greater than or equal to 0
    /// </remarks>
    public CreateSaleItemValidator()
    {
        RuleFor(item => item.ProductId)
            .NotEmpty()
            .WithMessage("Product ID is required");

        RuleFor(item => item. Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than 0");

        RuleFor(item => item. UnitPrice)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Unit price must be greater than or equal to 0");

        RuleFor(item => item. Discount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Discount must be greater than or equal to 0");
    }
}