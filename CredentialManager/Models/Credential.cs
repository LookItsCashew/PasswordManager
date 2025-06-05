using SQLite;

namespace CredentialManager.Models;

[Table("Credentials")]
public class Credential
{
    public int CredentialId { get; set; }
    public int GroupId  { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Nickname { get; set; }
}