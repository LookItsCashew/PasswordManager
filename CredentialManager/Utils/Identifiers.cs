using System.Xml;

namespace CredentialManager.Utils;

public class Identifiers
{
    private readonly string _identifierFilePath = 
        Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "identifiers.xml";

    private static Identifiers? _instance = null;

    private Identifiers()
    {
        if (!File.Exists(_identifierFilePath))
        {
            Xml.CreateXmlFile(_identifierFilePath, "identifiers");
            
            var doc = Xml.GetXmlDocument(_identifierFilePath)!;
            var root = doc.DocumentElement!;
            
            // create xml elements for each identifier in the document
            var keys = doc.CreateElement("keys");
            var creds = doc.CreateElement("credentials");
            root.AppendChild(keys);
            root.AppendChild(creds);
            
            // set initial value to 0 for each identifier
            keys.InnerText = "0";
            creds.InnerText = "0";
            
            doc.Save(_identifierFilePath);
        }
        
    }

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

    /// <summary>
    /// Retrieves the current identifier from file.
    /// This will only be used when the Identifier object is constructed.
    /// </summary>
    /// <param name="idName">name of the key to get.</param>
    /// <returns>int - current identifier saved in file.</returns>
    public int GetCurrentIdentifier(string idName)
    {
        int result = 0;
        var doc = Xml.GetXmlDocument(_identifierFilePath)!;
        try
        {
            // load id document into mem
            doc.Load(_identifierFilePath);

            var root = doc.DocumentElement!;
            var child = root.GetElementsByTagName(idName)[0]!;
            result = int.Parse(child.InnerText);
        }
        catch (XmlException e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            doc.Save(_identifierFilePath);
        }
        return result;
    }
    
    /// <summary>
    /// Increments identifier and saves in memory and to file.
    /// </summary>
    public void IncrementIdentifier(string idName)
    {
        var doc = Xml.GetXmlDocument(_identifierFilePath)!;
        try
        {
            // load id document into mem
            doc.Load(_identifierFilePath);

            var root = doc.DocumentElement!;
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
            doc.Save(_identifierFilePath);
        }
    }
}