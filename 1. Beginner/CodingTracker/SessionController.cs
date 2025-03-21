using Spectre.Console;
namespace CodingTracker;

internal class SessionController
{
    public bool AddSession(DateTime start, DateTime end)
    {
            Database db = new();
            db.Insert(new CodingSession(start, end));            
            return true;
    }

    public void ViewAllSessions()
    {
        AnsiConsole.MarkupLine("[bold]View All Sessions[/]");
        Database db = new();
        var sessions = db.GetAll();
        if (sessions.Count == 0)
        {
            AnsiConsole.MarkupLine("[bold red]No sessions found[/]");
        }
        else
        {
            foreach (var session in sessions)
            {
                AnsiConsole.MarkupLine($"[bold]Session Id:[/] {session.SessionId}");
                AnsiConsole.MarkupLine($"[bold]Start Time:[/] {session.Start}");
                AnsiConsole.MarkupLine($"[bold]End Time:[/] {session.End}");
                AnsiConsole.MarkupLine($"[bold]Duration:[/] {session.Duration}");
                AnsiConsole.MarkupLine("");
            }
        }
    }
    public void ViewByRange()
    {
        AnsiConsole.MarkupLine("[bold]View By Range[/]");
        string start = AnsiConsole.Ask<string>("Enter start time (yyyy-MM-dd HH:mm:ss): ");
        string end = AnsiConsole.Ask<string>("Enter end time (yyyy-MM-dd HH:mm:ss): ");
        if (Validation.ValidateDate(start) && Validation.ValidateDate(end))
        {
            Database db = new();
            var sessions = db.GetRange(DateTime.Parse(start), DateTime.Parse(end));
            if (sessions.Count == 0)
            {
                AnsiConsole.MarkupLine("[bold red]No sessions found[/]");
            }
            /*else
            {
                foreach (var session in sessions)
                {
                    AnsiConsole.MarkupLine($"[bold]Session Id:[/] {session.SessionId}");
                    AnsiConsole.MarkupLine($"[bold]Start Time:[/] {session.StartTime}");
                    AnsiConsole.Mark*/
        }
    }
}