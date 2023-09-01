using LibSoft_API.Data;
using LibSoft_Models;

namespace LibSoft_API.Services;

public class BookRepository : IBookRepository
{
    private readonly LibSoftDbContext _context;
    public BookRepository(LibSoftDbContext context)
    {
        _context = context;
    }

    public Task<T> GetAllBooks<T>()
    {
        throw new NotImplementedException();
    }

    public Task<T> GetBookById<T>(int id)
    {
        throw new NotImplementedException();
    }

    public Task<T> CreateBook<T>(BookDTO bookCreate)
    {
        throw new NotImplementedException();
    }

    public Task<T> UpdateBook<T>(BookDTO bookUpdate)
    {
        throw new NotImplementedException();
    }

    public Task<T> DeleteBook<T>(int id)
    {
        throw new NotImplementedException();
    }
}