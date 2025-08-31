using System.Text;
using CredentialManager.Models;
using CredentialManager.Database;
using CredentialManager.Utils;
using CredentialManager.Database.Repositories;

namespace CredentialManager.Services;

public class UserService
{
    UserRepository _repo = new UserRepository();

    public static bool IsUserRegistered()
    {
        var conn = ConnectionManager.GetDatabaseConnection();
        try
        {
            var results = conn.Query<User>("SELECT * FROM User");
            return results.Count != 0;
        }
        catch (SQLite.SQLiteException e)
        {
            Console.Error.WriteLine(e.Message);
        }
        finally
        {
            conn.Close();
        }

        return false;
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