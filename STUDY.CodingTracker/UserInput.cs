using Spectre.Console;

namespace STUDY.CodingTracker;

internal static class UserInput
{
    public static DateTime GetUserDateInput(string period)
    {
        DateTime date = AnsiConsole.Ask<DateTime>($"Please enter the {period} date (format: hh:mm:ss):");
        return date;
    }
}
