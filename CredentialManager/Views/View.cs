namespace CredentialManager.Views;

public abstract class View
{
    public required string ViewTitle { get; init; }

    public abstract void Display();
}