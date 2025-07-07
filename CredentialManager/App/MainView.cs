using Spectre.Console;

namespace CredentialManager.App;

public class MainView : IView
{
    private enum MainMenuChoices
    {
        Credentials,
        Groups
    }
    private IView SelectChoice(MainMenuChoices choice) => choice switch
    {
        MainMenuChoices.Credentials => new CredentialView(),
        MainMenuChoices.Groups => new ManageGroupsView(),
        _ => throw new ArgumentOutOfRangeException(nameof(choice), choice, null)
    };
    
    public void Render()
    {
        AnsiConsole.Write(new Rule("[yellow]Main Menu[/]"));

        var selectedOption = AnsiConsole.Prompt(
            new SelectionPrompt<MainMenuChoices>()
                .AddChoices(
                    MainMenuChoices.Credentials,
                    MainMenuChoices.Groups));
        
        var nextView = SelectChoice(selectedOption);
        AppView.TransitionSubViewEvent?.Invoke(nextView);
    }
}