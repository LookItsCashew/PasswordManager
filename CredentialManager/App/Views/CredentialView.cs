using Spectre.Console;

namespace CredentialManager.App.Views;

public class CredentialView : IView
{
    public void Render()
    {
        AnsiConsole.Write(new Rule("[yellow]Credentials[/]"));
        AnsiConsole.MarkupLine("This :clapping_hands: is :clapping_hands: a :clapping_hands: test :clapping_hands: ");
    }
}