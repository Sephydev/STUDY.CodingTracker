namespace STUDY.CodingTracker.Models;

internal class CodingSessionModel
{
    public Int64 id { get; set; }
    public DateTime startTime { get; set; }
    public DateTime endTime { get; set; }
    public TimeSpan duration { get; }

    public CodingSessionModel (string start, string end, Int64 newId = 0, string newDuration = "-1")
    {
        id = newId;

        startTime = ConvertStringToDateTime(start);

        endTime = ConvertStringToDateTime(end);

        duration = newDuration == "-1" ? endTime.Subtract(startTime) : ConvertStringToTimeSpan(newDuration);
    }

    private DateTime ConvertStringToDateTime(string date)
    {
        DateTime convertedDate;

        DateTime.TryParse(date, out convertedDate);
        return convertedDate;
    }

    private TimeSpan ConvertStringToTimeSpan(string time)
    {
        TimeSpan convertedTime;

        TimeSpan.TryParse(time, out convertedTime);

        return convertedTime;
    }

}
