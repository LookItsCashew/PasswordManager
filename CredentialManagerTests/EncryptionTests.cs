using CredentialManager.Encryption;
using CredentialManager.User;

namespace CredentialManagerTests;

public class EncryptionTests
{
    private readonly string _key = "TestKey";
    
    [Fact]
    public void SaltingAddsTextToString()
    {
        Encryptor esCrypto = new Encryptor(_key);
        var strOne = "Hello";
        var strTwo = "World";
        
        var salt = esCrypto.SaltHash(strOne, strTwo);
        Assert.Equal("HelloWorld", salt);
    }

    [Fact]
    public void SaltedHashIsDifferentThanHash()
    {
        Encryptor esCrypto = new Encryptor(_key);
        var saltedText = "HelloWorld";
        var unsaltedText = "World";
        
        Assert.NotEqual(esCrypto.HashText(saltedText), esCrypto.HashText(unsaltedText));
    }
    
    [Fact]
    public void HashingDifferentTextResultsInSameHash()
    {
        Encryptor esCrypto = new Encryptor(_key);
        
        Assert.Equal(esCrypto.HashText("testhash"), esCrypto.HashText("testhash"));
    }

    [Fact]
    public void LoginReturnsFalseWithWrongPassword()
    {
        UserService usUser = new UserService();
    }
}