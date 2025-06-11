using Spectre.Console;
namespace CodingTracker;

internal class SessionController
{
    private readonly Database db = new();

    public bool AddSession(DateTime start, DateTime end)
    {
        var session = new CodingSession();
        session.Start = start;
        session.End = end;
        db.Insert(session);            
        return true;
    }

    public List<CodingSession> ViewAllSessions()
    {
        try
        {
            return db.GetAll();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[bold red]{ex.Message}[/]");
            return new List<CodingSession>();
        }
    }
   
    public List<CodingSession> ViewByRange(DateTime StartDate, DateTime EndDate)
    {
        try
        {
            return db.GetByRange(StartDate, EndDate);
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[bold red]{ex.Message}[/]");
            return new List<CodingSession>();
        }
    }

    public CodingSession ViewById(int id)
    {
        try
        {
            return db.GetById(id);
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[bold red]{ex.Message}[/]");   
            return new CodingSession();
        }
    }

    public CodingSession DeleteSession(int id)
    {
        var session = ViewById(id);
        if (session == null)
        {
            AnsiConsole.MarkupLine("[bold red]Session not found.[/]");
            return new CodingSession();
        }
        
        db.Delete(session);
        AnsiConsole.MarkupLine("[bold green]Session deleted successfully[/]");
        return session;
    }

    public bool InsertTestData()
    {
        db.InsertTestData();
        return true;
    }
}