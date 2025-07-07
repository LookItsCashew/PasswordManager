using CredentialManager.App.Views;

namespace CredentialManager.App;

/// <summary>
/// Registry of events that view classes can subscribe to.
/// </summary>
internal static class AppEvents
{
    public static Action<IView>? TransitionSubViewEvent;
}