﻿namespace LibSoft_Models;

public class BookCreateDTO
{
    public string Title { get; set; }
    
    public string Author { get; set; }
    
    public string Description { get; set; }
    public int? Year { get; set; }
    
    public string? Genre { get; set; }
}