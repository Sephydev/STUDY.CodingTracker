namespace STUDY.CodingTracker.Models;

internal class CodingSessionModel
{
    public int id { get; set; }
    public DateTime startTime { get; set; }
    public DateTime endTime { get; set; }
    public TimeSpan duration { get; }

    public CodingSessionModel(DateTime start, DateTime end)
    {
        startTime = start;
        endTime = end;

        duration = endTime.Subtract(startTime);
    }
}
