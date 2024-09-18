using System.Threading.Tasks;
using BookStoreApplication.Controllers;
using BookStoreApplication.Interfaces;
using BookStoreApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BookStoreApplication.Test.ControllerTests;

public class ShoppingCartControllerTests
{
    private readonly Mock<IShoppingCartService> _mockCartService;
    private readonly Mock<UserManager<User>> _mockUserManager;
    private readonly ShoppingCartController _shoppingCartController;

    public ShoppingCartControllerTests()
    {
        _mockCartService = new Mock<IShoppingCartService>();
        _mockUserManager = new Mock<UserManager<User>>(
            new Mock<IUserStore<User>>().Object,
            null, null, null, null, null, null, null, null
        );
        _shoppingCartController = new ShoppingCartController(_mockCartService.Object, _mockUserManager.Object);
    }

    [Fact]
    public async Task AddToCart_ReturnsOkResult_WithCart()
    {
        var userId = "123";
        var cart = new ShoppingCart();
        _mockUserManager.Setup(manager => manager.GetUserId(It.IsAny<System.Security.Claims.ClaimsPrincipal>()))
                        .Returns(userId);
        _mockCartService.Setup(service => service.AddToCartAsync(It.IsAny<string>(), It.IsAny<int>()))
                        .ReturnsAsync(cart);

        var result = await _shoppingCartController.AddToCart(1);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public async Task GetCart_ReturnsOkResult_WithCart()
    {
        var userId = "123";
        var cart = new ShoppingCart();
        _mockUserManager.Setup(manager => manager.GetUserId(It.IsAny<System.Security.Claims.ClaimsPrincipal>()))
                        .Returns(userId);
        _mockCartService.Setup(service => service.GetCartAsync(It.IsAny<string>()))
                        .ReturnsAsync(cart);

        var result = await _shoppingCartController.GetCart();

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public async Task GetCart_ReturnsNotFound_WhenCartIsNull()
    {
        var userId = "123";
        _mockUserManager.Setup(manager => manager.GetUserId(It.IsAny<System.Security.Claims.ClaimsPrincipal>()))
                        .Returns(userId);
        _mockCartService.Setup(service => service.GetCartAsync(It.IsAny<string>()))
                        .ReturnsAsync((ShoppingCart)null);

        var result = await _shoppingCartController.GetCart();

        var notFoundResult = Assert.IsType<NotFoundResult>(result);
        Assert.Equal(404, notFoundResult.StatusCode);
    }

    [Fact]
    public async Task Checkout_ReturnsOkResult_WithTotalPrice()
    {
        var userId = "123";
        var totalPrice = 100m;
        _mockUserManager.Setup(manager => manager.GetUserId(It.IsAny<System.Security.Claims.ClaimsPrincipal>()))
                        .Returns(userId);
        _mockCartService.Setup(service => service.CheckoutAsync(It.IsAny<string>()))
                        .ReturnsAsync(totalPrice);

        var result = await _shoppingCartController.Checkout();

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }
}
