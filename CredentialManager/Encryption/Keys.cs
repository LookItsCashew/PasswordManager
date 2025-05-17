using System.Xml;
using CredentialManager.Utils;

namespace CredentialManager.Encryption;

public class Keys
{
    private readonly string _keyFilePath = Global.DefaultVaultFolderPath + "keys.xml";

    private static Keys? _instance;
    
    private readonly XmlDocument _keyDocument;
    
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

    private Keys()
    {
        if (!File.Exists(_keyFilePath))
        {
            Xml.CreateXmlFile(_keyFilePath, "keys");
            var keyText = GenerateKeyBase();
            
            _keyDocument = Xml.GetXmlDocument(_keyFilePath)!;
            
            // create root element for keys
            var keyElement = _keyDocument.CreateElement("key");
            
            // set key id
            keyElement.SetAttribute("id", 
                Global.Identifiers.GetCurrentIdentifier("keys")
                    .ToString());
            Xml.AppendElementToRoot(_keyDocument, keyElement);
            
            // set initial value to guid-based key text, encrypted
            Encryptor encryption = new Encryptor();  // uses a default encryptor to encrypt the keys
            
            keyElement.InnerText = encryption.EncryptText(keyText.ToString());
            
            Xml.SaveXmlDocument(_keyDocument, _keyFilePath);
            
            // increment the key identifier
            Global.Identifiers.IncrementIdentifier("keys");
        }
        _keyDocument = Xml.GetXmlDocument(_keyFilePath)!;
    }

    private Guid GenerateKeyBase() => Guid.NewGuid();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public string GetKeyById(string id)
    {
        string rtnValue = "";
        try
        {
            var keyElements = _keyDocument.GetElementsByTagName("key");

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
}