﻿namespace SharpOutcome.HttpApiExample.Data;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    
    public string Isbn { get; set; }
    public string Genre { get; set; }
    public string Author { get; set; }
    public int Price { get; set; }
}