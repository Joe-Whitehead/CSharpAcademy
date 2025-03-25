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

    public void InsertTestData()
    {   
        string delete = "DROP TABLE CodeSessions;";
        DbConnection.Execute(delete);

        CreateIfNotExist();
        for (int i = 0; i < 30; i++)
        {
            Dates dates = RandomDates();
            string sql = $"INSERT INTO CodeSessions (StartDate, EndDate) VALUES (@Start, @End);";
            DbConnection.Execute(sql,dates);
        }
    }

    private class Dates
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }

     private Dates RandomDates()
    {
        Random random = new Random();
        DateTime rangeStart = new DateTime(2024, 6, 1); // Start of the range
        DateTime rangeEnd = new DateTime(2025, 3, 31); // End of the range
        TimeSpan range = rangeEnd - rangeStart;

            // Generate random start time within the range
            TimeSpan randomStartSpan = new TimeSpan((long)(random.NextDouble() * range.Ticks));
            DateTime startTime = rangeStart + randomStartSpan;

            // Generate random duration to calculate end time
            TimeSpan duration = new TimeSpan(0, random.Next(1, 24 * 60), 0); // Random duration in minutes
            DateTime endTime = startTime + duration;

            // Ensure end time is within the range
            if (endTime > rangeEnd)
            {
                endTime = rangeEnd; // Clamp to rangeEnd if it exceeds the limit                
            }
            return new Dates { Start = startTime, End = endTime };
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
        return CalculateDuration(sessions);
    }

    public List<CodingSession> GetRange(DateTime start, DateTime end)
    {
        string sql = "SELECT * FROM CodeSessions WHERE StartDate >= @start AND EndDate <= @end;";
        var sessions = DbConnection.Query<CodingSession>(sql, new { start = start.ToString("yyyy-MM-dd"), end = end.ToString("yyyy-MM-dd") }).ToList();
        return CalculateDuration(sessions);
    }

    private List<CodingSession> CalculateDuration(List<CodingSession> sessions)
    {
        foreach (var session in sessions)
        {
            session.Duration = session.End - session.Start;
        }
        return sessions;
    }
}