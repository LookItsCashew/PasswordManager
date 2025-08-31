namespace CredentialManager.Main.Actions;

public interface IActionListener
{
    AppAction? QueuedAction { get; protected set; }

    public void OnActionTriggered(AppAction action);

    protected void QueueAction(AppAction action);

    protected void DequeueAction(AppAction action);
}