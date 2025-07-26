using Spectre.Console;

namespace CredentialManager.Application.Views;

public class ManageGroupsView : IView
{
    public void Render()
    {
        AnsiConsole.Write(new Rule("[yellow]Manage Credential Groups[/]"));
    }
}