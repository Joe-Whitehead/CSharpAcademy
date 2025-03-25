using Spectre.Console;
namespace CodingTracker;

internal class SessionController
{
    public bool AddSession(DateTime start, DateTime end)
    {
        Database db = new();
        var session = new CodingSession();
        session.Start = start;
        session.End = end;
        db.Insert(session);            
        return true;
    }

    public List<CodingSession> ViewAllSessions()
    {
        Database db = new();
        return db.GetAll();
    }
   
    public List<CodingSession> ViewByRange(DateTime StartDate, DateTime EndDate)
    {
        Database db = new();
        return new Database().GetRange(StartDate, EndDate);
    }

    public bool InsertTestData()
    {
        Database db = new();
        db.InsertTestData();
        return true;
    }
}