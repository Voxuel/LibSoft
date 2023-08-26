using System.ComponentModel.DataAnnotations;

namespace LibSoft_Models;

public class Book
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Title { get; set; }
    
    [Required]
    public string Author { get; set; }
    
    public int Year { get; set; }
    
    [Required]
    public string Genre { get; set; }
    
    public string Description { get; set; }
}