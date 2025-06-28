namespace CredentialManager.Views;

public abstract class View
{
    public required string ViewTitle { get; set; }

    public abstract void Display();

    public abstract void Clear();
}