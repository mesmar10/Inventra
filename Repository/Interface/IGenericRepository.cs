using System.Linq.Expressions;

namespace Inventra.Repository.Interface
{
    public interface IGenericRepository<T> where T :class
    {
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        Task AddAsync(T entity);
        Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> Predicate);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> Predicate);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> Predicate);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task SaveAsync();
    }
}
