namespace CredentialManager.Credentials;

public interface ICredentialService
{
    public bool AddCredential(Credential credential);
    public void RemoveCredential(Credential credential);
    public List<Credential> GetCredentials();
}