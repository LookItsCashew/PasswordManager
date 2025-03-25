namespace CredentialManager.Encryption;

public class EncryptionService : IEncryptionService
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

    public void SaltHash(string salt, byte[] hash)
    {
        throw new NotImplementedException();
    }
}