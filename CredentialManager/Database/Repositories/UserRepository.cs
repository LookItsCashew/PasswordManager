using CredentialManager.Models;
using SQLite;
using SQLitePCL;

namespace CredentialManager.Database.Repositories;

public class UserRepository : Repository<User>
{
    public override void Create(User obj)
    {
        var conn = ConnectionManager.GetDatabaseConnection();
        try
        {
            conn.Insert(obj);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            conn.Close();
        }
    }

    public override void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public override User? Read(int id)
    {
        throw new NotImplementedException();
    }

    public override List<User>? ReadAll()
    {
        var conn = ConnectionManager.GetDatabaseConnection();
        List<User>? results = null;
        try
        {
            results = conn.Query<User>($"SELECT * FROM User");
        }
        catch (SQLiteException e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            conn.Close();
        }
        return results;
    }

    public override void Update(int id, User obj)
    {
        throw new NotImplementedException();
    }
}
