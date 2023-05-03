namespace Core.Services
{
    public interface ICollectionService<T> where T : new()
    {
        List<T> GetAll();

        T? Get(Guid id);

        void Create(T model);

        void Update(T model);

        void Delete(Guid id);
    }
}
