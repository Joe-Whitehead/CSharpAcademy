using Dapper;
using System.Data.SQLite;

namespace CodingTracker;

internal class Database
{
    private readonly string _databasePath;
    private readonly string _connectionString;
    private readonly SQLiteConnection DbConnection;

    public Database()
    {
        _databasePath = System.Configuration.ConfigurationManager.AppSettings.Get("DatabasePath") ?? "";
        _connectionString = System.Configuration.ConfigurationManager.AppSettings.Get("ConnectionString") ?? "";          
        DbConnection = new SQLiteConnection(_connectionString);
        DbConnection.Open();

        if (!File.Exists(_databasePath) || new FileInfo(_databasePath).Length == 0 || GetAll().Count < 1)
            CreateIfNotExist();
    }

    public void CreateIfNotExist()
    {
        string createTable = @"
            CREATE TABLE IF NOT EXISTS CodeSessions (
                Id INTEGER PRIMARY KEY,
                StartDate TEXT,
                EndDate TEXT
            );
        ";
        DbConnection.Execute(createTable);
    }

    public void Insert(CodingSession session)
    {
        string sql = "INSERT INTO CodeSessions (StartDate, EndDate) VALUES (@Start, @End);";
        DbConnection.Execute(sql, session);
    }

    public void Delete(CodingSession session)
    {
        string sql = "DELETE FROM CodeSessions WHERE id = @id;";
        DbConnection.Execute(sql, session);
    }

    public void Update(CodingSession session)
    {
        string sql = "UPDATE CodeSessions SET StartDate = @Start, EndDate = @End WHERE id = @id;";
        DbConnection.Execute(sql, session);
    }

        public List<CodingSession> GetAll()
    {
        string sql = "SELECT Id as SessionId, StartDate as Start, EndDate as End FROM CodeSessions;";
        var sessions = DbConnection.Query<CodingSession>(sql).ToList();

        // Calculate Duration for each session
        foreach (var session in sessions)
        {
            session.Duration = session.End - session.Start;
        }

        return sessions;
    }

    public List<CodingSession> GetRange(DateTime start, DateTime end)
    {
        string sql = "SELECT * FROM CodeSessions WHERE StartDate >= @start AND EndDate <= @end;";
        return DbConnection.Query<CodingSession>(sql, new { start = start.ToString("yyyy-MM-dd"), end = end.ToString("yyyy-MM-dd") }).ToList();
    }
}