using CredentialManager.Credentials;

namespace CredentialManager.User;

public class UserService : IUserService
{
    public Credential CreateNewCredential(string username, string password, string nickname)
    {
        throw new NotImplementedException();
    }

    public void DeleteCredential(Credential credential)
    {
        throw new NotImplementedException();
    }

    public bool LogIn(string username, string password)
    {
        throw new NotImplementedException();
    }

    public bool Register(string username, string password)
    {
        throw new NotImplementedException();
    }

    public void UpdateCredential(Credential credential, string newUsername = "", string newPassword = "")
    {
        throw new NotImplementedException();
    }
}