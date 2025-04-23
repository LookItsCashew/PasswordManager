using System.Xml;

namespace CredentialManager;

/// <summary>
/// Static class that exposes logic for general XML file handling to the application
/// </summary>
public static class Xml
{
    /// <summary>
    /// Saves the current state of the XML document object to given file. 
    /// </summary>
    /// <param name="doc">The document requested to be saved.</param>
    /// <param name="path">The file path where the document should be saved.</param>
    public static void SaveXmlDocument(XmlDocument doc, string path)
    {
        try
        {
            doc.Save(path);
        }
        catch (XmlException e)
        {
            Console.WriteLine("Malformed XML Document. Could not save it to file.");
            Console.WriteLine(e.Message);
        }
    }
    
    /// <summary>
    /// Builds XML document object from given FileInfo object. Creates requested root element.
    /// </summary>
    /// <param name="file">The file to save the document to.</param>
    /// <param name="rootElement">Requested name of document's root element</param>
    /// <returns>True if document was saved successfully. False otherwise</returns>
    /// <exception cref="ApplicationException"></exception>
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
    
    /// <summary>
    /// Public facing method to create a requested XML file at the provided file path.
    /// </summary>
    /// <param name="filePath">Filepath to create file at.</param>
    /// <param name="rootElement">Name of the requested document root.</param>
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

    /// <summary>
    /// Provides external classes an XML document object loaded with the requested XML file.
    /// </summary>
    /// <param name="filePath">Path of XML file</param>
    /// <returns>XmlDocument object loaded with file</returns>
    public static XmlDocument? GetXmlDocument(string filePath)
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

    /// <summary>
    /// Provides XmlElement with given ID from document.
    /// </summary>
    /// <param name="doc">Document to get the element from.</param>
    /// <param name="elementId">Element's ID to find.</param>
    /// <returns></returns>
    public static XmlElement? GetXmlElement(XmlDocument doc, string elementId)
    {
        // get the specified element from the given document 
        var element = doc.GetElementById(elementId);
        return element;
    }

    /// <summary>
    /// Appends a new XmlElement to a given document. Saved at the provided file path.
    /// </summary>
    /// <param name="doc">The document to append the child element to.</param>
    /// <param name="child">XmlElement to append to the document</param>
    public static void AppendElementToRoot(XmlDocument doc, XmlElement child)
    {
        try
        {
            // presume if the xml file exists, we've already given it a root element
            var root = doc.DocumentElement!;

            // append child element to root
            root.AppendChild(child);
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