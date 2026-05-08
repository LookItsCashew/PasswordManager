
namespace CredentialManager.Main.Actions;

internal class QuitAppAction : AppAction
{
    public override ConsoleKey TriggerKey { get; init; }

    public QuitAppAction(ConsoleKey triggerKey)
    {
        TriggerKey = triggerKey;
    }

    public override void TriggerAction()
    {
        AppEvents.QuitAppEvent?.Invoke();
    }
}