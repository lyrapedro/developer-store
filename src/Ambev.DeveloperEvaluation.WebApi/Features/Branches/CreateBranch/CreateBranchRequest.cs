namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.CreateBranch;

public class CreateBranchRequest
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
}