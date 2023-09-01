using LibSoft_Models;

namespace LibSoft_API.Services;

public interface IBookRepository
{
    Task<T> GetAllBooks<T>();
    Task<T> GetBookById<T>(int id);
    Task<T> CreateBook<T>(BookDTO bookCreate);
    Task<T> UpdateBook<T>(BookDTO bookUpdate);
    Task<T> DeleteBook<T>(int id);
}