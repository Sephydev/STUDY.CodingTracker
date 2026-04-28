using Microsoft.Extensions.Configuration;
using Spectre.Console;
using STUDY.CodingTracker.Controllers;
using STUDY.CodingTracker.Models;

namespace STUDY.CodingTracker;

internal class UserInterface
{
    private CodingSessionController _codingSessionController;

    public UserInterface()
    {
        // Can't move it to CodingSessionController because of "Directory.GetCurrentDirectory()"
        IConfiguration config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false,
            reloadOnChange: true)
        .Build();

        _codingSessionController = new CodingSessionController(config);
    }

    public void MainMenu()
    {
        while (true)
        {
            string choice = DisplayMainMenu();

            switch (choice)
            {
                case "View Coding Sessions":
                    DisplayCodingSessionsTable();
                    AnsiConsole.MarkupLine("Press Any Key to Continue.");
                    Console.ReadKey();
                    break;
                case "Add Coding Session":
                    AnsiConsole.MarkupLine($"'{choice}' is under construction. Please try again later.");
                    Console.ReadKey();
                    break;
                case "Delete Coding Session":
                    AnsiConsole.MarkupLine($"'{choice}' is under construction. Please try again later.");
                    Console.ReadKey();
                    break;
                case "Update Coding Session":
                    AnsiConsole.MarkupLine($"'{choice}' is under construction. Please try again later.");
                    Console.ReadKey();
                    break;
                case "Exit the application":
                    AnsiConsole.MarkupLine("Thank you for using the app! See you soon!");
                    Console.ReadKey();
                    return;
            }
        }
    }

    private string DisplayMainMenu()
    {
        Console.Clear();

        AnsiConsole.MarkupLine("Welcome to [cyan]Coding Tracker[/]!");
        AnsiConsole.MarkupLine("-----------------------------------");

        string choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Please select one of the option:")
            .AddChoices("View Coding Sessions", "Add Coding Session", "Delete Coding Session", "Update Coding Session", "Exit the application")
            );

        return choice;
    }

    private void DisplayCodingSessionsTable()
    {
        var codingSessions = _codingSessionController.GetCodingSessions();
        var table = new Table().RoundedBorder().BorderColor(Color.Gold1);

        table.AddColumn("[DarkOrange]ID[/]");
        table.AddColumn("[DarkOrange]Date[/]");
        table.AddColumn("[DarkOrange]Start Time[/]");
        table.AddColumn("[DarkOrange]End Time[/]");
        table.AddColumn("[DarkOrange]Duration[/]");

        foreach(CodingSessionModel codingSession in codingSessions)
        {
            table.AddRow(
                $"[yellow]{codingSession.id.ToString()}[/]", 
                codingSession.startTime.ToString("dd-MM-yyyy"), 
                codingSession.startTime.ToString("HH:mm:ss"), 
                codingSession.endTime.ToString("HH:mm:ss"), 
                codingSession.duration.ToString()
            );
        }

        AnsiConsole.Write(table);
    }
}
