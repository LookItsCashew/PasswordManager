using CredentialManager.Models;
using CredentialManager.Utils;
using Spectre.Console;
using SQLite;

namespace CredentialManager.Database;

public static class ConnectionManager
{
    private static readonly FileInfo _database = new FileInfo(
        Path.Combine(
            Global.VaultDirectory.FullName,
            "credentials.db"
            )
        );

    public static bool DatabaseExists { get; } = _database.Exists;
    
    public static SQLiteConnection GetDatabaseConnection() => 
        new SQLiteConnection(_database.FullName);

    static ConnectionManager()
    {
        if (!_database.Exists)
        {
            CreateDatabase();
        }
    }

    private static void CreateDatabase()
    {
        var conn = new SQLiteConnection(_database.FullName);
        try
        {
            conn.CreateTable<Credential>();
            conn.CreateTable<User>();
            conn.CreateTable<Group>();
            conn.CreateTable<GroupType>();
        }
        catch (SQLiteException e)
        {
            AnsiConsole.WriteException(e);
        }
        finally
        {
            conn.Close();
        }
    }
}