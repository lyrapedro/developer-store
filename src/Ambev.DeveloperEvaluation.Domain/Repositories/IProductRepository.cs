using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Product entity operations.
/// Defines the contract for product data access operations.
/// </summary>
public interface IProductRepository
{
    /// <summary>
    /// Creates a new product in the database.
    /// </summary>
    /// <param name="product">The product to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created product</returns>
    Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a product by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the product</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product if found, null otherwise</returns>
    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a product by its SKU.
    /// </summary>
    /// <param name="sku">The product SKU to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product if found, null otherwise</returns>
    Task<Product? > GetBySkuAsync(string sku, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all products from the database.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of all products</returns>
    Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves only active products from the database.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of active products</returns>
    Task<List<Product>> GetActiveAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves products by category.
    /// </summary>
    /// <param name="category">The category to filter by</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of products in the specified category</returns>
    Task<List<Product>> GetByCategoryAsync(string category, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing product in the database.
    /// </summary>
    /// <param name="product">The product to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated product</returns>
    Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a product from the database.
    /// </summary>
    /// <param name="id">The unique identifier of the product to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the product was deleted, false if not found</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}