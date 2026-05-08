using Spectre.Console;

namespace CredentialManager.Main.Views;

public class MainView : IView
{
    private enum MainMenuChoices
    {
        Credentials,
        Groups,
        Quit
    }
    
    private IView SelectChoice(MainMenuChoices choice) => choice switch
    {
        // Using switch to easily accommodate 
        MainMenuChoices.Credentials => new CredentialView(),
        MainMenuChoices.Groups => new ManageGroupsView(),
        _ => throw new ArgumentOutOfRangeException(nameof(choice), choice, null)
    };

    public void Render()
    {
        AnsiConsole.Write(new Rule("[yellow italic]Main Menu[/]"));

        var selectedOption = AnsiConsole.Prompt(
            new SelectionPrompt<MainMenuChoices>()
                .AddChoices(
                    MainMenuChoices.Credentials,
                    MainMenuChoices.Groups,
                    MainMenuChoices.Quit));

        if (selectedOption == MainMenuChoices.Quit)
        {
            AppEvents.QuitAppEvent?.Invoke();
        }
        else
        {
            IView nextView;
            try
            {
                nextView = SelectChoice(selectedOption);
            }
            catch (ArgumentOutOfRangeException)
            {
                AnsiConsole.MarkupLine("⚠️ [red] Somehow an invalid choice was made[/] ⚠️");
                Thread.Sleep(1000);
                nextView = new MainView();
            }
            AppEvents.TransitionSubViewEvent?.Invoke(nextView);
        }
    }
}