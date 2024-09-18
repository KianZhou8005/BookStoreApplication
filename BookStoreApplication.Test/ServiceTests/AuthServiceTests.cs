using BookStoreApplication.Interfaces;
using BookStoreApplication.Models;
using BookStoreApplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;

namespace BookStoreApplication.Test.ServiceTests;

public class AuthServiceTests
{
    private readonly Mock<UserManager<User>> _userManagerMock;
    private readonly Mock<SignInManager<User>> _signInManagerMock;
    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        _userManagerMock = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
        _signInManagerMock = new Mock<SignInManager<User>>(_userManagerMock.Object, Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<User>>(), null, null, null, null);

        var jwtSettings = Options.Create(new JwtSettings { IssuerSigningKey = "YourSecretKey", ValidIssuer = "issuer", ValidAudience = "audience" });
        _authService = new AuthService(_userManagerMock.Object, _signInManagerMock.Object, jwtSettings);
    }

    [Fact]
    public async Task RegisterAsync_ShouldReturnSuccessMessage_WhenUserIsCreated()
    {
        var user = new User { UserName = "testuser" };
        _userManagerMock.Setup(x => x.CreateAsync(user, "Password123")).ReturnsAsync(IdentityResult.Success);

        var result = await _authService.RegisterAsync(user, "Password123");

        Assert.Equal("User registered successfully", result);
    }

    [Fact]
    public async Task LoginAsync_ShouldThrowException_WhenLoginFails()
    {
        var user = new User { UserName = "testuser", Password = "wrongpassword" };
        _signInManagerMock.Setup(x => x.PasswordSignInAsync(user.UserName, user.Password, false, false)).ReturnsAsync(SignInResult.Failed);

        await Assert.ThrowsAsync<Exception>(() => _authService.LoginAsync(user));
    }
}