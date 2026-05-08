namespace CredentialManager.Database.Repositories;

public abstract class Repository<T>
{
    public abstract void Create(T obj);
    public abstract void Update(int id, T obj);
    public abstract void Delete(int id);
    public abstract T? Read(int id);
    public abstract List<T>? ReadAll();
}
