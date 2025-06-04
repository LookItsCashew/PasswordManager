using System.Security.Cryptography;
using System.Text;

namespace CredentialManager.Services;

public class EncryptionService
{
    private readonly byte[] _key;

    public EncryptionService(string key)
    {
        using var sha256 = SHA256.Create();

        _key = sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
        Console.WriteLine($"Key has been generated: {Convert.ToBase64String(_key)}");
    }

    /// <summary>
    /// Generates Encryptor with default, non-user specified key
    /// </summary>
    public EncryptionService()
    {
        // TODO: replace this with an environment variable
        var keyStr = "LpJGdyo1";
        
        using var sha256 = SHA256.Create();
        _key = sha256.ComputeHash(Encoding.UTF8.GetBytes(keyStr));
    }
    
    /// <summary>
    /// Encrypts plain text into encrypted text based on key using SHA256
    /// </summary>
    /// <param name="plainText">The plan text to be encrypted</param>
    /// <returns>The encrypted representation of plain text</returns>
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

    /// <summary>
    /// Decrypts encrypted text based on key using SHA256 back to plain text
    /// </summary>
    /// <param name="encryptedText">The encrypted version of text</param>
    /// <returns>The decrypted plain text</returns>
    /// <exception cref="ArgumentException">Encryption text length is not valid</exception>
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

    /// <summary>
    /// Generates hashed version of plain text
    /// </summary>
    /// <param name="plainText">Plain text to be hashed</param>
    /// <returns>256 hash of plain text in byte array</returns>
    public byte[] HashText(string plainText)
    {
        // Convert the plaintext into hashed byte array
        using var sha256 = SHA256.Create();
        var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(plainText));

        return hash;
    }
    
    /// <summary>
    /// Adds salt to plain text, which will be hashed
    /// </summary>
    /// <param name="salt">The salt to add to text</param>
    /// <param name="plainText">Plain text that will be salted</param>
    /// <returns></returns>
    public string SaltHash(string salt, string plainText) => string.Concat([salt, plainText]);
}