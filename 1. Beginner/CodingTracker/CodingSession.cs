namespace CodingTracker;

public class CodingSession
{
    public int SessionId { get; }
    public DateTime Start { get; }
    public DateTime End { get; }
    public TimeSpan Duration { get; }

    public CodingSession(DateTime start, DateTime end)
    {
        Start = start;
        End = end;
        Duration = end - start;
    }

    public CodingSession(int sessionId, DateTime start, DateTime end, TimeSpan duration)
    {
        SessionId = sessionId;
        Duration = duration;
    }
}