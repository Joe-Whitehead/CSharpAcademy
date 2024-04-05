 using System.Data.SQLite;

namespace HabitLogger;

internal class Database
{
    private readonly string _fileName = "Habit-Logger.db";
    private string _connectionString = "DataSource=Habit-Logger.db";
    public SQLiteConnection DbConnection = new SQLiteConnection();

    public Database()
    {
        DbConnection = new SQLiteConnection(_connectionString);
        DbConnection.Open();

        if(!File.Exists(_fileName) || new FileInfo(_fileName).Length == 0)
            CreateTablesIfNotExist();
    }

    public void CreateTablesIfNotExist()
    {
        using var cmd = new SQLiteCommand(DbConnection);

        //Habits Table
        cmd.CommandText = "DROP TABLE IF EXISTS Habits";
        cmd.ExecuteNonQuery();
        cmd.CommandText = "CREATE TABLE Habits(id INTEGER PRIMARY KEY, Name TEXT, Unit TEXT)";
        cmd.ExecuteNonQuery();
        cmd.CommandText = @"INSERT INTO Habits(Name, Unit) VALUES ('Water', 'Glasses'), ('Fruit', 'Portions')";
        cmd.ExecuteNonQuery();
        Console.WriteLine("Habits Table Created");

        //Water Table
        cmd.CommandText = "DROP TABLE IF EXISTS Water";
        cmd.ExecuteNonQuery();
        cmd.CommandText = "CREATE TABLE Water(id INTEGER PRIMARY KEY, Quantity INTEGER, Datetime TEXT)";
        cmd.ExecuteNonQuery();
        cmd.CommandText = $"INSERT INTO Water(Quantity, DateTime) VALUES(2,'{DateTime.UtcNow}'), (3, '{DateTime.UtcNow.AddDays(-4)}')";
        cmd.ExecuteNonQuery();
        Console.WriteLine("Water Table Created");

        //Fruit Table
        cmd.CommandText = "DROP TABLE IF EXISTS Fruit";
        cmd.ExecuteNonQuery();
        cmd.CommandText = "CREATE TABLE Fruit(id INTEGER PRIMARY KEY, Quantity INTEGER, Datetime TEXT)";
        cmd.ExecuteNonQuery();
        cmd.CommandText = $"INSERT INTO Fruit(Quantity, DateTime) VALUES(1, '{DateTime.UtcNow}'), (2, '{DateTime.UtcNow.AddDays(-2)}')";        
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        Console.WriteLine("Fruit Table Created");
    }

    public List<Habit> GetHabits()
    {
        var habits = new List<Habit>();
        string getHabits = "SELECT id, Name, Unit FROM Habits";
        using var cmd = new SQLiteCommand(getHabits, DbConnection);
        using SQLiteDataReader rdr = cmd.ExecuteReader();

        while (rdr.Read())
        {
            habits.Add(new Habit(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2), new List<Entry>()));
        }
        rdr.Close();
        cmd.Dispose();

        return habits;
    }

    public List<Entry> GetHabitRecords(Habit habit)
    {
        string getDetails = "SELECT id, Quantity, Datetime FROM " + habit.HabitName;
        using var cmd = new SQLiteCommand(getDetails, DbConnection);
        using SQLiteDataReader rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
            DateTime.TryParse(rdr.GetString(2), out var date);
            habit.Entries.Add(new Entry(rdr.GetInt32(0), rdr.GetInt32(1), date));
        }
        rdr.Close();
        cmd.Dispose();

        return habit.Entries;
    }

    public List<Habit> ViewAllRecords()
    {
        var habitTables = GetHabits();
        foreach (Habit habit in habitTables)
        {
            GetHabitRecords(habit);
        }
        return habitTables;
    }

    public void insert(Habit habit, int quantity)
    {
        using var cmd = new SQLiteCommand(DbConnection);
        cmd.CommandText = $"INSERT INTO {habit.HabitName}(Quantity, Datetime) VALUES ({quantity}, '{DateTime.UtcNow}')";                      
        cmd.ExecuteNonQuery();
        cmd.Dispose();

    }

    public void Update()
    {

    }

    public void DeleteHabit(Habit habit)
    {
        using var cmd = new SQLiteCommand(DbConnection);
        cmd.CommandText = $"DELETE FROM Habits Where Name =  '{habit.HabitName}'";
        cmd.ExecuteNonQuery();

        cmd.CommandText = $"DROP TABLE IF EXISTS {habit.HabitName}";
        cmd.ExecuteNonQuery();
        cmd.Dispose();
    }

    public void DeleteRecord(Habit habit, Entry entry)
    {
        using var cmd = new SQLiteCommand(DbConnection);
        cmd.CommandText = $"DELETE FROM {habit.HabitName} Where id = '{entry.id}'";
    }

    public record Habit (int id, string HabitName, string Unit, List<Entry> Entries);
    public record Entry(int id, int Quantity, DateTime Date);
}
