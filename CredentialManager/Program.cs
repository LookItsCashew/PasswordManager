using CredentialManager.Services;
using CredentialManager.App;
using CredentialManager.App.Views;

namespace CredentialManager;

static class Program
{
    static void Main()
    {
        // explicitly set the console's encoding to UTF-8 for emoji support
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        
        var appView = new AppView();

        if (!UserService.IsUserRegistered())
        {
            AppEvents.TransitionSubViewEvent?.Invoke(new RegisterView());
        }
        else
        {
            AppEvents.TransitionSubViewEvent?.Invoke(new LoginView());
        }
        
        while (true)
        {
            if (Console.ReadKey(true).Key == ConsoleKey.Escape)
            {
                break;
            }
        }
    }
}