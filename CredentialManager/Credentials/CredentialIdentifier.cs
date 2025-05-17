using System.Xml;
using CredentialManager.Utils;

namespace CredentialManager.Credentials;

/// <summary>
/// Responsible for managing credential id uniqueness in the vault.
/// Id uniqueness is managed via Xml file which stores the next available id number as persistence.
/// Whenever a new credential gets added, the current id should be set as that credential's id, then
/// increment that value in the file. 
/// This is only to be used by the CredentialXmlService class as a component.
/// </summary>
public class CredentialIdentifier
{
    private readonly string _identifierFilePath = 
        Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "id.xml";
    
    public int CurrentIdentifier { get; internal set; }

    public CredentialIdentifier()
    {
        if (!File.Exists(_identifierFilePath))
        {
            Xml.CreateXmlFile(_identifierFilePath, "credentialId");
            
            // set initial value to 0 in document
            var doc = Xml.GetXmlDocument(_identifierFilePath)!;
            var root = doc.DocumentElement!;
            root.InnerText = "0";
            doc.Save(_identifierFilePath);
        }
        CurrentIdentifier = GetCurrentIdentifierFromDocument();
    }

    /// <summary>
    /// Retrieves the current identifier from file.
    /// This will only be used when the Identifier object is constructed.
    /// </summary>
    /// <returns>int - current identifier saved in file.</returns>
    public int GetCurrentIdentifierFromDocument()
    {
        int result = 0;
        var doc = Xml.GetXmlDocument(_identifierFilePath)!;
        try
        {
            // load id document into mem
            doc.Load(_identifierFilePath);

            var root = doc.DocumentElement!;
            result = int.Parse(root.InnerText);
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
    public void IncrementIdentifier()
    {
        var doc = Xml.GetXmlDocument(_identifierFilePath)!;
        try
        {
            // load id document into mem
            doc.Load(_identifierFilePath);

            var root = doc.DocumentElement!;
            int newId = int.Parse(root.InnerText) + 1;

            // set the root's text to the incremented id
            root.InnerText = newId.ToString();
            CurrentIdentifier = newId;
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