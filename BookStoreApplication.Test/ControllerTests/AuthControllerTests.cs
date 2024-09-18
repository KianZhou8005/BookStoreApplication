using System;
using System.Threading.Tasks;
using BookStoreApplication.Controllers;
using BookStoreApplication.Interfaces;
using BookStoreApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BookStoreApplication.Test.ControllerTests;

public class AuthControllerTests
{
    private readonly Mock<IAuthService> _mockAuthService;
    private readonly AuthController _authController;

    public AuthControllerTests()
    {
        _mockAuthService = new Mock<IAuthService>();
        _authController = new AuthController(_mockAuthService.Object);
    }

    [Fact]
    public async Task Register_ReturnsOkResult_WhenRegistrationIsSuccessful()
    {
        var user = new User { UserName = "testuser", Password = "password" };
        _mockAuthService.Setup(service => service.RegisterAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync("true");

        var result = await _authController.Register(user);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public async Task Register_ReturnsBadRequest_WhenExceptionIsThrown()
    {
        var user = new User { UserName = "testuser", Password = "password" };
        _mockAuthService.Setup(service => service.RegisterAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ThrowsAsync(new Exception("Registration failed"));

        var result = await _authController.Register(user);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, badRequestResult.StatusCode);
    }

    [Fact]
    public async Task Login_ReturnsOkResult_WithToken_WhenLoginIsSuccessful()
    {
        var user = new User { UserName = "testuser", Password = "password" };
        _mockAuthService.Setup(service => service.LoginAsync(It.IsAny<User>()))
            .ReturnsAsync("token");

        var result = await _authController.Login(user);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public async Task Login_ReturnsUnauthorized_WhenExceptionIsThrown()
    {
        var user = new User { UserName = "testuser", Password = "password" };
        _mockAuthService.Setup(service => service.LoginAsync(It.IsAny<User>()))
            .ThrowsAsync(new Exception("Login failed"));

        var result = await _authController.Login(user);

        var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
        Assert.Equal(401, unauthorizedResult.StatusCode);
    }
}

