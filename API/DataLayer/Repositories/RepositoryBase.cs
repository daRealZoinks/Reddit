using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories;

public class RepositoryBase<T> where T : BaseEntity
{
    private readonly DbSet<T> _dbSet;
    protected readonly AppDbContext AppDbContext;

    protected RepositoryBase(AppDbContext appDbContext)
    {
        AppDbContext = appDbContext;
        _dbSet = AppDbContext.Set<T>();
    }

    public List<T> GetAll()
    {
        return _dbSet.ToList();
    }

    public T? GetById(int id)
    {
        return _dbSet.FirstOrDefault(x => x.Id == id);
    }

    public void Add(T entity)
    {
        _dbSet.Add(entity);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }
}