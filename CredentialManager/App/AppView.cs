using Spectre.Console;

namespace CredentialManager.App;

/// <summary>
/// This view class acts as a container to maintain app's user interface state, and will only overwrite the subview's
/// display when the view changes to another.
/// </summary>
public class AppView : IView
{
    public Action? RefreshAppViewEvent;
    
    public static Action<IView>? TransitionSubViewEvent;

    public IView CurrentSubView { get; set; } = null!;

    public AppView()
    {
        RefreshAppViewEvent += OnRefreshAppViewEvent;
        TransitionSubViewEvent += OnTransitionSubViewEvent;
    }

    ~AppView()
    {
        RefreshAppViewEvent -= OnRefreshAppViewEvent;
        TransitionSubViewEvent -= OnTransitionSubViewEvent;
    }

    private void OnRefreshAppViewEvent()
    {
        AnsiConsole.Clear();
        Render();
    }

    private void OnTransitionSubViewEvent(IView subview)
    {
        CurrentSubView = subview;
        RefreshAppViewEvent?.Invoke();
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