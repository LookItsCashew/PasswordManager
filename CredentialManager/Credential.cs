namespace CredentialManager;

public class Credential(string username, string password, string nickname)
{
    public string Username { get; private set; } = username;
    public string Password { get; private set; } = password;
    public string CredentialNickname { get; set; } = nickname;

    public void UpdateUsername(string newUsername)
    {
        Username = newUsername;
    }
    
    public void UpdatePassword(string newPassword)
    {
        Password = newPassword;
    }
}