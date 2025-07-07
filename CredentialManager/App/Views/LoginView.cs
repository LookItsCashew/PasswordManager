using CredentialManager.Models;
using CredentialManager.Services;
using Spectre.Console;

namespace CredentialManager.App.Views;

public class LoginView : IView
{
    private void GetUserLogin()
    {
        while (true)
        {
            // get the user's login input
            var username = AnsiConsole.Prompt(
                new TextPrompt<string>("Username:"));
            var password = AnsiConsole.Prompt(
                new TextPrompt<string>("Password:")
                    .Secret());

            var es = new EncryptionService();
            var salted = es.SaltHash("htWt6583bLYT8", password);
            var hash = es.HashText(salted);

            var user = new User
            {
                Username = username,
                Password = hash
            };

            var userService = new UserService();
            if (!userService.CheckLogin(user))
            {
                AnsiConsole.Markup(
                    ":no_entry: [bold red] Invalid username or password [/] :no_entry: \n");
                continue;
            }

            break;
        }
    }

    public void Render()
    {
        AnsiConsole.Write(new Rule("[yellow]Please Login[/]"));
        GetUserLogin();
        AppView.TransitionSubViewEvent?.Invoke(new MainView());
    }
}