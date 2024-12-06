using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    /// <summary>
    /// Interface for Sale repository operations.
    /// </summary>
    public interface ISaleRepository
    {
        /// <summary>
        /// Creates a new sale in the repository.
        /// </summary>
        /// <param name="sale">The sale to create.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The created sale.</returns>
        Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a paginated list of sales from the repository.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The paginated list of sales.</returns>
        Task<IEnumerable<Sale>> GetSalesAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the total count of sales in the repository.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The total count of sales.</returns>
        Task<int> GetSalesCountAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves a Sale by their unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the Sale</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The Sale if found, null otherwise</returns>
        Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update a new sale in the repository.
        /// </summary>
        /// <param name="sale">The sale to update.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The update sale.</returns>
        Task UpdateAsync(Sale sale, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes a sale from the repository
        /// </summary>
        /// <param name="id">The unique identifier of the sale to delete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>True if the sale was deleted, false if not found</returns>
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}




