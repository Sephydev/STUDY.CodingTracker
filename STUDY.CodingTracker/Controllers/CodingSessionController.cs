using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Spectre.Console;
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

    public List<CodingSessionModel> GetCodingSessions()
    {
        List<CodingSessionModel> codingSessions = new();

        try
        {
            using var connection = new SqliteConnection(_connectionString);

            connection.Open();

            codingSessions = connection.Query<CodingSessionModel>("SELECT STARTTIME start, ENDTIME end, ID newId, DURATION newDuration FROM codingSessions").AsList();
        }
        catch (SqliteException e)
        {
            DBErrorMessage("getting the saved coding sessions", e.Message);
        }

        return codingSessions;
    }

    public void AddCodingSession(CodingSessionModel codingSession)
    {
        try
        {
            using var connection = new SqliteConnection(_connectionString);

            connection.Open();

            string sql = "INSERT INTO codingSessions (STARTTIME, ENDTIME, DURATION) VALUES (@StartTime, @EndTime, @Duration)";
            var parameters = new { @StartTime = codingSession.startTime, @EndTime = codingSession.endTime, @Duration = codingSession.duration };
            connection.Execute(sql, parameters);

            AnsiConsole.MarkupLine("[green]Coding session added successfully![/]");
        }
        catch (SqliteException e)
        {
            DBErrorMessage("adding the coding session", e.Message);
        }
    }

    public int DeleteCodingSession(int idToDelete)
    {
        int numberOfRowsDeleted = 0;

        try
        {
            using var connection = new SqliteConnection(_connectionString);

            connection.Open();

            numberOfRowsDeleted = connection.Execute("DELETE FROM codingSessions WHERE ID = @IdToDelete", new { @IdToDelete = idToDelete });
        }
        catch(SqliteException e)
        {
            DBErrorMessage("deleting the coding session", e.Message);
        }
        return numberOfRowsDeleted;
    }

    public int UpdateCodingSession(int idToUpdate, CodingSessionModel updatedCodingSession)
    {
        int numberOfRowsUpdated = 0;

        try
        {
            using var connection = new SqliteConnection(_connectionString);

            connection.Open();

            string sql = "UPDATE codingSessions SET STARTTIME = @StartTime, ENDTIME = @EndTime, DURATION = @Duration WHERE ID = @IdToUpdate";
            var parameters = new { @StartTime = updatedCodingSession.startTime, @EndTime = updatedCodingSession.endTime, @Duration = updatedCodingSession.duration, @IdToUpdate = idToUpdate };

            numberOfRowsUpdated = connection.Execute(sql, parameters);
        }
        catch (SqliteException e)
        {
            DBErrorMessage("updating the coding session", e.Message);
        }

        return numberOfRowsUpdated;
    }

    private void CreateTable()
    {
        try
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
        catch (SqliteException e)
        {
            DBErrorMessage("creating the DB Table", e.Message);
        }
    }

    private void DBErrorMessage(string action, string errorMessage)
    {
        Console.WriteLine($"An error occured while {action}. Error: {errorMessage}");
    }
}
