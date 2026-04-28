using Spectre.Console;

namespace STUDY.CodingTracker;

internal class UserInterface
{
    public void MainMenu()
    {
        while (true)
        {
            string choice = DisplayMainMenu();

            switch (choice)
            {
                case "View Coding Sessions":
                    AnsiConsole.MarkupLine($"'{choice}' is under construction. Please try again later.");
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
}
