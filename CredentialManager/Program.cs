using System.Buffers.Text;
using System.Net;
using System.Security.Cryptography;
using System.Text.Unicode;
using CredentialManager.Credentials;
using CredentialManager.Encryption;

namespace CredentialManager;

class Program
{
    static void Main()
    {
        ICredentialService xml = new CredentialXmlService();
        IEncryptionService encryption = new EncryptionService("TestKeyLol");
        var testPwd = "ImaPassw0rd";
        var encryptedPwd = encryption.EncryptText(testPwd);
        var decryptedPwd = encryption.DecryptText(encryptedPwd);
        var cred = new Credential("test", encryptedPwd, "Test Credential");
        
        if (xml.AddCredential(cred))
        {
            Console.WriteLine("Successfully Added Credential");
        }
        else
        {
            Console.WriteLine("Failed to Add Credential");
        }
        
        var credentials = xml.GetCredentials();
        foreach (var c in credentials)
        {
            Console.WriteLine(c.Nickname);
            Console.WriteLine(c.Username);
            Console.WriteLine(c.Password);
            Console.WriteLine("=======");
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