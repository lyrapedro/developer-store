using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents an item within a sale transaction.
/// Part of the Sale aggregate - cannot exist independently.
/// Implements the External Identities pattern with denormalization for Product data.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class SaleItem : BaseEntity
{
    private const int MinimumQuantityForDiscount = 4;
    private const int MaximumQuantityAllowed = 20;
    private const int QuantityForHigherDiscount = 10;
    private const decimal LowerDiscountPercentage = 0.10m; // 10%
    private const decimal HigherDiscountPercentage = 0.20m; // 20%

    /// <summary>
    /// Gets or sets the ID of the sale this item belongs to.
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// Gets or sets the product ID (External Identity pattern).
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the product's name at the time of sale (denormalized).
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the product's SKU at the time of sale (denormalized).
    /// </summary>
    public string ProductSku { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the quantity of this product sold.
    /// Must be greater than zero and cannot exceed 20 items.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the unit price of the product at the time of sale.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the discount applied to this item.
    /// Automatically calculated based on quantity rules.
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// Gets or sets the total amount for this item.
    /// Calculated as: (Quantity × UnitPrice) - Discount
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the item was added to the sale.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time of the last update to the item.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Initializes a new instance of the SaleItem class.
    /// </summary>
    public SaleItem()
    {
        CreatedAt = DateTime.UtcNow;
        Quantity = 1;
        Discount = 0;
    }

    /// <summary>
    /// Validates the quantity against business rules.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when quantity exceeds maximum allowed</exception>
    private void ValidateQuantity()
    {
        if (Quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero", nameof(Quantity));

        if (Quantity > MaximumQuantityAllowed)
            throw new InvalidOperationException(
                $"Cannot sell more than {MaximumQuantityAllowed} identical items. Requested: {Quantity}");
    }

    /// <summary>
    /// Calculates and applies the appropriate discount based on quantity.
    /// Business Rules:
    /// - Purchases below 4 items: NO discount allowed
    /// - Purchases of 4-9 items: 10% discount
    /// - Purchases of 10-20 items: 20% discount
    /// - Purchases above 20 items: NOT ALLOWED
    /// </summary>
    /// <returns>The calculated discount amount</returns>
    public decimal CalculateDiscount()
    {
        ValidateQuantity();

        var subtotal = Quantity * UnitPrice;

        if (Quantity < MinimumQuantityForDiscount)
            return 0m;

        if (Quantity >= QuantityForHigherDiscount)
            return subtotal * HigherDiscountPercentage;
        
        return subtotal * LowerDiscountPercentage;
    }

    /// <summary>
    /// Validates that manual discount follows business rules.
    /// Manual discounts are only allowed for quantities of 4 or more items.
    /// </summary>
    /// <param name="manualDiscount">The manual discount amount to validate</param>
    /// <exception cref="InvalidOperationException">Thrown when discount rules are violated</exception>
    private void ValidateManualDiscount(decimal manualDiscount)
    {
        if (manualDiscount < 0)
            throw new ArgumentException("Discount cannot be negative", nameof(manualDiscount));

        if (Quantity < MinimumQuantityForDiscount && manualDiscount > 0)
            throw new InvalidOperationException(
                $"Discounts are not allowed for purchases below {MinimumQuantityForDiscount} items");

        var subtotal = Quantity * UnitPrice;
        if (manualDiscount > subtotal)
            throw new ArgumentException(
                $"Discount cannot exceed subtotal. Subtotal: {subtotal:C}, Discount: {manualDiscount:C}",
                nameof(manualDiscount));
    }

    /// <summary>
    /// Updates the quantity and automatically recalculates discount and total.
    /// </summary>
    /// <param name="quantity">The new quantity</param>
    public void UpdateQuantity(int quantity)
    {
        Quantity = quantity;
        ValidateQuantity();
        
        // Automatically recalculate discount based on new quantity
        Discount = CalculateDiscount();
        CalculateTotal();
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Updates the unit price and recalculates discount and total.
    /// </summary>
    /// <param name="unitPrice">The new unit price</param>
    public void UpdateUnitPrice(decimal unitPrice)
    {
        if (unitPrice < 0)
            throw new ArgumentException("Unit price cannot be negative", nameof(unitPrice));

        UnitPrice = unitPrice;
        
        // Recalculate discount with new price
        Discount = CalculateDiscount();
        CalculateTotal();
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Applies a manual discount to this item.
    /// Manual discount must follow business rules (only for 4+ items).
    /// </summary>
    /// <param name="discount">The manual discount amount</param>
    public void ApplyManualDiscount(decimal discount)
    {
        ValidateManualDiscount(discount);
        
        Discount = discount;
        CalculateTotal();
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Applies automatic discount based on quantity rules.
    /// This is the recommended way to apply discounts.
    /// </summary>
    public void ApplyAutomaticDiscount()
    {
        Discount = CalculateDiscount();
        CalculateTotal();
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Updates the denormalized product information.
    /// </summary>
    public void UpdateProductInfo(string productName, string productSku)
    {
        ProductName = productName;
        ProductSku = productSku;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Calculates the total amount for this item.
    /// Formula: (Quantity × UnitPrice) - Discount
    /// </summary>
    public void CalculateTotal()
    {
        var subtotal = Quantity * UnitPrice;
        TotalAmount = subtotal - Discount;
    }

    /// <summary>
    /// Gets the subtotal before discount is applied.
    /// </summary>
    public decimal GetSubtotal()
    {
        return Quantity * UnitPrice;
    }

    /// <summary>
    /// Gets the applied discount percentage.
    /// </summary>
    public decimal GetDiscountPercentage()
    {
        var subtotal = GetSubtotal();
        return subtotal > 0 ? (Discount / subtotal) * 100 : 0;
    }
}