using Spectre.Console;

namespace CredentialManager.App;

public class AppView : View
{
    private View _currSubView = null!;
    
    public View CurrSubView => _currSubView;

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