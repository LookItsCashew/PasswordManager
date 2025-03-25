namespace CredentialManager.Encryption;

public interface IEncryptionService
{
    public string EncryptText(string plainText);
    public string DecryptText(string encryptedText);
    public byte[] HashPassword(string password);
    public void SaltPassword(string salt, string password);
}