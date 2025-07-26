using CredentialManager.Application.Views;

namespace CredentialManager.Application;

public class App
{
    private ApplicationView _appView = new ApplicationView();

    private bool _run = true;
    
    public App()
    {
        ApplicationEvents.QuitApplicationEvent += OnQuitEvent;
    }

    ~App()
    {
        ApplicationEvents.QuitApplicationEvent -= OnQuitEvent;
    }

    private void OnQuitEvent()
    {
        _run = false;
    }

    public void Run()
    {
        _appView.Render();
        while (_run)
        {
            if (Console.ReadKey(true).Key == ConsoleKey.Escape)
            {
                // Testing this only, will be moved to ViewAction sub-class 
                // for triggering a quit event
                ApplicationEvents.QuitApplicationEvent?.Invoke();
            }
        }
    }
}