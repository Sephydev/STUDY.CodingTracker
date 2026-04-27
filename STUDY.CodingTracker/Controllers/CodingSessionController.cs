using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using STUDY.CodingTracker.Models;

namespace STUDY.CodingTracker.Controllers;

internal class CodingSessionController
{
    private readonly IConfiguration _config;
    private readonly string _connectionString;

    public CodingSessionController(IConfiguration config)
    {
        _config = config;
        var section = _config.GetSection("DataBaseSettings");
        _connectionString = section["connectionString"] + section["databasePath"];

        CreateTable();
    }

    // View

    // Add

    public void AddCodingSession(CodingSessionModel codingSession)
    {
        using var connection = new SqliteConnection(_connectionString);

        connection.Open();

        string sql = "INSERT INTO codingSessions (STARTTIME, ENDTIME, DURATION) VALUES (@StartTime, @EndTime, @Duration)";
        var parameters = new { @StartTime = codingSession.startTime, @EndTime = codingSession.endTime, @Duration = codingSession.duration };
        connection.Execute(sql, parameters);
    }

    // Delete

    // Update

    private void CreateTable()
    {
        using var connection = new SqliteConnection(_connectionString);

        connection.Open();

        connection.Execute(
            @"CREATE TABLE IF NOT EXISTS codingSessions(
            ID INTEGER PRIMARY KEY AUTOINCREMENT,
            STARTTIME TEXT,
            ENDTIME TEXT,
            DURATION TEXT
            )"
        );
    }
}
