using CredentialManager.Main.Views;

namespace CredentialManager.Main;

/// <summary>
/// Registry of events that view classes can subscribe to.
/// </summary>
internal static class AppEvents
{
    public static Action<IView>? TransitionSubViewEvent;

    public static Action? QuitAppEvent;
}