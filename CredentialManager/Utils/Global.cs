using CredentialManager.Services;

namespace CredentialManager.Utils;

public static class Global
{
    public static DirectoryInfo AppDataDirectory => new DirectoryInfo(
        Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "CredentialManager"
            )
        );

    public static DirectoryInfo VaultDirectory => new DirectoryInfo(
        Path.Combine(
            AppDataDirectory.FullName,
            "vault")
        );

    public static readonly Identifiers Identifiers;

    public static readonly Keys Keys;
    
    static Global()
    {
        if (!VaultDirectory.Exists)
        {
            VaultDirectory.Create();
        }
        
        Identifiers = Identifiers.Instance;
        Keys = Keys.Instance;
    }
}