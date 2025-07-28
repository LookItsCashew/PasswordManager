
namespace CredentialManager.Application.ApplicationActions;

internal class QuitApplicationAction : ApplicationAction
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