using CredentialManager.Models;
using CredentialManager.Utils;
using SQLite;

namespace CredentialManager.Database;

public static class ConnectionManager
{
    private static readonly string DatabaseConn = Global.DefaultVaultFolderPath + "credentials.db";

    public static bool DatabaseExists { get; } = File.Exists(DatabaseConn);
    
    public static SQLiteConnection GetDatabaseConnection() => new SQLiteConnection(DatabaseConn);

    static ConnectionManager()
    {
        if (!DatabaseExists)
        {
            CreateDatabase();
        }
    }

    private static void CreateDatabase()
    {
        var conn = new SQLiteConnection(DatabaseConn);
        try
        {
            conn.CreateTable<Credential>();
            conn.CreateTable<User>();
            conn.CreateTable<Group>();
            conn.CreateTable<GroupType>();
        }
        catch (SQLiteException e)
        {
            Console.Error.WriteLine(e);
        }
        finally
        {
            conn.Close();
        }
    }
}