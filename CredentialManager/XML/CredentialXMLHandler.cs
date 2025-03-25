using System.Xml;

namespace CredentialManager.XML;

public class CredentialXMLHandler
{
    private readonly string _workingDirectory =
        Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "vault" + Path.DirectorySeparatorChar;

    private FileInfo _credentialFile;

    public CredentialXMLHandler()
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
        var root = xml.CreateElement("Credentials");
        
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
            var cred = xml.CreateElement("Credential");

            // Create elements for each property in credential 
            var nickname = xml.CreateElement("Nickname");
            var username = xml.CreateElement("Username");
            var password = xml.CreateElement("Password");
            nickname.InnerText = credential.Nickname;
            username.InnerText = credential.Username;
            password.InnerText = credential.Password;

            cred.AppendChild(nickname);
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
}