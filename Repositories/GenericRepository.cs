using TableBooking.Data;
using TableBooking.Interfaces;
using TableBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace TableBooking.Repositories
{
    /// <summary>
    /// Generic repository for basic CRUD operations.
    /// </summary>
    /// <typeparam name="T">The type of entity the repository operates on.</typeparam>
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        //// <summary>
        /// Constructor to initialize the GenericRepository.
        /// </summary>
        /// <param name="context">The DbContext for EF Core operations.</param>
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        /// <summary>
        /// Get all entities asynchronously.
        /// </summary>
        public async Task<IEnumerable<T>> GeAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        /// <summary>
        /// Get an entity by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <returns>The entity with the specified identifier.</returns>
        public async Task<T> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

        /// <summary>
        /// Create a new entity asynchronously.
        /// </summary>
        /// <param name="dataEntry">The entity to be created.</param>
        public async Task Create(T dataEntry)
        {
            await _context.AddAsync(dataEntry);
        }

        /// <summary>
        /// Edit an existing entity asynchronously.
        /// </summary>
        /// <param name="Entity">The entity to be updated.</param>
        public void Edit(T Entity)
        {
            _dbSet.Update(Entity);
        }

        /// <summary>
        /// Delete an existing entity asynchronously.
        /// </summary>
        /// <param name="Entity">The entity to be deleted.</param>
        public void Delete(T Entity)
        {
            _context.Remove(Entity);
        }

        /// <summary>
        /// Save changes to the underlying context asynchronously.
        /// </summary>
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public Task Create(Table table)
        {
            throw new NotImplementedException();
        }
    }
}
