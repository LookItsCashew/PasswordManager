using CredentialManager.Main.Views;
using Spectre.Console;

namespace CredentialManager.Main;

public class App
{
    AppView _appView = new AppView();

    bool _run = true;

    private App()
    {
        AppEvents.QuitAppEvent += OnQuitEvent;
        AnsiConsole.Clear();
    }

    ~App()
    {
        AppEvents.QuitAppEvent -= OnQuitEvent;
    }

    private void OnQuitEvent()
    {
        _run = false;
        AnsiConsole.MarkupLine("[bold green]Goodbye 👋 🥲[/]");
        Thread.Sleep(750);
    }

    public static App CreateApp() => new App();

    public void Run()
    {
        _appView.Render();
        while (_run)
        {
            if (Console.ReadKey(true).Key == ConsoleKey.Q)
            {
                // Testing this only, will be moved to ViewAction sub-class 
                // for triggering a quit event
                AppEvents.QuitAppEvent?.Invoke();
            }
        }
    }
}