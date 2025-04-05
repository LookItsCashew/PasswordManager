using CredentialManager.Encryption;
using CredentialManager.User;
using System.Security.Cryptography;

namespace CredentialManagerTests;

public class EncryptionTests
{
    private readonly string _key = "TestKey";
    
    [Fact]
    public void SaltingAddsTextToString()
    {
        IEncryptionService esCrypto = new EncryptionService(_key);
        var strOne = "Hello";
        var strTwo = "World";
        
        var salt = esCrypto.SaltHash(strOne, strTwo);
        Assert.Equal("HelloWorld", salt);
    }

    [Fact]
    public void SaltedHashIsDifferentThanHash()
    {
        IEncryptionService esCrypto = new EncryptionService(_key);
        var saltedText = "HelloWorld";
        var unsaltedText = "World";
        
        Assert.NotEqual(esCrypto.HashText(saltedText), esCrypto.HashText(unsaltedText));
    }
    
    [Fact]
    public void HashingDifferentTextResultsInSameHash()
    {
        IEncryptionService esCrypto = new EncryptionService(_key);
        
        Assert.Equal(esCrypto.HashText("testhash"), esCrypto.HashText("testhash"));
    }

    [Fact]
    public void LoginReturnsFalseWithWrongPassword()
    {
        IUserService usUser = new UserService();
    }
}