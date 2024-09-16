using Microsoft.AspNetCore.Identity;

namespace BookStoreApplication.Models;
public class User : IdentityUser
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}
