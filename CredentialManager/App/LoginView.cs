using CredentialManager.Models;
using CredentialManager.Services;
using Spectre.Console;

namespace CredentialManager.App;

public class LoginView : View
{
    public void GetUserLogin()
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

        var user = new User()
        {
            Username = username,
            Password = hash
        };
        
        var userService = new UserService();
        if (!userService.CheckLogin(user))
        {
            AnsiConsole.Write(
                new Markup("[bold red]Incorrect username or password. Please try again.\n[/]"));
            GetUserLogin();
        }
    }
    
    public override void Display()
    {
        var title = new Rule($"[yellow]{ViewTitle}[/]");
        AnsiConsole.Write(title);
    }
}