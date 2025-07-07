using Spectre.Console;

namespace CredentialManager.App;

/// <summary>
/// This view class acts as a container to maintain app's user interface state, and will only overwrite the subview's
/// display when the view changes to another.
/// </summary>
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
        AnsiConsole.Clear();
        AnsiConsole.Write(
            new FigletText("Credential Manager")
                .Centered()
                .Color(Color.Blue));
        _currSubView.Display();
    }
}