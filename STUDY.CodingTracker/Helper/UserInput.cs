using Spectre.Console;

namespace STUDY.CodingTracker.Helper;

internal static class UserInput
{
    public static string GetUserDateInput(string period)
    {
        string dateInput = AnsiConsole.Ask<string>($"Please enter the {period} date (format: HH:mm:ss):");
        return dateInput;
    }

    public static string GetUserIDInput(string operation)
    {
        string idInput = AnsiConsole.Ask<string>($"Please enter the id of the coding session you want to {operation}:");

        return idInput;
    }
}
