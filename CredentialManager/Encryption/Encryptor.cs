using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CredentialManager.Encryption;

public class Encryptor
{
    private readonly byte[] _key;

    public Encryptor(string key)
    {
        using var sha256 = SHA256.Create();

        _key = sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
        Console.WriteLine($"Key has been generated: {Convert.ToBase64String(_key)}");
    }

    public Encryptor()
    {
        var keyStr = "TestKey";
        
        using var sha256 = SHA256.Create();
        _key = sha256.ComputeHash(Encoding.UTF8.GetBytes(keyStr));
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
        // Convert encrypted text to bytes
        byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
        if (encryptedBytes.Length < 16)
            throw new ArgumentException("Encryption text length invalid.");
        
        // Setup aes object
        using var aes = Aes.Create();
        aes.Key = _key;
        byte[] iv = new byte[16];
        Array.Copy(encryptedBytes, 0, iv, 0, 16);
        aes.IV = iv;
    
        // Put encrypted bytes into memory stream, read from stream after 16-byte iv
        using var decrypt = aes.CreateDecryptor();
        using var decryptMemStream = new MemoryStream(encryptedBytes, 16, encryptedBytes.Length - 16);
        using var decryptCryptoStream = new CryptoStream(decryptMemStream, decrypt, CryptoStreamMode.Read);
        using var decryptStreamReader = new StreamReader(decryptCryptoStream);

        return decryptStreamReader.ReadToEnd();
    }

    public byte[] HashText(string plainText)
    {
        // Convert the plaintext into hashed byte array
        using var sha256 = SHA256.Create();
        var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(plainText));

        return hash;
    }

    public string SaltHash(string salt, string plainText) => string.Concat([salt, plainText]);
}