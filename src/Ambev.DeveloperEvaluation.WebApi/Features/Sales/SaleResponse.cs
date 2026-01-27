namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

/// <summary>
/// API response model for Sale operations.
/// </summary>
public class SaleResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the sale.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the sale number.
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date when the sale was made.
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// Gets or sets the customer information.
    /// </summary>
    public GetSaleCustomerInfo Customer { get; set; } = new();

    /// <summary>
    /// Gets or sets the branch information.
    /// </summary>
    public GetSaleBranchInfo Branch { get; set; } = new();

    /// <summary>
    /// Gets or sets the list of sale items.
    /// </summary>
    public List<GetSaleItemInfo> Items { get; set; } = new();

    /// <summary>
    /// Gets or sets the total amount of the sale. 
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets or sets whether the sale is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the sale was cancelled. 
    /// </summary>
    public DateTime? CancelledAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the sale was created. 
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time of the last update.
    /// </summary>
    public DateTime?  UpdatedAt { get; set; }
}

/// <summary>
/// Customer information in get sale result.
/// </summary>
public class GetSaleCustomerInfo
{
    /// <summary>
    /// Gets or sets the customer ID.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the customer name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the customer email.
    /// </summary>
    public string Email { get; set; } = string.Empty;
}

/// <summary>
/// Branch information in get sale result. 
/// </summary>
public class GetSaleBranchInfo
{
    /// <summary>
    /// Gets or sets the branch ID. 
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the branch name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch code.
    /// </summary>
    public string Code { get; set; } = string.Empty;
}

/// <summary>
/// Sale item information in get sale result. 
/// </summary>
public class GetSaleItemInfo
{
    /// <summary>
    /// Gets or sets the item ID.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the product ID.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the product name.
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the product SKU.
    /// </summary>
    public string ProductSku { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the quantity.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the unit price.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the discount applied.
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// Gets or sets the total amount for this item.
    /// </summary>
    public decimal TotalAmount { get; set; }
}