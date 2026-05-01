using Spectre.Console;

namespace STUDY.CodingTracker.Helper;

internal static class UserInput
{
    public static DateTime GetUserDateInput(string period)
    {
        while (true)
        {
            Console.Clear();
            string dateInput = AnsiConsole.Ask<string>($"Please enter the {period} date (format: HH:mm:ss):");

            if (Verification.VerifyDate(dateInput).correct)
            {
                DateTime date = Verification.VerifyDate(dateInput).date;
                return date;
            }

            AnsiConsole.WriteLine("The date you inputted were in a wrong format. Please try again! (Press Any Key to Continue).");
            Console.ReadKey();
        }
    }

    public static int GetUserIDInput(string operation)
    {
        int id = AnsiConsole.Ask<int>($"Please enter the id of the coding session you want to {operation}:");
        return id;
    }
}
