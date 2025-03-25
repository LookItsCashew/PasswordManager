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

    public byte[] HashPassword(string password)
    {
        throw new NotImplementedException();
    }

    public void SaltPassword(string salt, string password)
    {
        throw new NotImplementedException();
    }
}