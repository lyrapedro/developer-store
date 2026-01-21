using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a product in the inventory/catalog. 
/// Products can be sold and have their stock managed.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class Product : BaseEntity
{
    /// <summary>
    /// Gets or sets the product's name. 
    /// Must not be null or empty.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the product's SKU (Stock Keeping Unit).
    /// Must be unique across all products and is used for inventory tracking.
    /// </summary>
    public string Sku { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the product's detailed description.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the product's current price.
    /// Must be a positive value and represents the base selling price.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the current stock quantity available. 
    /// Must not be negative.  Zero indicates out of stock.
    /// </summary>
    public int StockQuantity { get; set; }

    /// <summary>
    /// Gets or sets the product's category for classification.
    /// Used for organizing and filtering products. 
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets whether the product is active in the system.
    /// Inactive products cannot be sold but maintain historical data.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the product was created. 
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time of the last update to the product's information.
    /// </summary>
    public DateTime?  UpdatedAt { get; set; }

    /// <summary>
    /// Initializes a new instance of the Product class.
    /// Sets the creation timestamp and activates the product by default.
    /// </summary>
    public Product()
    {
        CreatedAt = DateTime.UtcNow;
        IsActive = true;
        StockQuantity = 0;
    }

    /// <summary>
    /// Updates the product's information.
    /// Does not update stock quantity - use AddStock or RemoveStock for that.
    /// </summary>
    /// <param name="name">The product's name</param>
    /// <param name="description">The product's description</param>
    /// <param name="price">The product's price</param>
    /// <param name="category">The product's category</param>
    public void Update(
        string name,
        string description,
        decimal price,
        string category)
    {
        Name = name;
        Description = description;
        Price = price;
        Category = category;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Adds stock to the product inventory.
    /// </summary>
    /// <param name="quantity">The quantity to add (must be positive)</param>
    /// <exception cref="ArgumentException">Thrown when quantity is zero or negative</exception>
    public void AddStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));

        StockQuantity += quantity;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Removes stock from the product inventory.
    /// Typically called when a sale is completed.
    /// </summary>
    /// <param name="quantity">The quantity to remove (must be positive)</param>
    /// <exception cref="ArgumentException">Thrown when quantity is zero or negative</exception>
    /// <exception cref="InvalidOperationException">Thrown when there's insufficient stock</exception>
    public void RemoveStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));
        
        if (StockQuantity < quantity)
            throw new InvalidOperationException($"Insufficient stock.  Available: {StockQuantity}, Requested: {quantity}");

        StockQuantity -= quantity;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Checks if there is sufficient stock available for a given quantity.
    /// </summary>
    /// <param name="quantity">The quantity to check</param>
    /// <returns>True if sufficient stock is available, otherwise false</returns>
    public bool HasSufficientStock(int quantity)
    {
        return StockQuantity >= quantity;
    }

    /// <summary>
    /// Activates the product. 
    /// Active products can be sold.
    /// </summary>
    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Deactivates the product.
    /// Inactive products cannot be sold but maintain historical data. 
    /// </summary>
    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }
}