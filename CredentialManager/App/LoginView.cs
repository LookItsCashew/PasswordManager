using Spectre.Console;

namespace CredentialManager.App;

public class LoginView : View
{
    public override void Display()
    {
        var title = new Rule($"[yellow]{ViewTitle}[/]");
        AnsiConsole.Write(title);
    }
}