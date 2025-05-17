using System.Xml;
using CredentialManager.Utils;

namespace CredentialManager.Credentials;

public class CredentialXmlService
{
    public string CredFilePath { get; } = Global.DefaultVaultFolderPath + @"credentials.xml";

    public CredentialXmlService()
    {
        if (!File.Exists(CredFilePath))
        {
            Xml.CreateXmlFile(CredFilePath, "credentials");
        }
    }
    
    /// <summary>
    /// Appends a new Xml credential element to the root of the vault file.
    /// </summary>
    /// <param name="doc">The document object to append credential to the root.</param>
    /// <param name="credential">Credential that will be added to the vault.</param>
    /// <returns>Whether the credential was added to the file successfully.</returns>
    public bool AddCredentialToDocument(XmlDocument doc, Credential credential)
    {
        try
        {
            // this shouldn't be null, but I am using nullable for error handling
            var element = credential.GetXmlElement(doc)!;
            Xml.AppendElementToRoot(doc, element);
            Global.Identifiers.IncrementIdentifier("credentials");
            
            return true;
        }
        catch (XmlException e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }
    
    public void RemoveCredentialFromDocument(XmlDocument doc, XmlElement element)
    {
        try
        {
            // get xml element of the credential
            
            
            // the credential element is a child of the document's root
            var root = doc.DocumentElement!;
            
            // remove the credential's node from the document
            root.RemoveChild(element);
        }
        catch (XmlException e)
        {
            Console.WriteLine(e.Message);
        }
    }
    
    /// <summary>
    /// Retrieves each credential from the file.
    /// </summary>
    /// <returns>List of Credential objects.
    /// Returns empty list if there are no credentials added yet.</returns>
    public List<Credential> GetCredentials()
    {
        List<Credential> creds = [];
        try
        {
            var doc = Xml.GetXmlDocument(CredFilePath)!;

            // Get each credential node in the file
            var credNodes = doc.GetElementsByTagName("credential");
            if (credNodes.Count > 0)
            {
                foreach (XmlNode node in credNodes)
                {
                    var username = "";
                    var password = "";
                    var nickname = node!.Attributes!["nickname"]!.Value;
                    var id = int.Parse(node.Attributes["id"]!.Value);
                    foreach (XmlElement element in node)
                    {
                        switch (element.Name)
                        {
                            case "username":
                                username = element.InnerText;
                                break;
                            case "password":
                                password = element.InnerText;
                                break;
                        }
                    }
                
                    // add new credential object to list
                    Credential cred = new(username, password, nickname, id);
                    creds.Add(cred);
                }
            }
        }
        catch (XmlException e)
        {
            Console.WriteLine("Couldn't read credentials from xml.");
        }
        return creds;
    }
}