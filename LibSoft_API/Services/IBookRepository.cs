using LibSoft_Models;

namespace LibSoft_API.Services;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllBooks();
    Task<Book> GetBookById(int id);
    Task<Book> CreateBook(Book bookCreate);
    Task<Book> UpdateBook(Book bookUpdate);
    Task<Book> DeleteBook(int id);
}