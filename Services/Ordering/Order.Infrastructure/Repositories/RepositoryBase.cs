using Microsoft.EntityFrameworkCore;
using Order.Core.Common;
using Order.Core.Repositories;
using Order.Infrastructure.Data;
using System.Linq.Expressions;

namespace Order.Infrastructure.Repositories;

public class RepositoryBase<T> : IAsyncRepository<T> where T : EntityBase
{
    private readonly OrderContext _context;
    public RepositoryBase(OrderContext context)
    {
        _context=context;
    }
    public async Task<T> AddAsync(T entity)
    {
        await _context.AddAsync<T>(entity);
        await _context.SaveChangesAsync();  
        return entity;
    }

    public async Task DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().Where(predicate).ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}
