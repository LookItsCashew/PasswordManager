using CredentialManager.Models;

namespace CredentialManager.Services;

public interface ICredentialService
{
    public void AddCredential(Credential credential);

    public void RemoveCredentialById(Credential credential);

    public List<Credential> GetCredentials();
}