namespace CredentialManager.Encryption;

public interface IEncryptionService
{
    public string EncryptText(string plainText);
    public string DecryptText(string encryptedText);
    public byte[] HashText(string plainText);
    public string SaltHash(string salt, string plainText);
}