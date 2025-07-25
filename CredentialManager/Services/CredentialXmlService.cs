﻿using System.Xml;
using CredentialManager.Utils;
using CredentialManager.Models;

namespace CredentialManager.Services;

public class CredentialXmlService : ICredentialService
{
    private readonly string _credFilePath = Global.DefaultVaultFolderPath + @"credentials.xml";

    private readonly XmlDocument _credentialDocument;

    public CredentialXmlService()
    {
        if (!File.Exists(_credFilePath))
        {
            Xml.CreateXmlFile(_credFilePath, "credentials");
        }
        _credentialDocument = Xml.GetXmlDocument(_credFilePath)!;
    }
    
    /// <summary>
    /// Appends a new Xml credential element to the root of the vault file.
    /// </summary>
    /// <param name="credential">Credential that will be added to the vault.</param>
    /// <returns>Whether the credential was added to the file successfully.</returns>
    public void AddCredential(Credential credential)
    {
        try
        {
            // this shouldn't be null, but I am using nullable for error handling
            //var element = credential.GetXmlElement(_credentialDocument)!;
            var element = GetElementFromCredential(credential);

            if (element != null)
            {
                Xml.AppendElementToRoot(_credentialDocument, element);
                Global.Identifiers.IncrementIdentifier("credentials");

                Xml.SaveXmlDocument(_credentialDocument, _credFilePath);
            }

            throw new ApplicationException("Failed to add credential");
        }
        catch (XmlException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (ApplicationException e)
        {
            Console.Error.WriteLine("Could not convert credential to Xml element.\n\n" + e.Message);
        }
    }

    private XmlElement? GetElementFromCredential(Credential credential)
    {
        try
        {
            // create elements for credential field
            var credElement = _credentialDocument.CreateElement("credential");
            var usernameElement = _credentialDocument.CreateElement("username");
            var passwordElement = _credentialDocument.CreateElement("password");

            // add id and nickname attributes to cred element
            credElement.SetAttribute("id", credential.CredentialId.ToString());
            credElement.SetAttribute("nickname", credential.Nickname);
            
            // set value of elements
            usernameElement.InnerText = credential.Username;
            passwordElement.InnerText = credential.Password;

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
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="credential"></param>
    public void RemoveCredentialById(Credential credential)
    {
        try
        {
            // get each credential element
            var credElements = _credentialDocument.GetElementsByTagName("credential");
            var root = _credentialDocument.DocumentElement!;

            foreach (XmlElement el in credElements)
            {
                // very safe to presume there is an "id" attribute
                var credId = el.GetAttribute("id");
                if (credId == credential.CredentialId.ToString())
                {
                    root.RemoveChild(el);
                    break;
                }
            }
            
            Xml.SaveXmlDocument(_credentialDocument, _credFilePath);
        }
        catch (XmlException e)
        {
            Console.WriteLine(e.Message);
        }
    }
    
    /// <summary>
    /// Retrieves each credential from the file.
    /// </summary>
    /// <returns>List of Credential objects.
    /// Returns empty list if there are no credentials added yet.</returns>
    public List<Credential> GetCredentials()
    {
        List<Credential> creds = [];
        try
        {
            // Get each credential node in the file
            var credNodes = _credentialDocument.GetElementsByTagName("credential");
            if (credNodes.Count > 0)
            {
                foreach (XmlNode node in credNodes)
                {
                    var username = "";
                    var password = "";
                    var nickname = node!.Attributes!["nickname"]!.Value;
                    var id = int.Parse(node.Attributes["id"]!.Value);
                    foreach (XmlElement element in node)
                    {
                        switch (element.Name)
                        {
                            case "username":
                                username = element.InnerText;
                                break;
                            case "password":
                                password = element.InnerText;
                                break;
                        }
                    }
                
                    // add new credential object to list
                    Credential cred = new Credential();
                    cred.Username = username;
                    cred.Password = password;
                    cred.Nickname = nickname;
                    cred.CredentialId = id;
                    creds.Add(cred);
                }
            }
        }
        catch (XmlException e)
        {
            Console.Error.WriteLine("Couldn't read credentials from xml.\n\n" + 
                                    e.Message);
        }
        return creds;
    }
}