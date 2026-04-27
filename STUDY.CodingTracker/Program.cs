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
CodingSessionModel codingSessionTest = new(startTime, endTime);
Console.WriteLine($"Time test: {codingSessionTest.duration}");

CodingSessionController codingSessionController = new(config);
codingSessionController.AddCodingSession(codingSessionTest);
