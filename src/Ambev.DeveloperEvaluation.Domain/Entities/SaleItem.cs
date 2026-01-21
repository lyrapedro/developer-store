using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a sale in the system.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class SaleItem : BaseEntity
{
    /// <summary>
    /// Reference to the Sale.
    /// </summary>
    public Guid SaleId { get; private set; }

    /// <summary>
    /// Reference to the Product
    /// </summary>
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; }
    public string ProductSku { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal Discount { get; private set; }
    public decimal TotalAmount { get; private set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // Construtor privado para EF Core
    private SaleItem() { }

    // Construtor para criar novo item
    public SaleItem(
        Guid saleId,
        Guid productId,
        string productName,
        string productSku,
        int quantity,
        decimal unitPrice,
        decimal discount = 0)
    {
        // Validações
        if (saleId == Guid.Empty)
            throw new ArgumentException("Sale ID cannot be empty", nameof(saleId));
        if (productId == Guid.Empty)
            throw new ArgumentException("Product ID cannot be empty", nameof(productId));
        if (string.IsNullOrWhiteSpace(productName))
            throw new ArgumentException("Product name is required", nameof(productName));
        if (string.IsNullOrWhiteSpace(productSku))
            throw new ArgumentException("Product SKU is required", nameof(productSku));
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));
        if (unitPrice < 0)
            throw new ArgumentException("Unit price cannot be negative", nameof(unitPrice));
        if (discount < 0)
            throw new ArgumentException("Discount cannot be negative", nameof(discount));

        SaleId = saleId;
        ProductId = productId;
        ProductName = productName;
        ProductSku = productSku;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Discount = discount;

        CalculateTotal();
    }

    // Atualiza quantidade
    public void UpdateQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));

        Quantity = quantity;
        CalculateTotal();
        UpdateTimestamp();
    }

    // Atualiza preço unitário
    public void UpdateUnitPrice(decimal unitPrice)
    {
        if (unitPrice < 0)
            throw new ArgumentException("Unit price cannot be negative", nameof(unitPrice));

        UnitPrice = unitPrice;
        CalculateTotal();
        UpdateTimestamp();
    }

    // Aplica desconto
    public void ApplyDiscount(decimal discount)
    {
        if (discount < 0)
            throw new ArgumentException("Discount cannot be negative", nameof(discount));

        var subtotal = Quantity * UnitPrice;
        if (discount > subtotal)
            throw new ArgumentException("Discount cannot be greater than subtotal", nameof(discount));

        Discount = discount;
        CalculateTotal();
        UpdateTimestamp();
    }

    // Atualiza informações do produto (denormalizadas)
    public void UpdateProductInfo(string productName, string productSku)
    {
        ProductName = productName ?? throw new ArgumentNullException(nameof(productName));
        ProductSku = productSku ?? throw new ArgumentNullException(nameof(productSku));
        UpdateTimestamp();
    }

    // Calcula o total do item
    public void CalculateTotal()
    {
        var subtotal = Quantity * UnitPrice;
        TotalAmount = subtotal - Discount;
    }
    
    protected void UpdateTimestamp()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}