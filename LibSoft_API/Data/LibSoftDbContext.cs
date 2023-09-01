using LibSoft_Models;
using Microsoft.EntityFrameworkCore;

namespace LibSoft_API.Data;

public class LibSoftDbContext : DbContext
{
    public LibSoftDbContext(DbContextOptions<LibSoftDbContext> options): base(options)
    {
        
    }

    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Book>().HasData(new Book
        {
            Id = 1,
            Title = "Over the ocean",
            Author = "John R Sanders",
            Description = "John Sanders tour of the seven seas and what he faced along the way",
            Genre = "Autobiography",
            Year = 2011
        });
        modelBuilder.Entity<Book>().HasData(new Book
        {
            Id = 2,
            Title = "Patience",
            Author = "Dr Emilia Wing",
            Description = "The moments in medicine and its challenging nature",
            Genre = "Autobiography",
            Year = 2023
        });
        modelBuilder.Entity<Book>().HasData(new Book
        {
            Id = 3,
            Title = "The Highborn's demise",
            Author = "Martin Andersson",
            Description = "Set in 1800 england, a story about a beggars rise to become an assassin for" +
                          "a dark brotherhood created to dismantle the rich",
            Genre = "Fiction/Thriller",
            Year = 2015
        });
    }
}