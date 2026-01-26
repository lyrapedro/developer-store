using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events;

/// <summary>
/// Domain event raised when a sale is cancelled.
/// This event is published after the sale is successfully cancelled and stock is returned.
/// </summary>
public class SaleCancelledEvent : INotification
{
    /// <summary>
    /// Gets the ID of the cancelled sale.
    /// </summary>
    public Guid SaleId { get; }

    /// <summary>
    /// Gets the sale number.
    /// </summary>
    public string SaleNumber { get; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid CustomerId { get; }

    /// <summary>
    /// Gets the user name.
    /// </summary>
    public string CustomerName { get; }

    /// <summary>
    /// Gets the branch ID.
    /// </summary>
    public Guid BranchId { get; }

    /// <summary>
    /// Gets the branch name.
    /// </summary>
    public string BranchName { get; }

    /// <summary>
    /// Gets the total amount of the cancelled sale.
    /// </summary>
    public decimal TotalAmount { get; }

    /// <summary>
    /// Gets the reason for cancellation.
    /// </summary>
    public string CancellationReason { get; }

    /// <summary>
    /// Gets the date and time when the sale was cancelled.
    /// </summary>
    public DateTime CancelledAt { get; }

    /// <summary>
    /// Gets the date and time when the sale was originally created.
    /// </summary>
    public DateTime OriginalSaleDate { get; }

    /// <summary>
    /// Gets the user who cancelled the sale (if available).
    /// </summary>
    public string? CancelledBy { get; }

    /// <summary>
    /// Gets the items that were in the cancelled sale.
    /// </summary>
    public List<SaleCancelledEventItem> Items { get; }

    /// <summary>
    /// Initializes a new instance of the SaleCancelledEvent class.
    /// </summary>
    public SaleCancelledEvent(
        Guid saleId,
        string saleNumber,
        Guid userId,
        string userName,
        Guid branchId,
        string branchName,
        decimal totalAmount,
        DateTime cancelledAt,
        DateTime originalSaleDate,
        List<SaleCancelledEventItem> items,
        string? cancelledBy = null)
    {
        SaleId = saleId;
        SaleNumber = saleNumber;
        CustomerId = userId;
        CustomerName = userName;
        BranchId = branchId;
        BranchName = branchName;
        TotalAmount = totalAmount;
        CancelledAt = cancelledAt;
        OriginalSaleDate = originalSaleDate;
        Items = items;
        CancelledBy = cancelledBy;
    }
}

/// <summary>
/// Represents an item in the SaleCancelledEvent.
/// </summary>
public class SaleCancelledEventItem
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal TotalAmount { get; set; }
}