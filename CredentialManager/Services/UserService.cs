using CredentialManager.Models;
using CredentialManager.Database;

namespace CredentialManager.Services;

public class UserService
{
    public UserService()
    {
        
    }
    
    public User Registration()
    {
        Console.Write("Username: ");
        var username = Console.ReadLine();
        
        Console.Write("Password: ");
        var password = Console.ReadLine();
        
        Console.Write("Email: ");
        var email = Console.ReadLine();

        if (username == null || password == null)
        {
            Console.WriteLine("Please enter a username and password to register.");
            Registration();
        }
        
        return new User { Username = username!.Trim(), Password = password!.Trim(), Email = email.Trim() };
    }
    
    public User CheckLogin()
    {
        UserService us = new UserService();
        Console.Write("Username: ");
        var username = Console.ReadLine();
            
        Console.Write("Password: ");
        var password = Console.ReadLine();
        
        var user = new User
        {
            Username = username != null ? username : "",
            Password = password != null ? password : ""
        };

        if (!us.LogIn(user))
        {
            Console.WriteLine("Invalid username or password.");
            CheckLogin();
        }

        return user;
    }

    public bool LogIn(User user)
    {
        var conn = ConnectionManager.GetDatabaseConnection();
        try
        { 
            var results = conn.Query<User>($"SELECT * FROM User WHERE Username = '{user.Username}'");
            if (results.Any())
            {
                return results.First().Password == user.Password && results.First().Username == user.Username;
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