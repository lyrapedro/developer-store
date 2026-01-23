using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Customers.CreateCustomer;

/// <summary>
/// Validator for CreateCustomerCommand that defines validation rules for customer creation command.
/// </summary>
public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateCustomerCommandValidator with defined validation rules. 
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Name: Required, must be between 3 and 200 characters
    /// - Email:  Must be in valid format (using EmailValidator)
    /// - Phone: Must match phone format
    /// - Document: Required, must be between 11 and 20 characters (CPF/CNPJ)
    /// - Address: Optional, max 500 characters
    /// - City: Optional, max 100 characters
    /// - State: Optional, max 50 characters
    /// - ZipCode: Optional, max 10 characters
    /// </remarks>
    public CreateCustomerCommandValidator()
    {
        RuleFor(customer => customer.Name)
            .NotEmpty()
            .Length(3, 200)
            .WithMessage("Customer name must be between 3 and 200 characters");

        RuleFor(customer => customer.Email)
            .SetValidator(new EmailValidator());

        RuleFor(customer => customer.Phone)
            .Matches(@"^\+?[1-9]\d{1,14}$")
            .When(customer => !string.IsNullOrEmpty(customer.Phone))
            .WithMessage("Phone must be in valid international format");

        RuleFor(customer => customer.Document)
            .NotEmpty()
            .Length(11, 20)
            .WithMessage("Document must be between 11 and 20 characters");

        RuleFor(customer => customer.Address)
            .MaximumLength(500)
            .When(customer => !string.IsNullOrEmpty(customer.Address))
            .WithMessage("Address cannot exceed 500 characters");

        RuleFor(customer => customer.City)
            .MaximumLength(100)
            .When(customer => !string.IsNullOrEmpty(customer.City))
            .WithMessage("City cannot exceed 100 characters");

        RuleFor(customer => customer.State)
            .MaximumLength(50)
            .When(customer => !string.IsNullOrEmpty(customer.State))
            .WithMessage("State cannot exceed 50 characters");

        RuleFor(customer => customer.ZipCode)
            .MaximumLength(10)
            .When(customer => !string.IsNullOrEmpty(customer.ZipCode))
            .WithMessage("ZipCode cannot exceed 10 characters");
    }
}