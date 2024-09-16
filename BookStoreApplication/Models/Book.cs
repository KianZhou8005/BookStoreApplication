﻿using System.ComponentModel.DataAnnotations;

namespace BookStoreApplication.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public decimal Price { get; set; }
        public string? Category { get; set; }
    }
}
