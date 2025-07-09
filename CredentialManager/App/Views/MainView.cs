using Spectre.Console;

namespace CredentialManager.App.Views;

public class MainView : IView
{
    private enum MainMenuChoices
    {
        Credentials = 'c',
        Groups = 'g'
    }
    
    private IView SelectChoice(MainMenuChoices choice) => choice switch
    {
        MainMenuChoices.Credentials => new CredentialView(),
        MainMenuChoices.Groups => new ManageGroupsView(),
        _ => throw new ArgumentOutOfRangeException(nameof(choice), choice, null)
    };

    private MainMenuChoices ChoiceListener()
    {
        while (true)
        {
            switch (Console.ReadKey(true).KeyChar)
            {
                case (char)MainMenuChoices.Credentials:
                    return MainMenuChoices.Credentials;
                case (char)MainMenuChoices.Groups:
                    return MainMenuChoices.Groups;
            }
        }
    }
    
    public void Render()
    {
        AnsiConsole.Write(new Rule("[yellow]Main Menu[/]"));

        string menuContents = "";
        var choiceEnum = Enum.GetValues<MainMenuChoices>();
        foreach (MainMenuChoices choice in choiceEnum)
        {
            menuContents = string.Concat(menuContents, 
                "(" + Char.ToUpper((char)choice) + ")   ", 
                choice);
            menuContents += choiceEnum.Last() != choice ? "\n" : "";
        }
        
        var mainMenuPanel = new Panel(menuContents)
        {
            Expand = true,
            Border = BoxBorder.Double
        };
        
        AnsiConsole.Write(mainMenuPanel);

        var choiceSelected = ChoiceListener();

        // var selectedOption = AnsiConsole.Prompt(
        //     new SelectionPrompt<MainMenuChoices>()
        //         .AddChoices(
        //             MainMenuChoices.Credentials,
        //             MainMenuChoices.Groups));
        //
        var nextView = SelectChoice(choiceSelected);
        AppEvents.TransitionSubViewEvent?.Invoke(nextView);
    }
}