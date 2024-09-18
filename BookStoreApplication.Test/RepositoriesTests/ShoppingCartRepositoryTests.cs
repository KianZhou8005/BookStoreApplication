using BookStoreApplication.Data;
using BookStoreApplication.Models;
using BookStoreApplication.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BookStoreApplication.Test.RepositoriesTests;

public class ShoppingCartRepositoryTests
{
    private readonly ShoppingCartRepository _shoppingCartRepository;
    private readonly BookStoreContext _context;

    public ShoppingCartRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<BookStoreContext>()
            .UseInMemoryDatabase(databaseName: "ShoppingCartTestDb")
            .Options;

        _context = new BookStoreContext(options);

        // Seed data  
        if (!_context.ShoppingCarts.Any())
        {
            _context.ShoppingCarts.AddRange(
                new ShoppingCart { Id = 1, UserId = "user1", Books = new List<Book>() },
                new ShoppingCart { Id = 2, UserId = "user2", Books = new List<Book>() }
            );
            _context.SaveChanges();
        }

        _shoppingCartRepository = new ShoppingCartRepository(_context);
    }

    [Fact]
    public async Task GetCartByUserIdAsync_ShouldReturnCart_WhenCartExists()
    {
        var result = await _shoppingCartRepository.GetCartByUserIdAsync("user1");
        Assert.NotNull(result);
        Assert.Equal("user1", result.UserId);
    }

    [Fact]
    public async Task GetCartByUserIdAsync_ShouldReturnNull_WhenCartDoesNotExist()
    {
        var result = await _shoppingCartRepository.GetCartByUserIdAsync("unknown");
        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateCartAsync_ShouldUpdateCart()
    {
        var cart = new ShoppingCart { Id = 1, UserId = "user1", Books = new List<Book>() };
        await _shoppingCartRepository.UpdateCartAsync(cart);
        var result = await _context.ShoppingCarts.FindAsync(1);
        Assert.NotNull(result);
        Assert.Equal("user1", result.UserId);
    }
}
