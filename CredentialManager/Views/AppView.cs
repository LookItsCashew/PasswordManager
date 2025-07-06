using Spectre.Console;

namespace CredentialManager.Views;

public class AppView : View
{
    private View _currSubView = null!;

    public void SetSubView(View subView)
    {
        _currSubView = subView;
    }

    public override void Display()
    {
        AnsiConsole.Write(
            new FigletText("Credential Manager")
                .Centered()
                .Color(Color.Blue));
        _currSubView.Display();
    }
}