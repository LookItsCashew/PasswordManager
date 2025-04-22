using System.Xml;

namespace CredentialManager.Credentials;

public class CredentialXmlService : ICredentialService
{
    private static readonly string WorkingDirectory =
        Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "vault" + Path.DirectorySeparatorChar;

    private readonly FileInfo _credentialFile = new(WorkingDirectory + @"credentials.xml");
    
    private readonly string _credFilePath =  WorkingDirectory + @"credentials.xml";
    
    private readonly bool _workingDirectoryExists = Directory.Exists(WorkingDirectory);

    public CredentialXmlService()
    {
        GenerateFileSystemObjects();
    }
    
    private void GenerateFileSystemObjects()
    {
        if (!_workingDirectoryExists)
        {
            Directory.CreateDirectory(WorkingDirectory);
        }
        
        if (!File.Exists(_credFilePath))
        {
            Xml.CreateXmlFile(_credFilePath, "credentials");
        }
    }

    public bool AddCredential(Credential credential)
    {
        try
        {
            // these shouldn't be null, but I am using nullable for error handling
            var doc = Xml.GetXmlDocument(_credFilePath)!;
            var element = credential.GetXmlElement(doc);
            Xml.AppendElementToRoot(doc, element!, _credFilePath);
            
            return true;
        }
        catch (XmlException e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }

    public void RemoveCredential(Credential credential)
    {
        try
        {
            // get list of all credential nodes in document
            var doc = Xml.GetXmlDocument(_credFilePath)!;
            var element = credential.GetXmlElement(doc)!;
            
            // remove the credential's node from the document
            doc.RemoveChild(element);

            doc.Save(_credFilePath);
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
            var doc = Xml.GetXmlDocument(_credFilePath)!;

            // Get each credential node in the file
            var credNodes = doc.GetElementsByTagName("credential");
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
                Credential cred = new(username, password, nickname);
                creds.Add(cred);
            }
        }
        catch (XmlException e)
        {
            Console.WriteLine("Couldn't read credentials from xml.");
        }
        return creds;
    }
}