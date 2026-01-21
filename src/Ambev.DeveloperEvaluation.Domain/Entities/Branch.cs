using Ambev. DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a branch/store location in the system.
/// Branches are physical or virtual locations where sales can occur.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class Branch : BaseEntity
{
    /// <summary>
    /// Gets or sets the branch's name.
    /// Must not be null or empty.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch's unique code.
    /// Used for quick identification and must be unique across all branches.
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
    /// Gets or sets the branch's state or province.
    /// </summary>
    public string State { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch's postal/ZIP code.
    /// </summary>
    public string ZipCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch's contact phone number.
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets whether the branch is active in the system. 
    /// Inactive branches cannot be used in new sales.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the branch was created. 
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time of the last update to the branch's information.
    /// </summary>
    public DateTime?  UpdatedAt { get; set; }

    /// <summary>
    /// Initializes a new instance of the Branch class.
    /// Sets the creation timestamp and activates the branch by default.
    /// </summary>
    public Branch()
    {
        CreatedAt = DateTime.UtcNow;
        IsActive = true;
    }

    /// <summary>
    /// Updates the branch's information.
    /// </summary>
    /// <param name="name">The branch's name</param>
    /// <param name="code">The branch's unique code</param>
    /// <param name="address">The branch's street address</param>
    /// <param name="city">The branch's city</param>
    /// <param name="state">The branch's state</param>
    /// <param name="zipCode">The branch's ZIP code</param>
    /// <param name="phone">The branch's phone number</param>
    public void Update(
        string name,
        string code,
        string address,
        string city,
        string state,
        string zipCode,
        string phone)
    {
        Name = name;
        Code = code;
        Address = address;
        City = city;
        State = state;
        ZipCode = zipCode;
        Phone = phone;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Activates the branch. 
    /// Active branches can be used in new sales.
    /// </summary>
    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Deactivates the branch.
    /// Inactive branches cannot be used in new sales but maintain historical data.
    /// </summary>
    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }
}