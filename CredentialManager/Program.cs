using CredentialManager.Services;
using CredentialManager.Utils;
using CredentialManager.Database;
using CredentialManager.Models;

namespace CredentialManager;

static class Program
{
    static void Main()
    {
        UserService us = new UserService();
        User user;

        if (!ConnectionManager.DatabaseExists)
        {
            Console.WriteLine("Welcome to your new Credential Manager! Please register below.\n");
            user = us.Registration();
            Console.Clear();
            us.Register(user);
        }
        else
        {
            user = us.CheckLogin();
            Console.Clear();
        }
        Console.WriteLine($"Hello, {user.Username}!");
    }
}