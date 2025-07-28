using Spectre.Console;

namespace CredentialManager.Application.Views;

public class ManageGroupsView : IView
{
    public void PollForActions()
    {
        throw new NotImplementedException();
    }

    public void Render()
    {
        AnsiConsole.Write(new Rule("[yellow]Manage Credential Groups[/]"));
    }
}