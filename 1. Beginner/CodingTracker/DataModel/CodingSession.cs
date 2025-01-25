namespace CodingTracker.DataModel;

internal class CodingSession(DateTime start, DateTime end)
{
    public int SessionId { get; set; }
    public DateTime StartTime { get; } = start;
    public DateTime EndTime { get; } = end;
    public TimeSpan Duration { get; init; } = end - start;
}
