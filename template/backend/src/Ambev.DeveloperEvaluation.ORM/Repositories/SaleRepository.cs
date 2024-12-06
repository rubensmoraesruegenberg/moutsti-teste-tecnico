using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly DefaultContext _context;

        /// <summary>
        /// Initializes a new instance of UserRepository
        /// </summary>
        /// <param name="context">The database context</param>
        public SaleRepository(DefaultContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new Sale in the database
        /// </summary>
        /// <param name="Sale">The Sale to create</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created user</returns>
        public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            await _context.Sales.AddAsync(sale, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return sale;
        }


        /// <summary>
        /// Gets a paginated list of sales
        /// </summary>
        /// <param name="pageNumber">The page number</param>
        /// <param name="pageSize">The page size</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The paginated list of sales</returns>
        public async Task<IEnumerable<Sale>> GetSalesAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await _context.Sales
                .Include(s => s.SaleItems)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken); 
        }

        /// <summary>
        /// Gets the total count of sales
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The total count of sales</returns>
        public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Sales.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }


        /// <summary>
        /// Gets the total count of sales
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The total count of sales</returns>
        public async Task<int> GetSalesCountAsync(CancellationToken cancellationToken)
        {
            return await _context.Sales.CountAsync(cancellationToken);
        }

        public async Task UpdateAsync(Sale sale, CancellationToken cancellationToken)
        {
            _context.Sales.Update(sale);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}



