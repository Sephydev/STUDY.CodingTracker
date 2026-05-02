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
            MainMenuChoice choice = GetMainMenuChoice();

            switch (choice)
            {
                case MainMenuChoice.ViewCodingSessions:
                    DisplayCodingSessionsTable();
                    DisplayPressKeyToContinue();
                    break;
                case MainMenuChoice.AddCodingSession:
                    AddingCodingSessionUI();
                    DisplayPressKeyToContinue();
                    break;
                case MainMenuChoice.DeleteCodingSession:
                    DeleteCodingSessionUI();
                    DisplayPressKeyToContinue();
                    break;
                case MainMenuChoice.UpdateCodingSession:
                    UpdateCodingSessionUI();
                    DisplayPressKeyToContinue();
                    break;
                case MainMenuChoice.Exit:
                    AnsiConsole.MarkupLine("Thank you for using the app! See you soon!");
                    return;
            }
        }
    }

    private MainMenuChoice GetMainMenuChoice()
    {
        Console.Clear();

        AnsiConsole.MarkupLine("Welcome to [cyan]Coding Tracker[/]!");
        AnsiConsole.MarkupLine("-----------------------------------");

        MainMenuChoice choice = AnsiConsole.Prompt(
            new SelectionPrompt<MainMenuChoice>()
            .Title("Please select one of the option:")
            .AddChoices(Enum.GetValues<MainMenuChoice>())
            );

        return choice;
    }

    private void DisplayCodingSessionsTable()
    {
        Console.Clear();

        var codingSessions = _codingSessionController.GetCodingSessions();
        var table = new Table().RoundedBorder().BorderColor(Color.Gold1);

        table.AddColumn("[DarkOrange]ID[/]");
        table.AddColumn("[DarkOrange]Start Time[/]");
        table.AddColumn("[DarkOrange]End Time[/]");
        table.AddColumn("[DarkOrange]Duration[/]");

        foreach(CodingSessionModel codingSession in codingSessions)
        {
            table.AddRow(
                $"[yellow]{codingSession.id.ToString()}[/]", 
                codingSession.startTime.ToString("dd/MMM/yyyy HH:mm"), 
                codingSession.endTime.ToString("dd/MMM/yyyy HH:mm"), 
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
        while (true)
        {
            Console.Clear();

            string startTime = UserInput.GetUserDateInput("start");
            var verificationResultStartTime = Verification.VerifyDate(startTime);

            if (!verificationResultStartTime.correct)
            {
                DisplayInputDateErrorMessage();
                continue;
            }

            string endTime = UserInput.GetUserDateInput("end");
            var verificationResultEndTime = Verification.VerifyEndDate(endTime, verificationResultStartTime.date);

            if (!verificationResultEndTime.correct)
            {
                DisplayInputDateErrorMessage();
                continue;
            }

            CodingSessionModel newCodingSession = new CodingSessionModel(verificationResultStartTime.date, verificationResultEndTime.date);

            return newCodingSession;
        }
    }

    private void DisplayInputIDErrorMessage()
    {
        AnsiConsole.MarkupLine("[red]You've inputted the ID in a wrong format. Please try again![/]");
        AnsiConsole.MarkupLine("Good format: [green]It must be a whole positive number.[/]");
        DisplayPressKeyToContinue();
    }

    private void DisplayInputDateErrorMessage()
    {
        AnsiConsole.MarkupLine("[red]You've inputted the date in a wrong format and/or in the wrong order. Please try again![/]");
        AnsiConsole.MarkupLine("Good format: [green]dd/MM/yyyy HH:mm[/] (d = day, M = month, y = year, H = hour, m = minute)");
        AnsiConsole.MarkupLine("The start time must be [green]earlier[/] than end time.");
        DisplayPressKeyToContinue();
    }
}
