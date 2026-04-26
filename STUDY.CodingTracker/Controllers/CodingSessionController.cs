using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

    // Delete

    // Update

    private void CreateTable()
    {
        using var connection = new SqliteConnection(_connectionString);

        connection.Open();

        var codingSessionTable = connection.Execute(
            @"CREATE TABLE IF NOT EXISTS codingSessions(
            ID INTEGER PRIMARY KEY AUTOINCREMENT,
            STARTTIME TEXT,
            ENDTIME TEXT,
            DURATION TEXT
            )"
        );
    }
}
