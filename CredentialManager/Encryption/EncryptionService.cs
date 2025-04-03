using System.Security.Cryptography;
using System.Text;

namespace CredentialManager.Encryption;

public class EncryptionService : IEncryptionService
{
    private readonly byte[] _key;

    public EncryptionService(string key)
    {
        using var sha256 = SHA256.Create();

        _key = sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
        Console.WriteLine($"Key has been generated: {Convert.ToBase64String(_key)}");
    }
    
    public string EncryptText(string plainText)
    {
        // Setup aes object
        using var aes = Aes.Create();
        aes.Key = _key;
        aes.GenerateIV();
        
        // Create streams to write to mem and encrypt
        using var encryptMemStream = new MemoryStream();
        encryptMemStream.Write(aes.IV, 0, aes.IV.Length);
        using var encryptor = aes.CreateEncryptor();
        using var encryptCryptoStream = new CryptoStream(encryptMemStream, encryptor, CryptoStreamMode.Write);
        
        // Encrypt the plaintext
        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
        encryptCryptoStream.Write(plainBytes, 0, plainBytes.Length);
        encryptCryptoStream.FlushFinalBlock();
        
        return Convert.ToBase64String(encryptMemStream.ToArray());
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