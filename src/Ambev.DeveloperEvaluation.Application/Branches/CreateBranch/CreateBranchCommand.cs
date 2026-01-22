using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branches.CreateBranch;

/// <summary>
/// Command for creating a new branch.
/// </summary>
/// <remarks>
/// This command captures the required data for creating a branch,
/// including name, code, and location details.
/// /// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="BranchResult"/>.
/// 
/// The data provided in this command is validated using the 
/// <see cref="CreateBranchCommandValidator"/> which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
/// populated and follow the required rules.
/// </remarks>
public class CreateBranchCommand : IRequest<BranchResult>
{
    /// <summary>
    /// Gets or sets the branch's name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch's unique code.
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch's street address.
    /// </summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch's city.
    /// </summary>
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch's state. 
    /// </summary>
    public string State { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch's ZIP code. 
    /// </summary>
    public string ZipCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch's phone number.
    /// </summary>
    public string Phone { get; set; } = string.Empty;
    
    public ValidationResultDetail Validate()
    {
        var validator = new CreateBranchCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}