using BookStoreApplication.Interfaces;
using BookStoreApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApplication.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ShoppingCartController : Controller
{

    private readonly IShoppingCartService _cartService;
    private readonly UserManager<User> _userManager;

    public ShoppingCartController(IShoppingCartService cartService, UserManager<User> userManager)
    {
        _cartService = cartService;
        _userManager = userManager;
    }

    [HttpPost("{bookId}")]
    public async Task<IActionResult> AddToCart(int bookId)
    {
        var userId = _userManager.GetUserId(User);
        var cart = await _cartService.AddToCartAsync(userId, bookId);
        return Ok(cart);
    }

    [HttpGet]
    public async Task<IActionResult> GetCart()
    {
        var userId = _userManager.GetUserId(User);
        var cart = await _cartService.GetCartAsync(userId);
        if (cart == null)
        {
            return NotFound();
        }

        return Ok(cart);
    }

    [HttpGet("checkout")]
    public async Task<IActionResult> Checkout()
    {
        var userId = _userManager.GetUserId(User);
        var totalPrice = await _cartService.CheckoutAsync(userId);
        return Ok(new { TotalPrice = totalPrice });
    }
}
