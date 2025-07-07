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
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        var appView = new AppView();

        if (!UserService.IsUserRegistered())
        {
            appView.SetSubView(new RegisterView());
        }
        else
        {
            appView.SetSubView(new LoginView());
        }

        appView.Display();
        while (true)
        {
            if (Console.ReadKey(true).Key == ConsoleKey.Q)
            {
                break;
            }
        }
    }
}