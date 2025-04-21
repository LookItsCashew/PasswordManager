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
        
        Xml.CreateXmlFile(_credFilePath, "credentials");
    }

    public bool AddCredential(Credential credential)
    {
        try
        {
            var xml = new XmlDocument();
            xml.Load(_credentialFile.FullName);

            // Get root, so new credential can be added as a child node of it
            var root = xml.DocumentElement!;
            var cred = xml.CreateElement("credential");
            cred.SetAttribute("nickname", credential.Nickname);

            // Create elements for each property in credential 
            var username = xml.CreateElement("username");
            var password = xml.CreateElement("password");
            username.InnerText = credential.Username;
            password.InnerText = credential.Password;
            
            cred.AppendChild(username);
            cred.AppendChild(password);
            root.AppendChild(cred);

            xml.Save(_credentialFile.FullName);
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
        
    }
    
    public List<Credential> GetCredentials()
    {
        List<Credential> creds = new List<Credential>();
        try
        {
            var xml = new XmlDocument();
            xml.Load(_credentialFile.FullName);

            // Get each credential node in the file
            var credNodes = xml.GetElementsByTagName("credential");
            foreach (XmlNode node in credNodes)
            {
                var cred = new Credential();
                var attr = node.Attributes["nickname"];
                cred.UpdateNickname(attr.Value);
                foreach (XmlElement element in node)
                {
                    switch (element.Name)
                    {
                        case "username":
                            cred.UpdateUsername(element.InnerText);
                            break;
                        case "password":
                            cred.UpdatePassword(element.InnerText);
                            break;
                    }
                }
                
                creds.Add(cred);
            }
        }
        catch (XmlException e)
        {
            Console.WriteLine(e.Message);
        }
        return creds;
    }
}