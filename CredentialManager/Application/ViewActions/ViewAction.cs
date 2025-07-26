using Spectre.Console;
using Spectre.Console.Rendering;

namespace CredentialManager.Application.ViewActions;

public abstract class ViewAction
{
    public abstract ConsoleKey TriggerKey { get; protected set; }

    public Action ViewActionTriggered;

    protected ViewAction(ConsoleKey triggerKey)
    {
        TriggerKey = triggerKey;
    }
}