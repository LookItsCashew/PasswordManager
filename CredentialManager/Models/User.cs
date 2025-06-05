using SQLite;

namespace CredentialManager.Models;

[Table("User")]
public class User
{
    public string Username  { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}