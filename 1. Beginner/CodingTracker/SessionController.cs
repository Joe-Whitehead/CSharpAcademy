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

    public List<CodingSession> ViewAllSessions()
    {
        Database db = new();
        return db.GetAll();
    }
   /* public void ViewByRange()
    {
        AnsiConsole.MarkupLine("[bold]View By Range[/]");
        string start = AnsiConsole.Ask<string>("Enter start time (yyyy-MM-dd HH:mm:ss): ");
        string end = AnsiConsole.Ask<string>("Enter end time (yyyy-MM-dd HH:mm:ss): ");
        if (ValidateDate(start) && ValidateDate(end))
        {
            Database db = new();
            var sessions = db.GetRange(DateTime.Parse(start), DateTime.Parse(end));
            if (sessions.Count == 0)
            {
                AnsiConsole.MarkupLine("[bold red]No sessions found[/]");
            }
            else
            {
                foreach (var session in sessions)
                {
                    AnsiConsole.MarkupLine($"[bold]Session Id:[/] {session.SessionId}");
                    AnsiConsole.MarkupLine($"[bold]Start Time:[/] {session.StartTime}");
                    AnsiConsole.Mark
        }
   }*/
}