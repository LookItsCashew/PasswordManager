using Spectre.Console;

namespace CredentialManager.App;

public class MainView : IView
{
    public void Render()
    {
        AnsiConsole.Write(new Rule("[yellow]Welcome![/]"));
    }
}