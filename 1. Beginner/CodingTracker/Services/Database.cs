using System.Data.Common;
using System.Data.SQLite;
using CodingTracker.DataModel;
using Dapper;
using Spectre.Console.Rendering;

namespace CodingTracker.Services;

internal class Database
{
    private readonly string _databasePath;
    private readonly string _connectionString;
    private readonly SQLiteConnection DbConnection;

    public Database()
    {
        _databasePath = System.Configuration.ConfigurationManager.AppSettings.Get("DatabasePath") ?? "";
        _connectionString = System.Configuration.ConfigurationManager.AppSettings.Get("ConnectionString") ?? "";          
        DbConnection= new SQLiteConnection(_connectionString);
        DbConnection.Open();

        if (!File.Exists(_databasePath) || new FileInfo(_databasePath).Length == 0 || GetAll().Count < 1)
            CreateIfNotExist();
    }

    public void CreateIfNotExist()
    {
        string createTable = @"
            CREATE TABLE IF NOT EXISTS CodeSessions (
                id INTEGER PRIMARY KEY,
                StartTime TEXT,
                StartDate TEXT,
                EndTime TEXT,
                EndDate TEXT,
                Duration TEXT
            );
        ";
        DbConnection.Execute(createTable);
    }

    public void Insert(CodingSession session)
    {
        string sql = "INSERT INTO CodeSessions (StartTime, StartDate, EndTime, EndDate, Duration) VALUES (@StartTime, @StartDate, @EndTime, @EndDate, @Duration);";
        DbConnection.Execute(sql, session);
    }

    public List<CodingSession> GetAll()
    {
        string sql = "SELECT * FROM CodeSessions;";
        return DbConnection.Query<CodingSession>(sql).ToList();
    }

    public void Delete(CodingSession session)
    {
        string sql = "DELETE FROM CodeSessions WHERE id = @id;";
        DbConnection.Execute(sql, session);
    }

    public void Update(CodingSession session)
    {
        string sql = "UPDATE CodeSessions SET StartTime = @StartTime, StartDate = @StartDate, EndTime = @EndTime, EndDate = @EndDate, Duration = @Duration WHERE id = @id;";
        DbConnection.Execute(sql, session);
    }
}