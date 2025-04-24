using System.Xml;

namespace CredentialManager.Credentials;

public class CredentialIdentifier
{
    private static readonly string IdentifierFilePath = 
        Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "id.xml";
    
    public static int CurrentIdentifier { get; internal set; }

    public CredentialIdentifier()
    {
        if (!File.Exists(IdentifierFilePath))
        {
            Xml.CreateXmlFile(IdentifierFilePath, "identifier");
            
            // set initial value to 0 in document
            var doc = Xml.GetXmlDocument(IdentifierFilePath)!;
            var root = doc.DocumentElement!;
            root.InnerText = "0";
            doc.Save(IdentifierFilePath);
            CurrentIdentifier = 0;
        }
    }

    public int GetCurrentIdentifier()
    {
        int result = 0;
        var doc = Xml.GetXmlDocument(IdentifierFilePath)!;
        try
        {
            // load id document into mem
            doc.Load(IdentifierFilePath);

            var root = doc.DocumentElement!;
            result = int.Parse(root.InnerText);
        }
        catch (XmlException e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            doc.Save(IdentifierFilePath);
            CurrentIdentifier = result;
        }
        return result;
    }

    public void IncrementIdentifier()
    {
        var doc = Xml.GetXmlDocument(IdentifierFilePath)!;
        try
        {
            // load id document into mem
            doc.Load(IdentifierFilePath);

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
            doc.Save(IdentifierFilePath);
        }
    }
}