using System.Xml;

namespace CredentialManager.Utils;

public class Identifiers
{
    private readonly string _identifierFilePath = 
        Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "identifiers.xml";

    private static Identifiers? _instance = null;
    
    private readonly XmlDocument _identifierDocument;
    
    public static Identifiers Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Identifiers();
            }
            return _instance;
        }
    }

    private Identifiers()
    {
        if (!File.Exists(_identifierFilePath))
        {
            Xml.CreateXmlFile(_identifierFilePath, "identifiers");
            
            _identifierDocument = Xml.GetXmlDocument(_identifierFilePath)!;
            
            // create xml elements for each identifier in the document
            var keys = _identifierDocument.CreateElement("keys");
            var creds = _identifierDocument.CreateElement("credentials");
            Xml.AppendElementToRoot(_identifierDocument, keys);
            Xml.AppendElementToRoot(_identifierDocument, creds);
            
            // set initial value to 0 for each identifier
            keys.InnerText = "0";
            creds.InnerText = "0";
            
            Xml.SaveXmlDocument(_identifierDocument, _identifierFilePath);
        }
        _identifierDocument = Xml.GetXmlDocument(_identifierFilePath)!;
    }

    /// <summary>
    /// Retrieves the current identifier from file.
    /// This will only be used when the Identifier object is constructed.
    /// </summary>
    /// <param name="idName">name of the key to get.</param>
    /// <returns>int - current identifier saved in file.</returns>
    public int GetCurrentIdentifier(string idName)
    {
        int result = 0;
        try
        {
            var root = _identifierDocument.DocumentElement!;
            var child = root.GetElementsByTagName(idName)[0]!;
            result = int.Parse(child.InnerText);
        }
        catch (XmlException e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            Xml.SaveXmlDocument(_identifierDocument, _identifierFilePath);
        }
        return result;
    }
    
    /// <summary>
    /// Increments identifier and saves in memory and to file.
    /// </summary>
    public void IncrementIdentifier(string idName)
    {
        try
        {
            var root = _identifierDocument.DocumentElement!;
            var child = root.GetElementsByTagName(idName)[0]!;
            int newId = int.Parse(child.InnerText) + 1;

            // set the root's text to the incremented id
            child.InnerText = newId.ToString();
        }
        catch (XmlException e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            Xml.SaveXmlDocument(_identifierDocument, _identifierFilePath);
        }
    }
}