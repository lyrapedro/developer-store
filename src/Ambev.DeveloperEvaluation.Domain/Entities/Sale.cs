using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a sale transaction in the system.
/// This is the aggregate root for the sale context, containing all sale items.
/// Implements the External Identities pattern with denormalization for Customer and Branch data.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class Sale : BaseEntity
{
    /// <summary>
    /// Gets or sets the unique sale number.
    /// This is a business-friendly identifier (e.g., "SALE-2026-0001").
    /// Must be unique across all sales. 
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date and time when the sale was made.
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// Gets or sets the customer ID (External Identity pattern).
    /// References the Customer entity from the customer domain.
    /// This is denormalized to avoid tight coupling between domains.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the customer's name at the time of sale (denormalized).
    /// Stored to avoid queries to the customer domain and maintain historical accuracy.
    /// </summary>
    public string CustomerName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the customer's email at the time of sale (denormalized).
    /// Stored to avoid queries to the customer domain and maintain historical accuracy.
    /// </summary>
    public string CustomerEmail { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch ID where the sale was made (External Identity pattern).
    /// References the Branch entity from the branch domain. 
    /// This is denormalized to avoid tight coupling between domains.
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// Gets or sets the branch name at the time of sale (denormalized).
    /// Stored to avoid queries to the branch domain and maintain historical accuracy.
    /// </summary>
    public string BranchName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch code at the time of sale (denormalized).
    /// Stored to avoid queries to the branch domain and maintain historical accuracy.
    /// </summary>
    public string BranchCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the collection of items in this sale.
    /// Part of the Sale aggregate - items cannot exist without a sale.
    /// </summary>
    public List<SaleItem> Items { get; set; } = new();

    /// <summary>
    /// Gets or sets the total amount of the sale.
    /// This is calculated from the sum of all item totals.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets or sets whether the sale has been cancelled.
    /// Cancelled sales are kept for historical and auditing purposes.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the sale was cancelled.
    /// Null if the sale has not been cancelled.
    /// </summary>
    public DateTime? CancelledAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the sale was created. 
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time of the last update to the sale. 
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Initializes a new instance of the Sale class. 
    /// Sets the creation and sale timestamps, and initializes the items collection.
    /// </summary>
    public Sale()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        SaleDate = DateTime.UtcNow;
        Items = new List<SaleItem>();
        IsCancelled = false;
        TotalAmount = 0;
    }

    /// <summary>
    /// Adds an item to the sale. 
    /// Recalculates the total amount after adding the item.
    /// </summary>
    /// <param name="item">The sale item to add</param>
    /// <exception cref="InvalidOperationException">Thrown when trying to add items to a cancelled sale</exception>
    /// <exception cref="ArgumentNullException">Thrown when item is null</exception>
    public void AddItem(SaleItem item)
    {
        if (IsCancelled)
            throw new InvalidOperationException("Cannot add items to a cancelled sale");

        if (item == null)
            throw new ArgumentNullException(nameof(item));

        Items.Add(item);
        CalculateTotal();
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Removes an item from the sale by its ID.
    /// Recalculates the total amount after removing the item.
    /// </summary>
    /// <param name="itemId">The ID of the item to remove</param>
    /// <exception cref="InvalidOperationException">Thrown when trying to remove items from a cancelled sale</exception>
    public void RemoveItem(Guid itemId)
    {
        if (IsCancelled)
            throw new InvalidOperationException("Cannot remove items from a cancelled sale");

        var item = Items.FirstOrDefault(i => i.Id == itemId);
        if (item != null)
        {
            Items.Remove(item);
            CalculateTotal();
            UpdatedAt = DateTime.UtcNow;
        }
    }

    /// <summary>
    /// Updates the denormalized customer information.
    /// Should be called if customer details change after sale creation but before completion.
    /// </summary>
    /// <param name="customerName">The updated customer name</param>
    /// <param name="customerEmail">The updated customer email</param>
    /// <exception cref="InvalidOperationException">Thrown when trying to update a cancelled sale</exception>
    public void UpdateCustomerInfo(string customerName, string customerEmail)
    {
        if (IsCancelled)
            throw new InvalidOperationException("Cannot update a cancelled sale");

        CustomerName = customerName;
        CustomerEmail = customerEmail;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Updates the denormalized branch information. 
    /// Should be called if branch details change after sale creation but before completion. 
    /// </summary>
    /// <param name="branchName">The updated branch name</param>
    /// <param name="branchCode">The updated branch code</param>
    /// <exception cref="InvalidOperationException">Thrown when trying to update a cancelled sale</exception>
    public void UpdateBranchInfo(string branchName, string branchCode)
    {
        if (IsCancelled)
            throw new InvalidOperationException("Cannot update a cancelled sale");

        BranchName = branchName;
        BranchCode = branchCode;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Cancels the sale with a specified reason.
    /// Cancelled sales cannot be modified but are kept for historical and auditing purposes.
    /// </summary>
    /// <param name="reason">The reason for cancellation (required for auditing)</param>
    /// <exception cref="InvalidOperationException">Thrown when the sale is already cancelled</exception>
    /// <exception cref="ArgumentException">Thrown when reason is null or empty</exception>
    public void Cancel()
    {
        if (IsCancelled)
            throw new InvalidOperationException("Sale is already cancelled");

        IsCancelled = true;
        CancelledAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Reactivates a previously cancelled sale.
    /// Clears the cancellation information. 
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when the sale is not cancelled</exception>
    public void Reactivate()
    {
        if (!IsCancelled)
            throw new InvalidOperationException("Sale is not cancelled");

        IsCancelled = false;
        CancelledAt = null;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Calculates the total amount of the sale.
    /// This is the sum of all item totals (considering quantities, prices, and discounts).
    /// </summary>
    public void CalculateTotal()
    {
        TotalAmount = Items.Sum(item => item.TotalAmount);
    }

    /// <summary>
    /// Recalculates all totals in the sale.
    /// First recalculates each item's total, then recalculates the sale total.
    /// Useful after bulk updates to items. 
    /// </summary>
    public void RecalculateTotals()
    {
        foreach (var item in Items)
        {
            item.CalculateTotal();
        }
        CalculateTotal();
        UpdatedAt = DateTime.UtcNow;
    }
}