using BookStoreApplication.Interfaces;
using BookStoreApplication.Models;
using BookStoreApplication.Services;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BookStoreApplication.Test.ServiceTests;
public class ShoppingCartServiceTests
{
    private readonly Mock<IShoppingCartRepository> _cartRepositoryMock;
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly ShoppingCartService _shoppingCartService;

    public ShoppingCartServiceTests()
    {
        _cartRepositoryMock = new Mock<IShoppingCartRepository>();
        _bookRepositoryMock = new Mock<IBookRepository>();
        _shoppingCartService = new ShoppingCartService(_cartRepositoryMock.Object, _bookRepositoryMock.Object);
    }

    [Fact]
    public async Task AddToCartAsync_ShouldAddBookToCart_WhenBookExists()
    {
        var book = new Book { Id = 1, Title = "Book 1" };
        var cart = new ShoppingCart { UserId = "user1", Books = new List<Book>() };
        _bookRepositoryMock.Setup(x => x.GetBookByIdAsync(1)).ReturnsAsync(book);
        _cartRepositoryMock.Setup(x => x.GetCartByUserIdAsync("user1")).ReturnsAsync(cart);
        _cartRepositoryMock.Setup(x => x.UpdateCartAsync(It.IsAny<ShoppingCart>())).ReturnsAsync(cart);

        var result = await _shoppingCartService.AddToCartAsync("user1", 1);

        Assert.Contains(book, result.Books);
    }

    [Fact]
    public async Task CheckoutAsync_ShouldReturnTotalPrice_WhenCartExists()
    {
        var books = new List<Book> { new Book { Id = 1, Price = 10 }, new Book { Id = 2, Price = 15 } };
        var cart = new ShoppingCart { UserId = "user1", Books = books };
        _cartRepositoryMock.Setup(x => x.GetCartByUserIdAsync("user1")).ReturnsAsync(cart);

        var result = await _shoppingCartService.CheckoutAsync("user1");

        Assert.Equal(25, result);
    }
}
