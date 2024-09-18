using BookStoreApplication.Interfaces;
using BookStoreApplication.Models;
using BookStoreApplication.Services;
using Moq;

namespace BookStoreApplication.Test.ServiceTests;

public class BookServiceTests
{
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly BookService _bookService;

    public BookServiceTests()
    {
        _bookRepositoryMock = new Mock<IBookRepository>();
        _bookService = new BookService(_bookRepositoryMock.Object);
    }

    [Fact]
    public async Task GetAllBooksAsync_ShouldReturnAllBooks()
    {
        var books = new List<Book> { new Book { Id = 1, Title = "Book 1" }, new Book { Id = 2, Title = "Book 2" } };
        _bookRepositoryMock.Setup(x => x.GetAllBooksAsync()).ReturnsAsync(books);

        var result = await _bookService.GetAllBooksAsync();

        Assert.Equal(books, result);
    }

    [Fact]
    public async Task GetBookByIdAsync_ShouldReturnBook_WhenBookExists()
    {
        var book = new Book { Id = 1, Title = "Book 1" };
        _bookRepositoryMock.Setup(x => x.GetBookByIdAsync(1)).ReturnsAsync(book);

        var result = await _bookService.GetBookByIdAsync(1);

        Assert.Equal(book, result);
    }
}