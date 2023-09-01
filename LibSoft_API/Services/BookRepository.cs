using LibSoft_API.Data;
using LibSoft_Models;
using Microsoft.EntityFrameworkCore;

namespace LibSoft_API.Services;

public class BookRepository : IBookRepository
{
    private readonly LibSoftDbContext _context;
    public BookRepository(LibSoftDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Book>> GetAllBooks()
    {
        var result = await _context.Books.ToListAsync();

        return result.Any() ? result : null;
    }

    public async Task<Book> GetBookById(int id)
    {
        var result = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);

        return result;
    }

    public async Task<Book> CreateBook(Book bookCreate)
    {
        var result = await _context.Books.AddAsync(bookCreate);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<Book> UpdateBook(Book bookUpdate)
    {
        var result = await _context.Books.FirstOrDefaultAsync(b =>
            b.Id == bookUpdate.Id);

        if (result == null) return null;

        result.Title = bookUpdate.Title;
        result.Description = bookUpdate.Description;
        result.Genre = bookUpdate.Genre;
        result.Author = bookUpdate.Author;
        result.Year = bookUpdate.Year;
        result.Id = bookUpdate.Id;

        await _context.SaveChangesAsync();
        return result;
    }

    public async Task<Book> DeleteBook(int id)
    {
        var result = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
        if (result == null) return null;

        _context.Books.Remove(result);
        await _context.SaveChangesAsync();
        return result;
    }
}