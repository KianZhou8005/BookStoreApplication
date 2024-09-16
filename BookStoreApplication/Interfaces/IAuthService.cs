using BookStoreApplication.Models;

namespace BookStoreApplication.Interfaces;

public interface IAuthService
{
    Task<string> RegisterAsync(User user, string password);
    Task<string> LoginAsync(User user);
}