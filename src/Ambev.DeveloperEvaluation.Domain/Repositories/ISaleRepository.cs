using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Sale entity operations.
/// Defines the contract for sale data access operations.
/// </summary>
public interface ISaleRepository
{
    /// <summary>
    /// Creates a new sale in the database. 
    /// </summary>
    /// <param name="sale">The sale to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created sale</returns>
    Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a sale by its unique identifier, including all items. 
    /// </summary>
    /// <param name="id">The unique identifier of the sale</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale if found, null otherwise</returns>
    Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a sale by its sale number, including all items.
    /// </summary>
    /// <param name="saleNumber">The sale number to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale if found, null otherwise</returns>
    Task<Sale?> GetBySaleNumberAsync(string saleNumber, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all sales from the database, including all items.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of all sales</returns>
    Task<List<Sale>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves sales by customer ID.
    /// </summary>
    /// <param name="customerId">The customer ID to filter by</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of sales for the specified customer</returns>
    Task<List<Sale>> GetByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves sales by branch ID. 
    /// </summary>
    /// <param name="branchId">The branch ID to filter by</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of sales for the specified branch</returns>
    Task<List<Sale>> GetByBranchIdAsync(Guid branchId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves sales within a date range.
    /// </summary>
    /// <param name="startDate">The start date of the range</param>
    /// <param name="endDate">The end date of the range</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of sales within the specified date range</returns>
    Task<List<Sale>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves only cancelled sales. 
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of cancelled sales</returns>
    Task<List<Sale>> GetCancelledAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves only active (non-cancelled) sales.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of active sales</returns>
    Task<List<Sale>> GetActiveAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing sale in the database. 
    /// </summary>
    /// <param name="sale">The sale to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated sale</returns>
    Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a sale from the database.
    /// Note: This will cascade delete all sale items.
    /// </summary>
    /// <param name="id">The unique identifier of the sale to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the sale was deleted, false if not found</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}