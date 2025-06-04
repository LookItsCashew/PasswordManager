using CredentialManager.Services;
using CredentialManager.Utils;

namespace CredentialManager;

static class Program
{
    static void Main()
    {
        ICredentialService cxs = new CredentialXmlService();
        EncryptionService encryption = new EncryptionService(Global.Keys.GetKeyById("0"));
        var testPwd = "ImaPassw0rd";
        var encryptedPwd = encryption.EncryptText(testPwd);
        var decryptedPwd = encryption.DecryptText(encryptedPwd);
        
        var credentials = cxs.GetCredentials();
        foreach (var c in credentials)
        {
            Console.WriteLine(c.Id);
            Console.WriteLine(c.Nickname);
            Console.WriteLine(c.Username);
            Console.WriteLine(c.Password);
            Console.WriteLine("=======");
        }
    }
}