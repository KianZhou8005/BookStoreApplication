using BookStoreApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApplication.Data;

public class BookStoreContext : IdentityDbContext<User>
{
    public BookStoreContext(DbContextOptions<BookStoreContext> options)
        : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
}
