using System.Xml;

namespace CredentialManager.Credentials;

public class CredentialXmlService
{
    private static readonly string WorkingDirectory =
        Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "vault" + Path.DirectorySeparatorChar;
    
    public string CredFilePath { get; } = WorkingDirectory + @"credentials.xml";

    public CredentialXmlService()
    {
        GenerateFileSystemObjects();
    }
    
    private void GenerateFileSystemObjects()
    {
        if (!Directory.Exists(WorkingDirectory))
        {
            Directory.CreateDirectory(WorkingDirectory);
        }
        
        if (!File.Exists(CredFilePath))
        {
            Xml.CreateXmlFile(CredFilePath, "credentials");
        }
    }

    public bool AddCredentialToDocument(XmlDocument doc, Credential credential)
    {
        try
        {
            // this shouldn't be null, but I am using nullable for error handling
            var element = credential.GetXmlElement(doc)!;
            Xml.AppendElementToRoot(doc, element);
            
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
                    var attr = node.Attributes!["nickname"];
                    var username = "";
                    var password = "";
                    var nickname = attr!.InnerText;
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
                    //Credential cred = new(username, password, nickname);
                    //creds.Add(cred);
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