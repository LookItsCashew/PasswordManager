using Spectre.Console;

namespace CredentialManager.App.Views;

public class ManageGroupsView : IView
{
    public void Render()
    {
        AnsiConsole.Write(new Rule("[yellow]Manage Credential Groups[/]"));
    }
}