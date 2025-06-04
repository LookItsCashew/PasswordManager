using CredentialManager.Services;
using CredentialManager.User;

namespace CredentialManagerTests;

public class EncryptionTests
{
    private readonly string _key = "TestKey";
    
    [Fact]
    public void SaltingAddsTextToString()
    {
        EncryptionService esCrypto = new EncryptionService(_key);
        var strOne = "Hello";
        var strTwo = "World";
        
        var salt = esCrypto.SaltHash(strOne, strTwo);
        Assert.Equal("HelloWorld", salt);
    }

    [Fact]
    public void SaltedHashIsDifferentThanHash()
    {
        EncryptionService esCrypto = new EncryptionService(_key);
        var saltedText = "HelloWorld";
        var unsaltedText = "World";
        
        Assert.NotEqual(esCrypto.HashText(saltedText), esCrypto.HashText(unsaltedText));
    }
    
    [Fact]
    public void HashingDifferentTextResultsInSameHash()
    {
        EncryptionService esCrypto = new EncryptionService(_key);
        
        Assert.Equal(esCrypto.HashText("testhash"), esCrypto.HashText("testhash"));
    }

    [Fact]
    public void LoginReturnsFalseWithWrongPassword()
    {
        UserService usUser = new UserService();
    }
}