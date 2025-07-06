using Spectre.Console;

namespace CredentialManager.App;

public class MainView : IView
{
    public void Display()
    {
        AnsiConsole.Write(new Rule("[yellow]Welcome![/]"));
    }
}