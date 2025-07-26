using CredentialManager.Services;
using CredentialManager.Application;
using CredentialManager.Application.Views;

namespace CredentialManager;

static class Program
{
    static void Main()
    {
        // explicitly set the console's encoding to UTF-8 for emoji support
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var app = new App();
        app.Run();
    }
}