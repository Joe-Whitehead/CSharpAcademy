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
        return db.GetAll();
    }
   
    public List<CodingSession> ViewByRange(DateTime StartDate, DateTime EndDate)
    {
        //return db.Where(s => s.Start >= StartDate && s.End <= EndDate).ToList();
        return db.GetByRange(StartDate, EndDate);
    }

    public CodingSession? ViewById(int id)
    {
        try
        {
            return db.GetById(id);
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[bold red]{ex.Message}[/]");   
            return null;
        }
    }

    public CodingSession? DeleteSession(int id)
    {
        var session = db.GetById(id);
        if (session == null)
        {
            AnsiConsole.MarkupLine("[bold red]Session not found.[/]");
            return null;
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