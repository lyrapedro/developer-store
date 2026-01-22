using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Branches.CreateBranch;

/// <summary>
/// Validator for CreateBranchCommand that defines validation rules for branch creation command.
/// </summary>
public class CreateBranchCommandValidator : AbstractValidator<CreateBranchCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateBranchCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Name: Required, must be between 3 and 200 characters
    /// - Code: Required, must be between 2 and 50 characters
    /// - Address: Optional, max 500 characters
    /// - City: Optional, max 100 characters
    /// - State: Optional, max 50 characters
    /// - ZipCode: Optional, max 10 characters
    /// - Phone: Must match phone format if provided
    /// </remarks>
    public CreateBranchCommandValidator()
    {
        RuleFor(branch => branch.Name)
            .NotEmpty()
            .Length(3, 200)
            .WithMessage("Branch name must be between 3 and 200 characters");

        RuleFor(branch => branch.Code)
            .NotEmpty()
            .Length(2, 50)
            .WithMessage("Branch code must be between 2 and 50 characters");

        RuleFor(branch => branch.Address)
            .MaximumLength(500)
            .When(branch => !string.IsNullOrEmpty(branch.Address))
            .WithMessage("Address cannot exceed 500 characters");

        RuleFor(branch => branch.City)
            .MaximumLength(100)
            .When(branch => !string.IsNullOrEmpty(branch.City))
            .WithMessage("City cannot exceed 100 characters");

        RuleFor(branch => branch.State)
            .MaximumLength(50)
            .When(branch => !string.IsNullOrEmpty(branch.State))
            .WithMessage("State cannot exceed 50 characters");

        RuleFor(branch => branch.ZipCode)
            .MaximumLength(10)
            .When(branch => !string.IsNullOrEmpty(branch.ZipCode))
            .WithMessage("ZipCode cannot exceed 10 characters");

        RuleFor(branch => branch.Phone)
            .Matches(@"^\+?[1-9]\d{1,14}$")
            .When(branch => !string.IsNullOrEmpty(branch.Phone))
            .WithMessage("Phone must be in valid international format");
    }
}