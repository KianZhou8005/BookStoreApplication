using BookStoreApplication.Models;

namespace BookStoreApplication.Interfaces;

public interface IShoppingCartService
{
    Task<ShoppingCart> AddToCartAsync(string userId, int bookId);
    Task<ShoppingCart?> GetCartAsync(string userId);
    Task<decimal> CheckoutAsync(string userId);
}