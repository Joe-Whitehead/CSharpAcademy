using Spectre.Console;
using System.Globalization;
namespace CodingTracker;

internal class CodingSessionView
{
    public static void Title()
    {
        AnsiConsole.MarkupLine("[bold cyan][[Coding Tracker]][/]");
    }

    public static void PageTitle(string title)
    {
        AnsiConsole.MarkupLine($"""
            [bold]{title}[/]
            ----------------
            """);
            
    }

    public void Run()
    {
        SessionController sessionController = new();
        Validation dateTimeValidator = new();
        Title();
        while (true)
        {
            DateTime startDateTime, endDateTime;
            MenuOption selectedOption = MainMenu();
            switch (selectedOption)
            {
                case MenuOption.AddSession:
                    PageTitle("Add Session");                    
                    while (true)
                    {
                        try
                        {
                            startDateTime = GetValidatedDateTime(dateTimeValidator, "Enter start date (dd-MM-yyyy): ", "Enter start time (HH:mm): ");
                            endDateTime = GetValidatedDateTime(dateTimeValidator, "Enter end date (dd-MM-yyyy): ", "Enter end time (HH:mm): ");
                            if (!dateTimeValidator.ValidateDateTimeRange(startDateTime, endDateTime))
                            {
                                throw new ArgumentException("Invalid date range.");
                            }
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
                    PageTitle("View All Sessions");
                    List<CodingSession> allSessions = sessionController.ViewAllSessions();
                    if (allSessions.Count == 0)
                    {
                        AnsiConsole.MarkupLine("[bold red]No sessions found[/]");
                    }
                    else
                    {
                        foreach (var session in allSessions)
                        {
                            AnsiConsole.MarkupLine($"[bold]Session Id:[/] [cyan]{session.SessionId}[/]");
                            AnsiConsole.MarkupLine($"[bold]Start Time:[/] {session.Start:f}");
                            AnsiConsole.MarkupLine($"[bold]End Time:[/] {session.End:f}");
                            AnsiConsole.MarkupLine($"[bold]Duration:[/] [yellow]{session.Duration:hh\\:mm}[/]");
                            AnsiConsole.MarkupLine("");
                        }
                    }
                    break;

                case MenuOption.ViewByRange:
                    AnsiConsole.MarkupLine("[bold]View By Range[/]");

                    startDateTime = GetValidatedDateTime(dateTimeValidator, "Enter start date (dd-MM-yyyy): ");
                    endDateTime = GetValidatedDateTime(dateTimeValidator, "Enter end date (dd-MM-yyyy): ");
                    if (!dateTimeValidator.ValidateDateTimeRange(startDateTime, endDateTime))
                    {
                        Database db = new();
                        List<CodingSession> sessionRange = sessionController.ViewByRange(startDateTime, endDateTime);
                        if (sessionRange.Count == 0)
                        {
                            AnsiConsole.MarkupLine("[bold red]No sessions found[/]");
                        }
                        else
                        {
                            foreach (var session in sessionRange)
                            {
                                AnsiConsole.MarkupLine($"[bold]Session Id:[/] {session.SessionId}");
                                AnsiConsole.MarkupLine($"[bold]Start Time:[/] {session.Start}");
                                AnsiConsole.MarkupLine($"[bold]End Time:[/] {session.End}");
                                AnsiConsole.MarkupLine($"[bold]Duration:[/] {session.Duration:hh\\:mm}");
                            }
                        }
                    }
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
                case MenuOption.InsertTestData:
                    AnsiConsole.MarkupLine("[bold]Inserting test data[/]");
                    if(sessionController.InsertTestData())
                    {
                        AnsiConsole.MarkupLine("[bold green]Test data inserted successfully[/]");
                    }
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
            [green]7[/] Insert Test Data
            [red]8[/] Exit
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
    private DateTime GetValidatedDateTime(Validation validator, string datePrompt, string timePrompt = "00:00")
    {
        while (true)
        {
            Console.Write(datePrompt);
            string date = Console.ReadLine()!;
            if (!validator.ValidateDate(date))
            {
                AnsiConsole.MarkupLine("[red]Invalid date format. Please try again.[/]");
                continue;
            }

            Console.Write(timePrompt);
            string time = Console.ReadLine()!;
            if (!validator.ValidateTime(time))
            {
                AnsiConsole.MarkupLine("[red]Invalid time format. Please try again.[/]");
                continue;
            }

            return DateTime.ParseExact($"{date} {time}", "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
        }
    }
}
enum MenuOption{AddSession = 1, ViewAllSessions, ViewByRange, ViewById, UpdateSession, DeleteSession, InsertTestData, Exit}
