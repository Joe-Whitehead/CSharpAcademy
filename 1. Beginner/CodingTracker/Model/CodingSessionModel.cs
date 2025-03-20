namespace CodingTracker.Model;

internal class CodingSessionModel(DateTime start, DateTime end)
{
    public int SessionId { get; set; }
    public DateTime StartTime { get; } = start;
    public DateTime EndTime { get; } = end;
    public TimeSpan Duration { get; init; } = end - start;
}
