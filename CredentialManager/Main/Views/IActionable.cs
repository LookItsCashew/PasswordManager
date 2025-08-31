using CredentialManager.Main.Actions;

namespace CredentialManager.Main.Views;

public interface IActionable
{
    List<AppAction> Actions { get; protected set; }
    
    public void SetAppActions(List<AppAction> actions);
}