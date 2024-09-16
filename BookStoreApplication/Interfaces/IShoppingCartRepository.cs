using BookStoreApplication.Models;

namespace BookStoreApplication.Interfaces;

public interface IShoppingCartRepository
{
    Task<ShoppingCart?> GetCartByUserIdAsync(string userId);
    Task<ShoppingCart> UpdateCartAsync(ShoppingCart cart);
}