using BookStoreApplication.Data;
using BookStoreApplication.Models;
using BookStoreApplication.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BookStoreApplication.Test.RepositoriesTests;

public class BookRepositoryTests
{
    private readonly BookRepository _bookRepository;
    private readonly BookStoreContext _context;

    public BookRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<BookStoreContext>()
            .UseInMemoryDatabase(databaseName: "BookStoreTestDb")
            .Options;

        _context = new BookStoreContext(options);

        // Seed data  
        if (!_context.Books.Any())
        {
            _context.Books.AddRange(
                new Book { Id = 1, Title = "Book 1" },
                new Book { Id = 2, Title = "Book 2" }
            );
            _context.SaveChanges();
        }

        _bookRepository = new BookRepository(_context);
    }

    [Fact]
    public async Task GetAllBooksAsync_ShouldReturnAllBooks()
    {
        var result = await _bookRepository.GetAllBooksAsync();
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetBookByIdAsync_ShouldReturnBook_WhenBookExists()
    {
        var result = await _bookRepository.GetBookByIdAsync(1);
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task GetBookByIdAsync_ShouldReturnNull_WhenBookDoesNotExist()
    {
        var result = await _bookRepository.GetBookByIdAsync(99);
        Assert.Null(result);
    }

    [Fact]
    public async Task AddBookAsync_ShouldAddBook()
    {
        var newBook = new Book { Id = 3, Title = "Book 3" };
        await _bookRepository.AddBookAsync(newBook);
        var result = await _context.Books.FindAsync(3);
        Assert.NotNull(result);
        Assert.Equal("Book 3", result.Title);
    }
}
