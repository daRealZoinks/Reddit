namespace Core.Services;

public interface ICollectionService<T> where T : new()
{
    void Add(T entity);
    List<T> GetAll();
    T? GetById(int id);
    void Update(T entity);
    void Delete(int id);
}