namespace CredentialManager.Credentials;

public class Credential
{
    public string? Username { get; private set; }
    public string? Password { get; private set; }
    public string? Nickname { get; private set; }

    public Credential()
    {
        
    }

    public Credential(string username, string password, string nickname)
    {
        Username = username;
        Password = password;
        Nickname = nickname;
    }

    public void UpdateUsername(string newUsername) => Username = newUsername;
    
    public void UpdatePassword(string newPassword) => Password = newPassword;
    
    public void UpdateNickname(string newNickname) => Nickname = newNickname;
}