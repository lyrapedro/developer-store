namespace Ambev.DeveloperEvaluation.Application.Branches;

/// <summary>
/// Response model for branch operations.
/// </summary>
public class BranchResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the created branch.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the branch's name. 
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch's code.
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch's address.
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

    /// <summary>
    /// Gets or sets whether the branch is active.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the branch was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }
}