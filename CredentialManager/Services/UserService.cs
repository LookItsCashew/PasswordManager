using System.Text;
using CredentialManager.Models;
using CredentialManager.Database;
using CredentialManager.Utils;
using CredentialManager.Database.Repositories;

namespace CredentialManager.Services;

public class UserService
{
    private readonly UserRepository _repo = new();

    public bool IsUserRegistered()
    {
        // if second 'if' expression is evaluated, then ReadAll should NOT have returned null
        return _repo.ReadAll() is not null && _repo.ReadAll().Count > 0;
    }
    
    public bool CheckLogin(User user) => LogIn(user);

    bool LogIn(User user)
    {
        var results = _repo.ReadAll();
        if (results != null && results.Count > 0)
        {
            return results.First().Username == user.Username && 
                results.First().Password == user.Password;
        }

        return false;
    }

    public void Register(User user)
    {
        // TODO: validate email address
        _repo.Create(user);
    }
}