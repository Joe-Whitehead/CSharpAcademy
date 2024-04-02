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
        cmd.CommandText = "CREATE TABLE Water(id INTEGER PRIMARY KEY, Quantity INTEGER, DateTime TEXT)";
        cmd.ExecuteNonQuery();
        cmd.CommandText = $"INSERT INTO Water(Quantity, DateTime) VALUES(2,'{DateTime.UtcNow}'), (3, '{DateTime.UtcNow.AddDays(-4)}')";
        cmd.ExecuteNonQuery();
        Console.WriteLine("Water Table Created");

        //Fruit Table
        cmd.CommandText = "DROP TABLE IF EXISTS Fruit";
        cmd.ExecuteNonQuery();
        cmd.CommandText = "CREATE TABLE Fruit(id INTEGER PRIMARY KEY, Quantity, DateTime TEXT)";
        cmd.ExecuteNonQuery();
        cmd.CommandText = $"INSERT INTO Fruit(Quantity, DateTime) VALUES(1, '{DateTime.UtcNow}'), (2, '{DateTime.UtcNow.AddDays(-2)}')";        
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        Console.WriteLine("Fruit Table Created");
    }

    public List<Habit> ViewAllRecords()
    {
        var habitTables = new List<Habit>();        
        string getHabits = "SELECT Name, Unit FROM Habits";
        using var habitCmd = new SQLiteCommand(getHabits, DbConnection);        
        using SQLiteDataReader habitRdr = habitCmd.ExecuteReader();
        
        while (habitRdr.Read()) {
            habitTables.Add(new Habit(habitRdr.GetString(0), habitRdr.GetString(1), new List<Entry>()));
        }
        habitRdr.Close();
        habitCmd.Dispose();     

        foreach (Habit habit in habitTables)
        {                    
            string getDetails =  "SELECT Quantity, DateTime FROM " + habit.HabitName;            
            using var detailCmd = new SQLiteCommand(getDetails, DbConnection);
            using SQLiteDataReader detailRdr = detailCmd.ExecuteReader();
            while (detailRdr.Read())
            {
                DateTime.TryParse(detailRdr.GetString(1), out var date);
                habit.Entries.Add(new Entry(detailRdr.GetInt32(0), date.Date.ToString("ddd (dd/MM/yy)")));                
            }
            detailRdr.Close();
            detailCmd.Dispose();
        }
        return habitTables;
    }

    public void Update()
    {

    }

    public void Delete()
    {

    }

    public record Habit (string HabitName, string Unit, List<Entry> Entries);
    public record Entry(int Quantity, string Date);
}
