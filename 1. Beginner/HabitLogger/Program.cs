using ConsoleTables;
using HabitLogger;
bool endAPp = false;
Database db = new Database();

while (!endAPp)
{    
    int menuSelection;

    Console.WriteLine("""
        Habit Tracker
        -------------
        MAIN MENU

        0 - Close Application
        1 - View All Records
        2 - Insert Record
        3 - Delete Record
        4 - Update Record
        -------------
        """);
    menuSelection = ValidateMenu();
    Console.Clear();

    switch (menuSelection)
    {
        case 0:
            endAPp = true;
            Console.WriteLine("Goodbye");
            break;

        case 1:            
            //TODO: View Records
            Console.WriteLine("View Records \n");
            //Console.WriteLine();
            DisplayRecords();
            break;
        case 2:
            //TODO: Inseert Record
            Console.WriteLine("Insert Record");
            break;
        case 3:
            //TODO: Delete Record
            Console.WriteLine("Delete Record");
            break;
        case 4:
            //TODO: Update Record
            Console.WriteLine("Update Record");
            break;
    }
    Console.WriteLine("Press any key to continue");
    Console.ReadKey(true);
    Console.Clear();
}

void DisplayRecords()
{
    var habits = db.ViewAllRecords();        
    foreach (var record in habits)
    {
        var table = new ConsoleTable("Quantity", "Unit");

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(record.HabitName);
        Console.ResetColor();

        foreach (var row in record.Entries)
        {
            table.AddRow($"{row.Quantity} {record.Unit}", row.Date);
        }        
        table.Write(Format.MarkDown);
        Console.WriteLine();
    }
}

int ValidateMenu()
{
    string? input = Console.ReadLine();
    int validNumber;
    while (!int.TryParse(input, out validNumber))
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("Value must be a number: ");
        Console.ResetColor();
        input = Console.ReadLine();
    }
    return validNumber;
}