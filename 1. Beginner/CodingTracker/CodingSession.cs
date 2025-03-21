namespace CodingTracker;

internal class CodingSession(DateTime start, DateTime end)
{
    public int SessionId { get; set; }
    public DateTime Start { get; } = start;
    public DateTime End { get; } = end;
    public TimeSpan Duration { get; init; } = end - start;
}
