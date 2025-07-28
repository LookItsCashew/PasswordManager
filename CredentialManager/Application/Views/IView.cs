namespace CredentialManager.Application.Views;

public interface IView
{
    public void PollForActions();

    public void Render();
}