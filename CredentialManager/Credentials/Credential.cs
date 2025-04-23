using System.Xml;

namespace CredentialManager.Credentials;

public class Credential(string username, string password, string nickname, int id)
{
    public int Id { get; } = id;
    public string Username { get; private set; } = username;
    public string Password { get; private set; } = password;
    public string Nickname { get; private set; } = nickname;

    public void UpdateUsername(string newUsername) => Username = newUsername;
    
    public void UpdatePassword(string newPassword) => Password = newPassword;
    
    public void UpdateNickname(string newNickname) => Nickname = newNickname;

    public XmlElement? GetXmlElement(XmlDocument doc)
    {
        try
        {
            // create elements for credential field
            var credElement = doc.CreateElement("credential");
            var usernameElement = doc.CreateElement("username");
            var passwordElement = doc.CreateElement("password");

            // add id and nickname attributes to cred element
            credElement.SetAttribute("nickname", Nickname);
            
            // set value of elements
            usernameElement.InnerText = this.Username!;
            passwordElement.InnerText = this.Password!;

            // append child elements to credential element, then return credential element
            var appendedUsername = credElement.AppendChild(usernameElement);
            var appendedPass = credElement.AppendChild(passwordElement);
            
            return appendedUsername != null && appendedPass != null ? credElement : null;
        }
        catch (XmlException e)
        {
            Console.WriteLine("Unsupported character encountered.");
            Console.WriteLine(e.Message);
        }
        return null;
    }
}