using BookStoreApplication.Interfaces;
using BookStoreApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApplication.Controllers;
public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User user)
    {
        try
        {
            var result = await _authService.RegisterAsync(user, user.Password);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] User user)
    {
        try
        {
            var token = await _authService.LoginAsync(user);
            return Ok(new { Token = token });
        }
        catch (Exception ex)
        {
            return Unauthorized(ex.Message);
        }
    }
}
