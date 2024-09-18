using BookStoreApplication.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BookStoreApplication.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace BookStoreApplication.Test.IntegrationTests;

public class IntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;

    public IntegrationTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Register_ReturnsOk()
    {
        // Arrange  
        var user = new User
        {
            Id = "111",
            UserName = "testuser1",
            Password = "Test@123"
        };

        // Act  
        var response = await _client.PostAsJsonAsync("/api/Auth/register", user);

        // Assert  
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadAsStringAsync();
        Assert.Equal("User registered successfully", result);
    }

    [Fact]
    public async Task Login_ReturnsToken()
    {
        // Arrange  
        var user = new User
        {
            Id = "222",
            UserName = "testuser2",
            Password = "Test@123"
        };

        // Register the user first  
        var registerResponse = await _client.PostAsJsonAsync("/api/Auth/register", user);
        registerResponse.EnsureSuccessStatusCode();
        
        // Act  
        var response = await _client.PostAsJsonAsync("/api/Auth/login", user);

        // Assert  
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
        Assert.True(result.ContainsKey("token"));
    }

    [Fact]
    public async Task GetBooks_ReturnsBooks()
    {
        // Act  
        var response = await _client.GetAsync("/api/books");

        // Assert  
        response.EnsureSuccessStatusCode();
        var books = await response.Content.ReadFromJsonAsync<IEnumerable<Book>>();
        Assert.NotNull(books);
    }

    [Fact]
    public async Task AddBook_ReturnsCreatedBook()
    {
        // Arrange  
        var book = new Book
        {
            Title = "Test Book",
            Author = "Test Author",
            Price = 9.99m
        };

        // Act  
        var response = await _client.PostAsJsonAsync("/api/books", book);

        // Assert  
        response.EnsureSuccessStatusCode();
        var createdBook = await response.Content.ReadFromJsonAsync<Book>();
        Assert.NotNull(createdBook);
        Assert.Equal(book.Title, createdBook.Title);
    }
}
