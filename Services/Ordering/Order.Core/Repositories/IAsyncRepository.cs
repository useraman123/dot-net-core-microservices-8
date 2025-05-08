using Order.Core.Common;
using System.Linq.Expressions;

namespace Order.Core.Repositories;

public interface IAsyncRepository<T> where T :EntityBase
{
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T,bool>> predicate);
    Task<T> GetByIdAsync(int id);
    Task<T>AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(int id);
}
