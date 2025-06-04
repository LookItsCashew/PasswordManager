namespace CredentialManager.Credentials;

public interface ICredentialService
{
    public bool AddCredential(Credential credential);

    public void RemoveCredentialById(Credential credential);

    public List<Credential> GetCredentials();
}