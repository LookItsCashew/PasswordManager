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
        var decl = xml.CreateXmlDeclaration("1.0", "utf-8", null);
        var root = xml.CreateElement("Credentials");
        xml.AppendChild(decl);
        xml.AppendChild(root);
        xml.Save(_credentialFile.FullName);
    }

    public void AddCredential(Credential credential)
    {
        
    }

    public void RemoveCredential(Credential credential)
    {
        
    }
}