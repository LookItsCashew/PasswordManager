using System.Xml;

namespace CredentialManager.Credentials;

public class CredentialXmlService : ICredentialService
{
    private readonly string _workingDirectory =
        Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "vault" + Path.DirectorySeparatorChar;

    private FileInfo _credentialFile;

    public CredentialXmlService()
    {
        if (!WorkingDirectoryExists())
        {
            CreateWorkingDirectory();
        }
        _credentialFile = new FileInfo(_workingDirectory + @"credentials.xml");
        if (!_credentialFile.Exists)
        {
            _credentialFile.Create().Close();
            BuildCredentialDocument();
        }
    }

    private bool WorkingDirectoryExists() => Directory.Exists(_workingDirectory);
    
    private void CreateWorkingDirectory() => Directory.CreateDirectory(_workingDirectory);

    private void BuildCredentialDocument()
    {
        var xml = new XmlDocument();
        
        // Declare metadata of xml document and its root element
        var decl = xml.CreateXmlDeclaration("1.0", "utf-8", null);
        var root = xml.CreateElement("credentials");
        
        xml.AppendChild(decl);
        xml.AppendChild(root);
        
        xml.Save(_credentialFile.FullName);
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