using System.Text;
using CredentialManager.Models;
using CredentialManager.Database;
using CredentialManager.Utils;

namespace CredentialManager.Services;

public class UserService
{
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
    
    public bool CheckLogin(User user)
    {
        return LogIn(user);
    }

    private bool LogIn(User user)
    {
        var conn = ConnectionManager.GetDatabaseConnection();
        try
        { 
            var results = conn.Query<User>($"SELECT * FROM User WHERE Username = '{user.Username}'");
            if (results.Any())
            {
                return results.First().Password == user.Password && 
                       results.First().Username == user.Username;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            conn.Close();
        }
        return false;
    }

    public void Register(User user)
    {
        var conn = ConnectionManager.GetDatabaseConnection();
        try
        {
            conn.Insert(user);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            conn.Close();
        }
    }
}