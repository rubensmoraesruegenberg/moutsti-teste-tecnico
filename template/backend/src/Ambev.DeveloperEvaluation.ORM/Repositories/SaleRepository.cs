using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

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
        //public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
        //{
        //    await _context.Sales.AddAsync(sale, cancellationToken);
        //    await _context.SaveChangesAsync(cancellationToken);
        //    return sale;
        //}

        public Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
