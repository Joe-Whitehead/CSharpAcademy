using Spectre.Console;
namespace CodingTracker;

internal class SessionController
{
    private Database db = new();

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
        List<CodingSession> sessions = db.GetAll();
        return sessions.Where(s => s.Start >= StartDate && s.End <= EndDate).ToList();
        
    }

    public CodingSession ViewById(int id)
    {
        return db.GetById(id);
    }

    public bool InsertTestData()
    {
        db.InsertTestData();
        return true;
    }
}