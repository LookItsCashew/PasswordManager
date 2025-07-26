
namespace CredentialManager.Application.ViewActions;

internal class QuitApplicationAction : ViewAction
{
    public override ConsoleKey TriggerKey { get; protected set; }

    public QuitApplicationAction(ConsoleKey triggerKey) : base(triggerKey)
    {
        TriggerKey = triggerKey;
    }

    private void TriggerAction()
    {
        ApplicationEvents.QuitApplicationEvent?.Invoke();
    }
}