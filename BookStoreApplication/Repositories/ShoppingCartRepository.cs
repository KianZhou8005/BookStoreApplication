using BookStoreApplication.Data;
using BookStoreApplication.Interfaces;
using BookStoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApplication.Repositories;

public class ShoppingCartRepository : IShoppingCartRepository
{
    private readonly BookStoreContext _context;

    public ShoppingCartRepository(BookStoreContext context)
    {
        _context = context;
    }

    public async Task<ShoppingCart?> GetCartByUserIdAsync(string userId)
    {
        return await _context.ShoppingCarts.Include(c => c.Books)
            .FirstOrDefaultAsync(c => c.UserId == userId);
    }

    public async Task<ShoppingCart> UpdateCartAsync(ShoppingCart cart)
    {
        _context.ShoppingCarts.Update(cart);
        await _context.SaveChangesAsync();
        return cart;
    }
}
