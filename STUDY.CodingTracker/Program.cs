using Microsoft.Extensions.Configuration;

using STUDY.CodingTracker.Models;
using STUDY.CodingTracker.Configuration;

IConfiguration config = new ConfigurationBuilder()
.SetBasePath(Directory.GetCurrentDirectory())
.AddJsonFile("appsettings.json", optional: false,
    reloadOnChange: true)
.Build();

var databaseSettings = config.GetSection("DatabaseSettings");

Configuration myConfig = new Configuration();
databaseSettings.Bind(myConfig);
Console.WriteLine($"Connection string: {myConfig.ConnectionString + myConfig.DatabasePath}");

DateTime startTime;
DateTime endTime;

DateTime.TryParse("01:12:27", out startTime);
DateTime.TryParse("02:57:46", out endTime);
CodingSessionModel codingSessionTest = new(startTime, endTime);
Console.WriteLine($"Time test: {codingSessionTest.duration}");