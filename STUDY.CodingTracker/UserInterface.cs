using Microsoft.Extensions.Configuration;
using Spectre.Console;
using STUDY.CodingTracker.Controllers;
using STUDY.CodingTracker.Helper;
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
                    DisplayPressKeyToContinue();
                    break;
                case "Add Coding Session":
                    AddingCodingSessionUI();
                    DisplayPressKeyToContinue();
                    break;
                case "Delete Coding Session":
                    DeleteCodingSessionUI();
                    DisplayPressKeyToContinue();
                    break;
                case "Update Coding Session":
                    UpdateCodingSessionUI();
                    DisplayPressKeyToContinue();
                    break;
                case "Exit the application":
                    AnsiConsole.MarkupLine("Thank you for using the app! See you soon!");
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
        Console.Clear();

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

    private void AddingCodingSessionUI()
    {
        bool success;

        CodingSessionModel newCodingSession = CreateCodingSession();

        success = _codingSessionController.AddCodingSession(newCodingSession);

        if (success)
        {
            AnsiConsole.MarkupLine("[green]Coding session added successfully![/]");
            return;
        }

        AnsiConsole.MarkupLine("[red]Coding session was not added...[/]");
    }

    private void DeleteCodingSessionUI()
    {
        while (true)
        {
            DisplayCodingSessionsTable();

            string idToDelete = UserInput.GetUserIDInput("delete");
            var verificationResult = Verification.VerifyId(idToDelete);

            if (!verificationResult.correct)
            {
                DisplayInputIDErrorMessage();
                continue;
            }
            int numberOfRows = _codingSessionController.DeleteCodingSession(verificationResult.id);

            if (numberOfRows > 0)
            {
                AnsiConsole.MarkupLine("[green]Coding session deleted successfully![/]");
                return;
            }

            AnsiConsole.MarkupLine("[red]The id you entered doesn't exist.[/]");
            DisplayPressKeyToContinue();
        }
    }

    private void UpdateCodingSessionUI()
    {
        while (true)
        {
            DisplayCodingSessionsTable();

            string idToUpdate = UserInput.GetUserIDInput("update");
            var verificationResult = Verification.VerifyId(idToUpdate);

            if (!verificationResult.correct)
            {
                DisplayInputIDErrorMessage();
                continue;
            }

            CodingSessionModel updatedCodingSession = CreateCodingSession();

            int numberOfRows = _codingSessionController.UpdateCodingSession(verificationResult.id, updatedCodingSession);

            if (numberOfRows > 0)
            {
                AnsiConsole.MarkupLine("[green]Coding session updated successfully![/]");
                return;
            }

            AnsiConsole.MarkupLine("[red]The id you entered doesn't exist.[/]");
        }
    }

    private void DisplayPressKeyToContinue()
    {
        AnsiConsole.MarkupLine("(Press Any Key to Continue.)");
        Console.ReadKey();
    }

    private CodingSessionModel CreateCodingSession()
    {
        DateTime startTime = UserInput.GetUserDateInput("start");
        DateTime endTime = UserInput.GetUserDateInput("end");

        CodingSessionModel newCodingSession = new CodingSessionModel(startTime, endTime);

        return newCodingSession;
    }

    private void DisplayInputIDErrorMessage()
    {
        AnsiConsole.WriteLine("You've inputted the ID in a wrong format. Please try again!");
        AnsiConsole.WriteLine("It must be a whole positive number.");
        DisplayPressKeyToContinue();
    }
}
