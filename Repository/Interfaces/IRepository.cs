namespace Repository.Interfaces;

public interface IRepository<T>: IDisposable
{
    T GetById(int id);
    IEnumerable<T> GetAll();
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    void AddRange(IEnumerable<T> entity);
    IQueryable<T> GetAllQueryable();
}