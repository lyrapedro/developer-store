namespace Ambev. DeveloperEvaluation.Application.Customers;

/// <summary>
/// Represents the response returned after successfully creating or retrieve a customer.
/// </summary>
public class CustomerResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the customer.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the customer's full name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the customer's email address.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the customer's phone number.
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the customer's document number. 
    /// </summary>
    public string Document { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the customer's address.
    /// </summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the customer's city. 
    /// </summary>
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the customer's state. 
    /// </summary>
    public string State { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the customer's ZIP code. 
    /// </summary>
    public string ZipCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets whether the customer is active.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the customer was created. 
    /// </summary>
    public DateTime CreatedAt { get; set; }
}