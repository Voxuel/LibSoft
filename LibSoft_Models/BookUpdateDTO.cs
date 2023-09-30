namespace LibSoft_Models;

public class BookUpdateDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    
    public string Author { get; set; }
    
    public string Genre { get; set; }
    public int? Year { get; set; }
    
    public string Description { get; set; }
    public int NumbersInStock { get; set; }
}