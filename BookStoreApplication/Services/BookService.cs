using BookStoreApplication.Interfaces;
using BookStoreApplication.Models;

namespace BookStoreApplication.Services;
public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return await _bookRepository.GetAllBooksAsync();
    }

    public async Task<Book?> GetBookByIdAsync(int id)
    {
        return await _bookRepository.GetBookByIdAsync(id);
    }

    public async Task<Book> AddBookAsync(Book book)
    {
        return await _bookRepository.AddBookAsync(book);
    }
}