using CredentialManager.Services;
using Spectre.Console;

namespace CredentialManager.Application.Views;

/// <summary>
/// This view class acts as a container to maintain app's user interface state, and will only overwrite the subview's
/// display when the view changes to another.
/// </summary>
public class ApplicationView : IView
{
    public IView CurrentSubView { get; private set; } = null!;

    public ApplicationView()
    {
        ApplicationEvents.TransitionSubViewEvent += OnTransitionSubViewEvent;
        CurrentSubView = SetInitialView();
    }

    /// <summary>
    /// Destructor, called when the object gets reclaimed by GC
    /// </summary>
    ~ApplicationView()
    {
        ApplicationEvents.TransitionSubViewEvent -= OnTransitionSubViewEvent;
    }
    
    private IView SetInitialView() => UserService.IsUserRegistered() ? new LoginView() : new RegisterView();

    private void RefreshAppView()
    {
        AnsiConsole.Clear();
        Render();
    }

    private void OnTransitionSubViewEvent(IView subview)
    {
        CurrentSubView = subview;
        RefreshAppView();
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