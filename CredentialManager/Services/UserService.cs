using System.Xml;
using CredentialManager.Utils;

namespace CredentialManager.User;

public class UserService
{
    private readonly string _userFilePath = Directory.GetCurrentDirectory() + 
                                                Path.DirectorySeparatorChar + "user.xml";

    private readonly XmlDocument _userDocument;

    public UserService()
    {
        if (!File.Exists(_userFilePath))
        {
            // create the file
            Xml.CreateXmlFile(_userFilePath, "user");
            
            
        }
        _userDocument = Xml.GetXmlDocument(_userFilePath)!;
    }

    public bool LogIn(string username, string password)
    {
        throw new NotImplementedException();
    }

    public bool Register(string username, string password)
    {
        throw new NotImplementedException();
    }
}