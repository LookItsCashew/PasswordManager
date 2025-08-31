namespace CredentialManager.Main.Actions;

public abstract class AppAction
{
    public abstract ConsoleKey TriggerKey { get; init; }

    public Action? AppActionTriggered;

    public abstract void TriggerAction();
}