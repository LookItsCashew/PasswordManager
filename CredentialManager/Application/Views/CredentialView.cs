using Spectre.Console;

namespace CredentialManager.Application.Views;

public class CredentialView : IView
{
    public void PollForActions()
    {
        throw new NotImplementedException();
    }

    public void Render()
    {
        AnsiConsole.Write(new Rule("[yellow italic]Credentials[/]"));
        AnsiConsole.MarkupLine("This :clapping_hands: is :clapping_hands: a :clapping_hands: test :clapping_hands: ");
    }
}