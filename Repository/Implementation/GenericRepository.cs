using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Inventra.Data;
using Inventra.Models.DomainModels;
using Inventra.Repository.Interface;

namespace Inventra.Repository.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly InventraDbContext dbContext;
        private readonly DbSet<T> dbSet;

        public GenericRepository(InventraDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            dbSet.Remove(entity);
            await Task.Yield();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            // Special handling for StockTransfer
            if (typeof(T) == typeof(StockTransfer))
            {
                var stockTransferQuery = (IQueryable<StockTransfer>)query;

                stockTransferQuery = stockTransferQuery
                    .Include(t => t.TransferDetails)
                    .ThenInclude(td => td.Product);

                query = (IQueryable<T>)stockTransferQuery;
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllNoTrackingAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbSet.AsNoTracking();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            // Special handling for StockTransfer
            if (typeof(T) == typeof(StockTransfer))
            {
                var stockTransferQuery = (IQueryable<StockTransfer>)query;

                stockTransferQuery = stockTransferQuery
                    .Include(t => t.TransferDetails)
                    .ThenInclude(td => td.Product);

                query = (IQueryable<T>)stockTransferQuery;
            }

            return await query.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            // Special handling for StockTransfer
            if (typeof(T) == typeof(StockTransfer))
            {
                var stockTransferQuery = (IQueryable<StockTransfer>)query;

                stockTransferQuery = stockTransferQuery
                    .Include(t => t.TransferDetails)
                    .ThenInclude(td => td.Product);

                query = (IQueryable<T>)stockTransferQuery;
            }

            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }

        public async Task UpdateAsync(T entity)
        {
            dbSet.Update(entity);
            await Task.Yield();
        }

        public async Task SaveAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> Predicate)
        {
            return await dbSet.FirstOrDefaultAsync(Predicate);
        }
        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> Predicate)
        {
            return await dbSet.AnyAsync(Predicate);
        }
    }
}
