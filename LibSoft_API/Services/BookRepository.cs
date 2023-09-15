using LibSoft_API.Data;
using LibSoft_Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LibSoft_API.Services;

/// <summary>
/// Handles CRUD operations for book repository implmentation.
/// </summary>
public class BookRepository : IBookRepository
{
    private readonly LibSoftDbContext _context;
    public BookRepository(LibSoftDbContext context)
    {
        _context = context;
    }
    
    /// <summary>
    /// Gets all books in the database.
    /// </summary>
    /// <returns>TResult of IEnumerable of type "Book"</returns>
    public async Task<IEnumerable<Book>> GetAllBooks()
    {
        var result = await _context.Books.ToListAsync();

        return result.Any() ? result : null;
    }
    /// <summary>
    /// Gets the book with the given ID.
    /// </summary>
    /// <param name="id">ID of book to be searched for</param>
    /// <returns>TResult of type "Book"</returns>
    public async Task<Book> GetBookById(int id)
    {
        var result = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);

        return result;
    }
    
    /// <summary>
    /// Creates new entity of type "Book" and adds it to the database.
    /// </summary>
    /// <param name="bookCreate">Book object to be added to the database.</param>
    /// <returns>TResult of type "Book".</returns>
    public async Task<Book> CreateBook(Book bookCreate)
    {
        var found = await _context.Books.FirstOrDefaultAsync(b =>
            b.Title == bookCreate.Title && 
            b.Author == bookCreate.Author);
        if (found == null)
        {
            bookCreate.NumbersInStock = 1;
            var result = await _context.Books.AddAsync(bookCreate);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
        found.NumbersInStock += 1;
        await _context.SaveChangesAsync();
        return found;
    }
    /// <summary>
    /// Updates an existing entity in the database.
    /// </summary>
    /// <param name="bookUpdate">The entity to be updated.</param>
    /// <returns>TResult of type "Book"</returns>
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
    /// <summary>
    /// Deletes entity with specified ID
    /// </summary>
    /// <param name="id">ID for entity to be deleted.</param>
    /// <returns>TResult of type "Book"</returns>
    public async Task<Book> DeleteBook(int id)
    {
        var result = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
        if (result == null) return null;

        if (result.NumbersInStock > 0)
        {
            result.NumbersInStock -= 1;
            await _context.SaveChangesAsync();
            return result;
        }
        _context.Books.Remove(result);
        await _context.SaveChangesAsync();
        return result;
    }

    public async Task<IEnumerable<Book>> SearchByAuthor(string auth)
    {
        var result = await _context.Books.Where(b =>
            b.Author == auth).ToListAsync();

        return result.Any() ? result : null;
    }

    public async Task<IEnumerable<Book>> SearchByGenre(string genre)
    {
        var result = await _context.Books.Where(b =>
            b.Genre == genre).ToListAsync();

        return result.Any() ? result : null;
    }
}