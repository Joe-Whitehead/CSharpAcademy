namespace CodingTracker;

public class CodingSession(DateTime start, DateTime end)
{
    public DateTime Start { get; } = start;
    public DateTime End { get; } = end;
    public TimeSpan Duration { get; init; } = end - start;
}