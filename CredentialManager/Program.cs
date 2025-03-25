using System.Security.Cryptography;
using CredentialManager.XML;

namespace CredentialManager;

class Program
{
    static void Main()
    {
        Console.WriteLine("Hello, World!");
        var cred = new Credential("test", "what the heck", "Test Credential");
        var xml = new CredentialXMLHandler();
        if (xml.AddCredential(cred))
        {
            Console.WriteLine("Successfully Added Credential");
        }
        else
        {
            Console.WriteLine("Failed to Add Credential");
        }

        var cont = true;
        while(cont)
        {
            var keyInfo = Console.ReadKey();
            switch (keyInfo.Key)
            {
                case ConsoleKey.RightArrow:
                    Console.WriteLine("|_");
                    break;
                case ConsoleKey.LeftArrow:
                    Console.WriteLine("_|");
                    break;
                case ConsoleKey.UpArrow:
                    Console.WriteLine("-");
                    break;
                case ConsoleKey.DownArrow:
                    Console.WriteLine("_");
                    break;
                default:
                    cont = false;
                    break;
            }
        }
    }
}