using System.Xml;

namespace CredentialManager;

public static class Xml
{
    private static bool BuildDocument(FileInfo file, string rootElement)
    {
        if (!file.Exists)
        {
            try
            {
                // create xml document at given filepath 
                var doc = new XmlDocument();

                // create metadata and root element of the document
                var decl = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                var root = doc.CreateElement(rootElement);

                // append declaration and root to document, then save document to file
                doc.AppendChild(decl);
                doc.AppendChild(root);
                doc.Save(file.FullName);

                return true;
            }
            catch (XmlException e)
            {
                throw new ApplicationException(
                    "Something went wrong generating the xml document; the file could not be created.");
            }
        }
        return false;
    }

    public static void CreateXmlFile(string filePath, string rootElement)
    {
        try
        {
            var file = new FileInfo(filePath);
            Console.WriteLine(BuildDocument(file, rootElement)  // expect this will throw app exception if anything goes wrong
                ? $"File {file.FullName} created."
                : $"File {file.FullName} already exists.");
        }
        catch (ApplicationException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public static XmlDocument? GetXmlFile(string filePath)
    {
        try
        {
            // load new xml document with file content at path
            var doc = new XmlDocument();
            doc.Load(filePath);
            
            return doc;
        }
        catch (Exception e)
        {
            switch (e)
            {
                case FileNotFoundException:
                    Console.WriteLine($"{filePath} not found.");
                    break;
                case XmlException:
                    Console.WriteLine("Malformed XML file.");
                    break;
                default:
                    Console.WriteLine(e.Message);
                    break;
            }
        }
        return null;
    }

    public static XmlElement? GetXmlElement(XmlDocument doc, string elementId)
    {
        // get the specified element from the given document 
        var element = doc.GetElementById(elementId);
        return element;
    }

    public static void AppendElementToRoot(string filePath, XmlElement child)
    {
        // get the document based on file path
        var doc = GetXmlFile(filePath);
        if (doc is null)
        {
            Console.WriteLine($"Could not open file {filePath}. It does not exist.");
            return;
        }
        
        try
        {
            // presume if the xml file exists, we've already given it a root element
            var root = doc.DocumentElement!;

            // append child element to root and save the document. 
            root.AppendChild(child);
            doc.Save(filePath);
        }
        catch (Exception e)
        {
            switch (e)
            {
                case InvalidOperationException :
                    Console.WriteLine("Could not add element to document.");
                    break;
                case XmlException :
                    Console.WriteLine("Malformed XML file.");
                    break;
                default:
                    Console.WriteLine(e.Message);
                    break;
            }
        }
    }
}