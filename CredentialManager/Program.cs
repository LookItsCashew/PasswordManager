using CredentialManager.Credentials;
using CredentialManager.Encryption;
using CredentialManager.Utils;

namespace CredentialManager;

static class Program
{
    static void Main()
    {
        CredentialXmlService cxs = new CredentialXmlService();
        Encryptor encryption = new Encryptor(Global.Keys.GetKeyById("0"));
        var testPwd = "ImaPassw0rd";
        var encryptedPwd = encryption.EncryptText(testPwd);
        var decryptedPwd = encryption.DecryptText(encryptedPwd);

        var cred = new Credential(
            "test",
            encryptedPwd,
            "Test Credential",
            Global.Identifiers.GetCurrentIdentifier("credentials"));

        Console.WriteLine(cxs.AddCredentialToDocument(cred)
            ? "Successfully Added Credential"
            : "Failed to Add Credential");
        
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