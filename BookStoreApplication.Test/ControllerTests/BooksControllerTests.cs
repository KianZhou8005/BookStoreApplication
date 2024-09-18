using System.Collections.Generic;
using System.Threading.Tasks;
using BookStoreApplication.Controllers;
using BookStoreApplication.Interfaces;
using BookStoreApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BookStoreApplication.Test.ControllerTests;

public class BooksControllerTests
{
    private readonly Mock<IBookService> _mockBookService;
    private readonly BooksController _booksController;

    public BooksControllerTests()
    {
        _mockBookService = new Mock<IBookService>();
        _booksController = new BooksController(_mockBookService.Object);
    }

    [Fact]
    public async Task GetBooks_ReturnsOkResult_WithListOfBooks()
    {
        // Arrange  
        var mockBooks = new List<Book>
        {
            new Book { Id = 1, Title = "Book 1" },
            new Book { Id = 2, Title = "Book 2" }
        } as IEnumerable<Book>;

        _mockBookService.Setup(service => service.GetAllBooksAsync())
                        .ReturnsAsync(mockBooks);

        // Act  
        var result = await _booksController.GetBooks();

        // Assert  
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsAssignableFrom<IEnumerable<Book>>(okResult.Value);
        Assert.Equal(2, returnValue.Count());
    }

    [Fact]
    public async Task GetBook_ReturnsOkResult_WithBook()
    {
        // Arrange  
        var book = new Book { Id = 1, Title = "Book 1" };
        _mockBookService.Setup(service => service.GetBookByIdAsync(It.IsAny<int>()))
                        .ReturnsAsync(book);

        // Act  
        var result = await _booksController.GetBook(1);

        // Assert  
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<Book>(okResult.Value);
        Assert.Equal(book.Id, returnValue.Id);
    }

    [Fact]
    public async Task GetBook_ReturnsNotFound_WhenBookIsNotFound()
    {
        // Arrange  
        _mockBookService.Setup(service => service.GetBookByIdAsync(It.IsAny<int>()))
                        .ReturnsAsync((Book)null);

        // Act  
        var result = await _booksController.GetBook(1);

        // Assert  
        var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
        Assert.Equal(404, notFoundResult.StatusCode);
    }

    [Fact]
    public async Task PostBook_ReturnsCreatedAtActionResult_WithCreatedBook()
    {
        // Arrange  
        var book = new Book { Id = 1, Title = "New Book" };
        _mockBookService.Setup(service => service.AddBookAsync(It.IsAny<Book>()))
                        .ReturnsAsync(book);

        // Act  
        var result = await _booksController.PostBook(book);

        // Assert  
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnValue = Assert.IsType<Book>(createdAtActionResult.Value);
        Assert.Equal(book.Id, returnValue.Id);
    }
}
