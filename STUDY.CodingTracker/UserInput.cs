using Spectre.Console;

namespace STUDY.CodingTracker;

internal static class UserInput
{
    public static DateTime GetUserDateInput(string period)
    {
        DateTime date = AnsiConsole.Ask<DateTime>($"Please enter the {period} date (format: HH:mm:ss):");
        return date;
    }

    public static int GetUserIDInput(string operation)
    {
        int id = AnsiConsole.Ask<int>($"Please enter the id of the coding session you want to {operation}:");
        return id;
    }
}
