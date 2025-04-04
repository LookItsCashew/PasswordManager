using CredentialManager.Encryption;

namespace CredentialManagerTests;

public class EncryptionServiceStub : IEncryptionService
{
    public string EncryptText(string plainText)
    {
        throw new NotImplementedException();
    }

    public string DecryptText(string encryptedText)
    {
        throw new NotImplementedException();
    }

    public byte[] HashText(string plainText)
    {
        throw new NotImplementedException();
    }

    public string SaltHash(string salt, string plainText)
    {
        throw new NotImplementedException();
    }
}