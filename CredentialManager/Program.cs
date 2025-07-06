using CredentialManager.Services;
using CredentialManager.Utils;
using CredentialManager.Database;
using CredentialManager.Models;
using CredentialManager.Views;
using Spectre.Console;

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
            var loginView = new LoginView
            {
                ViewTitle = "Please Login"
            };
            loginView.Display();
            user = us.CheckLogin();
        }

        while (true)
        {
            if (Console.ReadKey(true).Key == ConsoleKey.Q)
            {
                break;
            }
        }
    }
}