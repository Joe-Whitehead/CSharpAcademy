using System.Data.SQLite;

namespace HabitLogger;

internal class Database
{
    private string _connectionString = "DataSource=Habit-Logger.db";
    public SQLiteConnection DbConnection = new SQLiteConnection();

    public Database()
    {
        DbConnection = new SQLiteConnection(_connectionString);
        DbConnection.Open();      
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
        cmd.CommandText = $"INSERT INTO Water(Quantity, DateTime) VALUES(2,'{DateTime.UtcNow}')";
        cmd.ExecuteNonQuery();
        Console.WriteLine("Water Table Created");

        //Fruit Table
        cmd.CommandText = "DROP TABLE IF EXISTS Fruit";
        cmd.ExecuteNonQuery();
        cmd.CommandText = "CREATE TABLE Fruit(id INTEGER PRIMARY KEY, Quantity, DateTime TEXT)";
        cmd.ExecuteNonQuery();
        cmd.CommandText = $"INSERT INTO Fruit(Quantity, DateTime) VALUES(1, '{DateTime.UtcNow}')";
    }

    public void Update()
    {

    }

    public void Delete()
    {

    }

}
