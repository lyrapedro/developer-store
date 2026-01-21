using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a customer in the sales domain.
/// This is separate from User - a Customer is who BUYS products, while a User is who LOGS IN to the system.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class Customer : BaseEntity
{
    /// <summary>
    /// Gets or sets the customer's full name.
    /// Must not be null or empty.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the customer's email address.
    /// Must be a valid email format and is used for communication and invoicing.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the customer's phone number.
    /// Must be a valid phone number format.
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the customer's document number (CPF for individuals or CNPJ for companies).
    /// Must be unique in the system and follow the appropriate format.
    /// </summary>
    public string Document { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the customer's street address.
    /// </summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the customer's city. 
    /// </summary>
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the customer's state or province.
    /// </summary>
    public string State { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the customer's postal/ZIP code.
    /// </summary>
    public string ZipCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets whether the customer is active in the system. 
    /// Inactive customers cannot be used in new sales. 
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Gets or sets the optional reference to a User account.
    /// If populated, links this customer to a user who can log in to the system.
    /// This is optional - customers can exist without user accounts.
    /// </summary>
    public Guid?  UserId { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the customer was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time of the last update to the customer's information.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Initializes a new instance of the Customer class.
    /// Sets the creation timestamp and activates the customer by default.
    /// </summary>
    public Customer()
    {
        CreatedAt = DateTime.UtcNow;
        IsActive = true;
    }

    /// <summary>
    /// Updates the customer's information.
    /// </summary>
    /// <param name="name">The customer's full name</param>
    /// <param name="email">The customer's email address</param>
    /// <param name="phone">The customer's phone number</param>
    /// <param name="document">The customer's document (CPF/CNPJ)</param>
    /// <param name="address">The customer's street address</param>
    /// <param name="city">The customer's city</param>
    /// <param name="state">The customer's state</param>
    /// <param name="zipCode">The customer's ZIP code</param>
    public void Update(
        string name,
        string email,
        string phone,
        string document,
        string address,
        string city,
        string state,
        string zipCode)
    {
        Name = name;
        Email = email;
        Phone = phone;
        Document = document;
        Address = address;
        City = city;
        State = state;
        ZipCode = zipCode;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Activates the customer account. 
    /// Active customers can be used in new sales. 
    /// </summary>
    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Deactivates the customer account. 
    /// Inactive customers cannot be used in new sales but maintain historical data.
    /// </summary>
    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }
}