using Spectre.Console;

namespace CredentialManager.App;

public class MainView : IView
{
    public void Render()
    {
        AnsiConsole.Write(new Rule("[yellow]Welcome![/]"));
        AnsiConsole.MarkupLine("This :clapping_hands: is :clapping_hands: a :clapping_hands: test :clapping_hands: ");
    }
}