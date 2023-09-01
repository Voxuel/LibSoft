using LibSoft_API.Data;
using LibSoft_Models;

namespace LibSoft_API.Services;

public class BookRepository : IServiceRepository<Book>
{
    private readonly LibSoftDbContext _context;
    public BookRepository(LibSoftDbContext context)
    {
        _context = context;
    }
    
    public Task<Book> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Book> GetItemById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Book> CreateItem(Book entity)
    {
        throw new NotImplementedException();
    }

    public Task<Book> UpdateItem(Book entity)
    {
        throw new NotImplementedException();
    }

    public Task<Book> DeleteItem(int id)
    {
        throw new NotImplementedException();
    }
}