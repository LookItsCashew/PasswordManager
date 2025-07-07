using Spectre.Console;

namespace CredentialManager.App;

/// <summary>
/// This view class acts as a container to maintain app's user interface state, and will only overwrite the subview's
/// display when the view changes to another.
/// </summary>
public class AppView : IView
{
    public Action? RefreshAppViewEvent;

    public IView CurrentSubView { get; set; } = null!;

    public AppView()
    {
        RefreshAppViewEvent += OnRefreshAppViewEvent;
    }

    private void OnRefreshAppViewEvent()
    {
        AnsiConsole.Clear();
        Render();
    }

    public void Render()
    {
        AnsiConsole.Write(
            new FigletText("Credential Manager")
                .Centered()
                .Color(Color.Blue));
        CurrentSubView.Render();
    }
}