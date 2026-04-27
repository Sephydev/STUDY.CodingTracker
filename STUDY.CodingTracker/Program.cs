using Microsoft.Extensions.Configuration;
using STUDY.CodingTracker.Controllers;
using STUDY.CodingTracker.Models;

IConfiguration config = new ConfigurationBuilder()
.SetBasePath(Directory.GetCurrentDirectory())
.AddJsonFile("appsettings.json", optional: false,
    reloadOnChange: true)
.Build();

DateTime startTime;
DateTime endTime;

DateTime.TryParse("01:12:27", out startTime);
DateTime.TryParse("02:57:46", out endTime);
CodingSessionModel codingSessionTest = new(0, "01:12:27", "02:57:46");
Console.WriteLine($"Time test: {codingSessionTest._duration}");

CodingSessionController codingSessionController = new(config);
codingSessionController.AddCodingSession(codingSessionTest);

foreach(var codingSession in codingSessionController.GetCodingSessions())
{
    Console.WriteLine($"{codingSession._id} | {codingSession._startTime.ToString("dd-MMM-yyyy")} | {codingSession._startTime.ToString("hh:mm:ss")} | {codingSession._endTime.ToString("hh:mm:ss")} | {codingSession._duration}");
}
