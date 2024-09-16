using BookStoreApplication.Data;
using BookStoreApplication.Interfaces;
using BookStoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApplication.Repositories;

public class BookRepository : IBookRepository
{
    private readonly BookStoreContext _context;

    public BookRepository(BookStoreContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return await _context.Books.ToListAsync();
    }

    public async Task<Book?> GetBookByIdAsync(int id)
    {
        return await _context.Books.FindAsync(id);
    }

    public async Task<Book> AddBookAsync(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return book;
    }
}