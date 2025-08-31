using Spectre.Console;

namespace CredentialManager.Main.Views;

public class CredentialView : IView
{
    public void Render()
    {
        AnsiConsole.Write(new Rule("[yellow italic]Credentials[/]"));
        AnsiConsole.MarkupLine("This :clapping_hands: is :clapping_hands: a :clapping_hands: test :clapping_hands: ");

        var credTable = new Table();
        credTable.AddColumn(new TableColumn("Test").Centered());
        AnsiConsole.Write(credTable);
    }
}