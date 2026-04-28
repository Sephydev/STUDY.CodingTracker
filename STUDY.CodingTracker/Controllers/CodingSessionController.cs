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

    public List<CodingSessionModel> GetCodingSessions()
    {
        using var connection = new SqliteConnection(_connectionString);

        connection.Open();

        var codingSessions = connection.Query<CodingSessionModel>("SELECT ID id, STARTTIME startTime, ENDTIME endTime FROM codingSessions").AsList();

        return codingSessions;
    }

    // Add

    public void AddCodingSession(CodingSessionModel codingSession)
    {
        using var connection = new SqliteConnection(_connectionString);

        connection.Open();

        string sql = "INSERT INTO codingSessions (STARTTIME, ENDTIME, DURATION) VALUES (@StartTime, @EndTime, @Duration)";
        var parameters = new { @StartTime = codingSession._startTime, @EndTime = codingSession._endTime, @Duration = codingSession._duration };
        connection.Execute(sql, parameters);
    }

    // Delete

    public int DeleteCodingSession(int idToDelete)
    {
        using var connection = new SqliteConnection(_connectionString);

        connection.Open();

        int numberOfRows = connection.Execute("DELETE FROM codingSessions WHERE ID = @IdToDelete", new { @IdToDelete = idToDelete });

        return numberOfRows;
    }

    // Update

    public int UpdateCodingSession(int idToUpdate, CodingSessionModel updatedCodingSession)
    {
        using var connection = new SqliteConnection(_connectionString);

        connection.Open();

        string sql = "UPDATE codingSessions SET STARTTIME = @StartTime, ENDTIME = @EndTime, DURATION = @Duration WHERE ID = @IdToUpdate";
        var parameters = new { @StartTime = updatedCodingSession._startTime, @EndTime = updatedCodingSession._endTime, @Duration = updatedCodingSession._duration, @IdToUpdate = idToUpdate };

        int numberOfRows = connection.Execute(sql, parameters);

        return numberOfRows;
    }

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
