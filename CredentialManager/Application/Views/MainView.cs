using Spectre.Console;

namespace CredentialManager.Application.Views;

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

        public void PollForActions()
    {
        throw new NotImplementedException();
    }

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
            ApplicationEvents.QuitApplicationEvent?.Invoke();
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
            ApplicationEvents.TransitionSubViewEvent?.Invoke(nextView);
        }
    }
}