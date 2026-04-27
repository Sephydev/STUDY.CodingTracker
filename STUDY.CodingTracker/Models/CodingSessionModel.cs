namespace STUDY.CodingTracker.Models;

internal class CodingSessionModel
{
    public int _id { get; set; }
    public DateTime _startTime { get; set; }
    public DateTime _endTime { get; set; }
    public TimeSpan _duration { get; }

    public CodingSessionModel (Int64 id, string startTime, string endTime)
    {
        _id = (int)id;

        DateTime temp;

        DateTime.TryParse(startTime, out temp);
        _startTime = temp;

        DateTime.TryParse(endTime, out temp);
        _endTime = temp;

        _duration = _endTime.Subtract(_startTime);
    }

}
