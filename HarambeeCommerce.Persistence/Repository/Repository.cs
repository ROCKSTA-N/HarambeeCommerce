using HarambeeCommerce.Persistence.Contexts;
using HarambeeCommerce.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HarambeeCommerce.Persistence.Repository;

public class Repository<T> : IRepository<T> where T : BaseEntity 
{
    private readonly HarambeeCommerceContext _db; 
    private DbSet<T> _dbSet;

    protected DbSet<T> DbSet
    {
        get => _dbSet ??= _db.Set<T>();
    }

    public Repository(HarambeeCommerceContext db)
    {
        _db = db;
    }

    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }

    public T Find<TKey>(TKey id)
    {
        return DbSet.Find(id);
    }

    public async Task<T> FindAsync<TKey>(TKey id)
    {
        return await DbSet.FindAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        await DbSet.AddAsync(entity);
    }

    public void AddRange(IEnumerable<T> entities)
    {
        DbSet.AddRange(entities);
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await DbSet.AddRangeAsync(entities);
    }

    public IQueryable<T> List(Expression<Func<T, bool>> expression)
    {
        return DbSet.Where(expression);
    }

    public void Update(T entity)
    {
        DbSet.Update(entity);
    }
}
