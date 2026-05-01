using Spectre.Console;
using STUDY.CodingTracker.Helper;

namespace STUDY.CodingTracker.Helper;

internal static class UserInput
{
    public static DateTime GetUserDateInput(string period)
    {
        while (true)
        {
            Console.Clear();

            string dateInput = AnsiConsole.Ask<string>($"Please enter the {period} date (format: HH:mm:ss):");
            var verificationResult = Verification.VerifyDate(dateInput);

            if (verificationResult.correct)
            {
                DateTime date = verificationResult.date;
                return date;
            }

            AnsiConsole.WriteLine("The date you inputted were in a wrong format. Please try again! (Press Any Key to Continue).");
            Console.ReadKey();
        }
    }

    public static string GetUserIDInput(string operation)
    {
        while (true)
        {
            string idInput = AnsiConsole.Ask<string>($"Please enter the id of the coding session you want to {operation}:");
                
            return idInput;
        }
    }
}
