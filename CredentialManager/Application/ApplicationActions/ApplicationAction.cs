namespace CredentialManager.Application.ApplicationActions;

public abstract class ApplicationAction
{
    public abstract ConsoleKey TriggerKey { get; protected set; }

    public Action ApplicationActionTriggered;

    protected ApplicationAction(ConsoleKey triggerKey)
    {
        TriggerKey = triggerKey;
    }
}