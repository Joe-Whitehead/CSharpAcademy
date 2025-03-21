namespace CodingTracker;

internal class CodingSession
{
    public int SessionId { get; set; }
    public DateTime Start { get; }
    public DateTime End { get; }
    public TimeSpan Duration { get; init; }

    public CodingSession() { }

    public CodingSession(DateTime start, DateTime end)
    {
        Start = start;
        End = end;
        Duration = end - start;
    }

}