using CredentialManager.Credentials;
using CredentialManager.Encryption;
using CredentialManager.Utils;

namespace CredentialManager;

class Program
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

        var credDoc = Xml.GetXmlDocument(cxs.CredFilePath);
        if (credDoc != null && cxs.AddCredentialToDocument(credDoc, cred))
        {
            Xml.SaveXmlDocument(credDoc, cxs.CredFilePath);
            Console.WriteLine("Successfully Added Credential");
        }
        else
        {
            Console.WriteLine("Failed to Add Credential");
        }
        
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