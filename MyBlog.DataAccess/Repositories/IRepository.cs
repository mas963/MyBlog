using MyBlog.Core.Common;
using System.Linq.Expressions;

namespace MyBlog.DataAccess.Repositories;

public interface IRepository<T, in TKey> where T : class, IEntity<TKey>, new() where TKey: IEquatable<TKey>
{
    IQueryable<T> Get(Expression<Func<T, bool>> predicate = null);
    Task<T> GetAsync(Expression<Func<T, bool>> predicate);
    Task<T> GetByIdAsync(TKey id);
    Task<T> AddAsync(T entity);
    Task<bool> AddRangeAsync(IEnumerable<T> entities);
    Task<T> UpdateAsync(TKey id, T entity);
    Task<T> UpdateAsync(T entity, Expression<Func<T, bool>> predicate);
    Task<T> DeleteAsync(T entity);
    Task<T> DeleteAsync(TKey id);
    Task<T> DeleteAsync(Expression<Func<T, bool>> filter);
}
