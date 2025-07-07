using CredentialManager.Services;
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
            appView.CurrentSubView =  new RegisterView();
        }
        else
        {
            appView.CurrentSubView = new LoginView();
        }

        appView.RefreshAppViewEvent?.Invoke();
        while (true)
        {
            if (Console.ReadKey(true).Key == ConsoleKey.Q)
            {
                break;
            }
        }
    }
}