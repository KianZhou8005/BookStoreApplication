using BookStoreApplication.Interfaces;
using BookStoreApplication.Models;

namespace BookStoreApplication.Services;

public class ShoppingCartService : IShoppingCartService
{
    private readonly IShoppingCartRepository _cartRepository;
    private readonly IBookRepository _bookRepository;

    public ShoppingCartService(IShoppingCartRepository cartRepository, IBookRepository bookRepository)
    {
        _cartRepository = cartRepository;
        _bookRepository = bookRepository;
    }

    public async Task<ShoppingCart> AddToCartAsync(string userId, int bookId)
    {
        var book = await _bookRepository.GetBookByIdAsync(bookId) ?? throw new Exception("Book not found");
        var cart = await _cartRepository.GetCartByUserIdAsync(userId);
        cart ??= new ShoppingCart { UserId = userId };

        cart.Books.Add(book);
        return await _cartRepository.UpdateCartAsync(cart);
    }

    public async Task<ShoppingCart?> GetCartAsync(string userId)
    {
        return await _cartRepository.GetCartByUserIdAsync(userId);
    }

    public async Task<decimal> CheckoutAsync(string userId)
    {
        var cart = await _cartRepository.GetCartByUserIdAsync(userId) ?? throw new Exception("Cart not found");
        var totalPrice = cart.Books.Sum(book => book.Price);
        return totalPrice;
    }
}