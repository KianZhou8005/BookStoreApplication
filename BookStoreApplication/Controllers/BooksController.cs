using BookStoreApplication.Interfaces;
using BookStoreApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApplication.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    // GET: api/Books  
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
    {
        var books = await _bookService.GetAllBooksAsync();
        return Ok(books);
    }

    // GET: api/Books/5  
    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetBook(int id)
    {
        var book = await _bookService.GetBookByIdAsync(id);

        if (book == null)
        {
            return NotFound();
        }

        return Ok(book);
    }

    // POST: api/Books  
    [HttpPost]
    public async Task<ActionResult<Book>> PostBook(Book book)
    {
        var createdBook = await _bookService.AddBookAsync(book);
        return CreatedAtAction(nameof(GetBook), new { id = createdBook.Id }, createdBook);
    }

}
