using HarambeeCommerce.Persistence.Entities;
using System.Linq.Expressions;

namespace HarambeeCommerce.Persistence.Repository;

public interface IRepository<T> where T : BaseEntity
{
    T Find<TKey>(TKey id);
    
    Task<T> FindAsync<TKey>(TKey id);

    Task AddAsync(T entity);

    void AddRange(IEnumerable<T> entities);

    Task AddRangeAsync(IEnumerable<T> entities);

    IQueryable<T> List(Expression<Func<T, bool>> expression);

    void Update(T entity);

    Task SaveAsync();
}