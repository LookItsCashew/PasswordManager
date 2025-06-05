using CredentialManager.Services;
using CredentialManager.Utils;
using CredentialManager.Database;
using CredentialManager.Models;

namespace CredentialManager;

static class Program
{
    static User Registration()
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

    static User CheckLogin()
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

    static void MainLoop()
    {
        while (true)
        {
            
        }
    }
    
    static void Main()
    {
        UserService us = new UserService();
        User user;

        if (!ConnectionManager.DatabaseExists)
        {
            Console.WriteLine("Welcome to your new Credential Manager! Please register below.\n");
            user = Registration();
            Console.Clear();
            us.Register(user);
        }
        else
        {
            user = CheckLogin();
            Console.Clear();
        }
        Console.WriteLine($"Hello, {user.Username}!");
        //MainLoop();
    }
}