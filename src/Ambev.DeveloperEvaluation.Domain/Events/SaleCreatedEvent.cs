using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events;

/// <summary>
/// Domain event raised when a new sale is created.
/// This event is published after the sale is successfully persisted.
/// </summary>
public class SaleCreatedEvent : INotification
{
    /// <summary>
    /// Gets the ID of the created sale.
    /// </summary>
    public Guid SaleId { get; }

    /// <summary>
    /// Gets the sale number.
    /// </summary>
    public string SaleNumber { get; }

    /// <summary>
    /// Gets the customer ID.
    /// </summary>
    public Guid CustomerId { get; }

    /// <summary>
    /// Gets the customer name.
    /// </summary>
    public string CustomerName { get; }

    /// <summary>
    /// Gets the customer email.
    /// </summary>
    public string CustomerEmail { get; }

    /// <summary>
    /// Gets the branch ID.
    /// </summary>
    public Guid BranchId { get; }

    /// <summary>
    /// Gets the branch name.
    /// </summary>
    public string BranchName { get; }

    /// <summary>
    /// Gets the total amount of the sale.
    /// </summary>
    public decimal TotalAmount { get; }

    /// <summary>
    /// Gets the number of items in the sale.
    /// </summary>
    public int ItemCount { get; }

    /// <summary>
    /// Gets the date and time when the sale was created.
    /// </summary>
    public DateTime CreatedAt { get; }

    /// <summary>
    /// Gets the items in the sale.
    /// </summary>
    public List<SaleCreatedEventItem> Items { get; }

    /// <summary>
    /// Initializes a new instance of the SaleCreatedEvent class.
    /// </summary>
    public SaleCreatedEvent(
        Guid saleId,
        string saleNumber,
        Guid customerId,
        string customerName,
        string customerEmail,
        Guid branchId,
        string branchName,
        decimal totalAmount,
        int itemCount,
        DateTime createdAt,
        List<SaleCreatedEventItem> items)
    {
        SaleId = saleId;
        SaleNumber = saleNumber;
        CustomerId = customerId;
        CustomerName = customerName;
        CustomerEmail = customerEmail;
        BranchId = branchId;
        BranchName = branchName;
        TotalAmount = totalAmount;
        ItemCount = itemCount;
        CreatedAt = createdAt;
        Items = items;
    }
}

/// <summary>
/// Represents an item in the SaleCreatedEvent.
/// </summary>
public class SaleCreatedEventItem
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string ProductSku { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalAmount { get; set; }
}