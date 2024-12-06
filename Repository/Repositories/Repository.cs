using Microsoft.EntityFrameworkCore;
using Repository.Domain;
using Repository.Interfaces;


namespace Repository.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly DbContext _context;
    private readonly DbSet<T> _dbSet;
    public Repository(Context context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }
    public T GetById(int id)
    {
        return _dbSet.Find(id);
        
    }
    public IEnumerable<T> GetAll()
    {
        return _dbSet;
    }

    public void Add(T entity)
    {
        _dbSet.Add(entity);
        _context.SaveChanges();
    }
    public void Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        _context.SaveChanges();
    }
    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
        _context.SaveChanges();
    }
    public void AddRange(IEnumerable<T> entity)
    {
        _dbSet.AddRange(entity);
        _context.SaveChanges();
    }
    public IQueryable<T> GetAllQueryable()
    {
        return _dbSet;
    }
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

}