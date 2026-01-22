using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Branch entity operations. 
/// Defines the contract for branch data access operations.
/// </summary>
public interface IBranchRepository
{
    /// <summary>
    /// Creates a new branch in the database.
    /// </summary>
    /// <param name="branch">The branch to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created branch</returns>
    Task<Branch> CreateAsync(Branch branch, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a branch by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the branch</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The branch if found, null otherwise</returns>
    Task<Branch?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a branch by its code.
    /// </summary>
    /// <param name="code">The branch code to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The branch if found, null otherwise</returns>
    Task<Branch? > GetByCodeAsync(string code, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all branches from the database. 
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of all branches</returns>
    Task<List<Branch>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves only active branches from the database.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of active branches</returns>
    Task<List<Branch>> GetActiveAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing branch in the database.
    /// </summary>
    /// <param name="branch">The branch to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated branch</returns>
    Task<Branch> UpdateAsync(Branch branch, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a branch from the database. 
    /// </summary>
    /// <param name="id">The unique identifier of the branch to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the branch was deleted, false if not found</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}