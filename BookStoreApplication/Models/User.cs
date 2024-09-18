using Microsoft.AspNetCore.Identity;

namespace BookStoreApplication.Models;
public class User : IdentityUser
{
    public string Password { get; set; }
}