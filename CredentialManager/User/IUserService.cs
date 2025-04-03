using CredentialManager.Credentials;

namespace CredentialManager.User;

public interface IUserService
{
    public Credential CreateNewCredential(string username, string password, string nickname);
    public void DeleteCredential(Credential credential);
    public bool LogIn(string username, string password);
    public bool Register(string username, string password);
    public void UpdateCredential(Credential credential, string newUsername = "", string newPassword = "");
}