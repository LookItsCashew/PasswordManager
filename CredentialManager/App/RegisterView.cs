using CredentialManager.Services;
using CredentialManager.Models;
using Spectre.Console;

namespace CredentialManager.App;

public class RegisterView : IView
{
    private void GetUserRegistration()
    {
        while (true)
        {
            string username = AnsiConsole.Prompt(
                new TextPrompt<string>("Username:"));
            var password = AnsiConsole.Prompt(
                new TextPrompt<string>("Password:")
                    .Secret());
            var email = AnsiConsole.Prompt(
                new TextPrompt<string>("Email:"));

            if (username == "" || password == "")
            {
                AnsiConsole.Markup(
                    "[bold red]Cannot leave username or password blank :face_with_steam_from_nose: \n[/]");
                continue;
            }
            
            var es = new EncryptionService();
            var salted = es.SaltHash("htWt6583bLYT8", password);
            var hash = es.HashText(salted);

            var user = new User
            {
                Username = username,
                Password = hash,
                Email = email
            };
            
            var userService = new UserService();
            userService.Register(user);
            
            break;
        }
    }
    
    public void Render()
    {
        AnsiConsole.Write(new Rule("[yellow]Register Your Account[/]"));
        GetUserRegistration();
        AppView.TransitionSubViewEvent?.Invoke(new MainView());
    }
}