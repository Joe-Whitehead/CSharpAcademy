using Spectre.Console;
using System.Globalization;
namespace CodingTracker;

internal class CodingSessionView
{
    public void Title()
    {
        AnsiConsole.MarkupLine("[bold cyan][[Coding Tracker]][/]");
    }

    public void PageTitle(string title)
    {
        AnsiConsole.MarkupLine($"""
            [bold]{title}[/]
            ----------------
            """);
            
    }

    public void Run()
    {
        Database db = new();
        SessionController sessionController = new();
        Validation dateTimeValidator = new();
        Title();
        while (true)
        {                       
            MenuOption selectedOption = MainMenu();
            switch (selectedOption)
            {
                case MenuOption.AddSession:
                    PageTitle("Add Session");
                    DateTime startDateTime, endDateTime;
                    while (true)
                    {
                        try
                        {
                            startDateTime = dateTimeValidator.GetValidatedDateTime("Enter start date (dd-MM-yyyy): ", "Enter start time (HH:mm): ");
                            endDateTime = dateTimeValidator.GetValidatedDateTime("Enter end date (dd-MM-yyyy): ", "Enter end time (HH:mm): ");
                            dateTimeValidator.ValidateDateTimeRange(startDateTime, endDateTime);

                            // Break out of the loop if inputs are valid
                            break;
                        }
                        catch (Exception e)
                        {
                            AnsiConsole.MarkupLine($"[red]{e.Message} Please re-enter the values.[/]");
                        }
                    }

                    // Add the session after successful validation
                    sessionController.AddSession(startDateTime, endDateTime);
                    AnsiConsole.MarkupLine("[bold green]Session added successfully[/]");

                    break;
                case MenuOption.ViewAllSessions:
                    AnsiConsole.MarkupLine("[bold]View All Sessions[/]");
                    break;
                case MenuOption.ViewByRange:
                    AnsiConsole.MarkupLine("[bold]View By Range[/]");
                    break;
                case MenuOption.ViewById:
                    AnsiConsole.MarkupLine("[bold]View By Id[/]");
                    break;
                case MenuOption.UpdateSession:
                    AnsiConsole.MarkupLine("[bold]Update Session[/]");
                    break;
                case MenuOption.DeleteSession:
                    AnsiConsole.MarkupLine("[bold]Delete Session[/]");
                    break;
                case MenuOption.Exit:
                    Environment.Exit(0);
                    return;
            }
            Console.ReadLine();
            Console.Clear();
        }
    }

    public MenuOption MainMenu()
    {
        AnsiConsole.MarkupLine("""
            [Bold]Main Menu[/]
            ----------------
            [green]1[/] Add Session
            [green]2[/] View All Sessions
            [green]3[/] View By Range
            [green]4[/] View By Id
            [green]5[/] Update Session
            [green]6[/] Delete Session
            [red]7[/] Exit
            """);

       while (true)
       if (int.TryParse(AnsiConsole.Ask<string>("[green]Enter your choice[/]"), out int choice) && Enum.IsDefined(typeof(MenuOption), choice))
        {
                Console.Clear();
                return (MenuOption)choice;
        }
       else 
        {
                Console.Clear();
                AnsiConsole.MarkupLine("[red]Invalid choice[/]");
        }
    }  
}
enum MenuOption{AddSession = 1, ViewAllSessions, ViewByRange, ViewById, UpdateSession, DeleteSession, Exit}
