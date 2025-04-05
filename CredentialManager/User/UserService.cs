using CredentialManager.Credentials;
using CredentialManager.Encryption;

namespace CredentialManager.User;

public class UserService : IUserService
{
    private readonly string _workingDirectory = Directory.GetCurrentDirectory();

    public UserService()
    {
        var _userFile = new FileInfo(_workingDirectory + Path.DirectorySeparatorChar + "user.xml");
    }
    
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