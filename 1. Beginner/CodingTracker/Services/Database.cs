using System.Data.SQLite;
using Dapper;
using System.Data;
using Spectre.Console;

namespace CodingTracker.Services;

internal class Database
{
    private static string DatabasePath { get; } = System.Configuration.ConfigurationManager.AppSettings.Get("DatabasePath") ?? "";
    private static string ConnectionString { get; } = System.Configuration.ConfigurationManager.AppSettings.Get("ConnectionString") ?? "";
   // public SQLiteConnection DbConnection = new SQLiteConnection(ConnectionString);

    public Database()
    {
        SQLiteConnection DbConnection = new SQLiteConnection(ConnectionString);
        DbConnection.Open();

        using var cmd = DbConnection.CreateCommand();
        cmd.CommandText =
            @"
                CREATE TABLE IF NOT EXISTS CodeSessions (
                    id INTEGER PRIMARY KEY,
                    StartTime TEXT,
                    EndTime TEXT,
                    Duration TEXT
                );
            ";
        cmd.ExecuteNonQuery();
    }
}
