using System.Text;
using CredentialManager.Models;
using CredentialManager.Database;
using CredentialManager.Utils;

namespace CredentialManager.Services;

public class UserService
{
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

        var es = new EncryptionService();
        var salted = es.SaltHash("htWt6583bLYT8", password);
        var hash = es.HashText(salted);
        
        return new User { Username = username!.Trim(), Password = hash!.Trim(), Email = email.Trim() };
    }
    
    public User CheckLogin()
    {
        UserService us = new UserService();
        Console.Write("Username: ");
        var username = Console.ReadLine();
            
        Console.Write("Password: ");
        var password = Console.ReadLine();
        
        var es = new EncryptionService();
        var salted = es.SaltHash("htWt6583bLYT8", password);
        var hash = es.HashText(salted);
        
        var user = new User
        {
            Username = username != null ? username : "",
            Password = hash != null ? hash : ""
        };

        if (!us.LogIn(user))
        {
            Console.WriteLine("Invalid username or password.");
            CheckLogin();
        }

        return user;
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
            // hash & salt password before saving to database
            var es = new EncryptionService(Global.Keys.GetKeyById("0"));
            
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