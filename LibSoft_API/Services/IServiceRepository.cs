namespace LibSoft_API.Services;

public interface IServiceRepository<T>
{
    Task<T> GetAll();
    Task<T> GetItemById(int id);
    Task<T> CreateItem(T entity);
    Task<T> UpdateItem(T entity);
    Task<T> DeleteItem(int id);
}