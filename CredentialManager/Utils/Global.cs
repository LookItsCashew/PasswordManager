using CredentialManager.Encryption;

namespace CredentialManager.Utils;

public static class Global
{
    public static string DefaultVaultFolderPath => 
        Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "vault" + Path.DirectorySeparatorChar;
    
    public static readonly Identifiers Identifiers;

    public static readonly Keys Keys;
    
    static Global()
    {
        if (!Directory.Exists(DefaultVaultFolderPath))
        {
            Directory.CreateDirectory(DefaultVaultFolderPath);
        }
        
        Identifiers = Identifiers.Instance;
        Keys = Keys.Instance;
    }
}