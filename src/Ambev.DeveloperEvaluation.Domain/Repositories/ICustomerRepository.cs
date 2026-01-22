using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Customer entity operations. 
/// Defines the contract for customer data access operations.
/// </summary>
public interface ICustomerRepository
{
    /// <summary>
    /// Creates a new customer in the database.
    /// </summary>
    /// <param name="customer">The customer to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created customer</returns>
    Task<Customer> CreateAsync(Customer customer, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a customer by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the customer</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The customer if found, null otherwise</returns>
    Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a customer by their email address. 
    /// </summary>
    /// <param name="email">The email address to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The customer if found, null otherwise</returns>
    Task<Customer?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a customer by their document (CPF/CNPJ).
    /// </summary>
    /// <param name="document">The document number to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The customer if found, null otherwise</returns>
    Task<Customer?> GetByDocumentAsync(string document, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all customers from the database. 
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of all customers</returns>
    Task<List<Customer>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves only active customers from the database.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of active customers</returns>
    Task<List<Customer>> GetActiveAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing customer in the database.
    /// </summary>
    /// <param name="customer">The customer to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated customer</returns>
    Task<Customer> UpdateAsync(Customer customer, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a customer from the database.
    /// </summary>
    /// <param name="id">The unique identifier of the customer to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the customer was deleted, false if not found</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}