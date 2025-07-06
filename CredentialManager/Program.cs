using CredentialManager.Services;
using CredentialManager.Utils;
using CredentialManager.Database;
using CredentialManager.Models;
using CredentialManager.App;

namespace CredentialManager;

static class Program
{
    static void Main()
    {
        var appView = new AppView
        {
            ViewTitle = "Credential Manager"
        };
        
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
            appView.SetSubView(loginView);
            appView.Display();
            
            // we know the current view is a LoginView at this point, but validating anyway for sanity
            var currView = appView.CurrSubView as LoginView;  // if this fails for any reason, currView will be null
            currView?.GetUserLogin();
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