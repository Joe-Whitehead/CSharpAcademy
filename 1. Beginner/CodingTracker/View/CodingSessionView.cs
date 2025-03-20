using Spectre.Console;
using CodingTracker.Controller;
namespace CodingTracker.View;

internal class CodingSessionView
{
    public void Title()
    {
        AnsiConsole.MarkupLine("[bold cyan][[Coding Tracker]][/]");
    }

    public void Run()
    {
        Database db = new();
        while (true)
        {                       
            MenuOption selectedOption = MainMenu();
            switch (selectedOption)
            {
                case MenuOption.AddSession:
                    AnsiConsole.MarkupLine("[bold]Add Session[/]");                    
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
