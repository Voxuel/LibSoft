using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibSoft_Models;

public class Book
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Title { get; set; }
    
    [Required]
    public string Author { get; set; }
    
    public int? Year { get; set; }
    
    public string? Genre { get; set; }
    [Required]
    public string Description { get; set; }

    public int NumbersInStock { get; set; }
}