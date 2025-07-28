using CredentialManager.Application.Views;
using Spectre.Console;

namespace CredentialManager.Application;

public class App
{
    private ApplicationView _appView = new ApplicationView();

    private bool _run = true;

    public App()
    {
        ApplicationEvents.QuitApplicationEvent += OnQuitEvent;
        AnsiConsole.Clear();
    }

    ~App()
    {
        ApplicationEvents.QuitApplicationEvent -= OnQuitEvent;
    }

    private void OnQuitEvent()
    {
        _run = false;
        AnsiConsole.MarkupLine("[bold green]Goodbye 👋 🥲[/]");
        Thread.Sleep(750);
    }

    public void Run()
    {
        _appView.Render();
        while (_run)
        {
            _appView.PollForActions();
            if (Console.ReadKey(true).Key == ConsoleKey.Escape)
            {
                // Testing this only, will be moved to ViewAction sub-class 
                // for triggering a quit event
                ApplicationEvents.QuitApplicationEvent?.Invoke();
            }
        }
    }
}