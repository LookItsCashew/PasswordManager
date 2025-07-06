using Spectre.Console;

namespace CredentialManager.App;

public class AppView : IView
{
    private IView _currSubView = null!;
    
    public IView CurrSubView => _currSubView;

    public void SetSubView(IView subView)
    {
        _currSubView = subView;
    }

    public void Display()
    {
        AnsiConsole.Write(
            new FigletText("Credential Manager")
                .Centered()
                .Color(Color.Blue));
        _currSubView.Display();
    }
}