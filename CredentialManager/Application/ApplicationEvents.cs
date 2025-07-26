using CredentialManager.Application.Views;

namespace CredentialManager.Application;

/// <summary>
/// Registry of events that view classes can subscribe to.
/// </summary>
internal static class ApplicationEvents
{
    public static Action<IView>? TransitionSubViewEvent;

    public static Action? QuitApplicationEvent;
}