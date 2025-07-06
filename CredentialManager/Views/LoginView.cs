using Spectre.Console;

namespace CredentialManager.Views;

public class LoginView : View
{
    public override void Display()
    {
        var title = new Rule($"[yellow]{ViewTitle}[/]");
        AnsiConsole.Write(title);
    }
}