using System.ComponentModel.DataAnnotations;

namespace BookStoreApplication.Models;

public class ShoppingCart
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public ICollection<Book> Books { get; set; } = new List<Book>();
}