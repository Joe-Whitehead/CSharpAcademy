﻿using ConsoleTables;
using HabitLogger;
using static HabitLogger.Database;
bool endAPp = false;
Database db = new Database();

while (!endAPp)
{    
    int menuSelection;
    int userQuantity;
    Habit userHabit;

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
            Console.WriteLine("View Records \n");
            DisplayRecords();
            break;

        case 2:
            Console.WriteLine("Insert Record");
            Console.WriteLine("-------------");

            userHabit = HabitMenu();

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(userHabit.HabitName);
            Console.ResetColor();
            Console.WriteLine("------");            
            Console.Write($"Write Quantity in {userHabit.Unit}: ");
            userQuantity = ValidateMenu();
            if (userQuantity < 1)
            {
                Console.WriteLine("Invalid number - Returning to Main Menu");
                break;
            }
            db.insert(userHabit, userQuantity);
            Console.WriteLine($"Added {userQuantity} {userHabit.Unit} to {userHabit.HabitName}");
            break;

        case 3:
            //TODO: Delete Record
            Console.WriteLine("Delete Record");            

            Console.WriteLine("""
                1 - Delete Habit & Records
                2 - Delete Record
                """);
            menuSelection = ValidateMenu();
            
            switch (menuSelection)
            {
                case 1:
                    userHabit = HabitMenu();
                    db.DeleteHabit(userHabit);
                    break;

                case 2:
                    //db.DeleteRecord(userHabit, userRecord);
                    break;
            }

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

Habit HabitMenu()
{
    var habits = db.GetHabits();
    int i = 1;
    foreach (var habit in habits)
    {
        Console.WriteLine($"{i} - {habit.HabitName}");
        i++;
    }
    Console.Write("\nSelect Habit: ");
    var menuSelection = ValidateMenu();

    return habits[menuSelection - 1];
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
            table.AddRow($"{row.Quantity} {record.Unit}", row.Date.Date.ToString("ddd (dd/MM/yy)"));
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