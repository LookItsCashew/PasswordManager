using System.Xml;

namespace CredentialManager.XML;

public class CredentialXMLHandler
{
    private readonly string _workingDirectory = Directory.GetCurrentDirectory() + @"\vault\";
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
        }
    }

    public bool WorkingDirectoryExists() => Directory.Exists(_workingDirectory);
    
    public void CreateWorkingDirectory() => Directory.CreateDirectory(_workingDirectory);

    public void AddCredential(Credential credential)
    {
        
    }
}