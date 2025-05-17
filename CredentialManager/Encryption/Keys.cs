using System;
using System.Xml;
using CredentialManager.Utils;

namespace CredentialManager.Encryption;

public class Keys
{
    private readonly string _keyFilePath = Global.DefaultVaultFolderPath + "keys.xml";

    private static Keys? _instance = null;

    private Keys()
    {
        if (!File.Exists(_keyFilePath))
        {
            Xml.CreateXmlFile(_keyFilePath, "keys");
            var keyText = GenerateKeyBase();
            
            var doc = Xml.GetXmlDocument(_keyFilePath)!;
            var root = doc.DocumentElement!;
            
            // create root element for keys
            var key = doc.CreateElement("key");
            
            // set key id
            key.SetAttribute("id", 
                Global.Identifiers.GetCurrentIdentifier("keys")
                    .ToString());
            root.AppendChild(key);
            
            // set initial value to guid-based key text, encrypted
            Encryptor encryption = new Encryptor();  // uses a default key encryptor to encrypt the keys initially
            
            key.InnerText = encryption.EncryptText(keyText.ToString());
            
            doc.Save(_keyFilePath);
            
            // increment the key identifier
            Global.Identifiers.IncrementIdentifier("keys");
        }
    }

    private Guid GenerateKeyBase() => Guid.NewGuid();

    public string GetKeyById(string id)
    {
        string rtnValue = "";
        try
        {  
            var doc = Xml.GetXmlDocument(_keyFilePath)!;
            
            var keyElements = doc.GetElementsByTagName("key");

            foreach (XmlElement el in keyElements)
            {
                // get the id attribute value
                var keyId = el.GetAttribute("id");
                if (keyId == id)
                {
                    // decrypt the keys
                    Encryptor encryption = new Encryptor();  // uses a default key encryptor to encrypt the keys initially
                
                    rtnValue = encryption.DecryptText(el.InnerText);
                    break;
                }
            }
        }
        catch (XmlException ex)
        {
            Console.Error.WriteLine(ex.Message);
        }
        return rtnValue;
    }

    public static Keys Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Keys();
            }
            return _instance;
        }
    }
    
    
}