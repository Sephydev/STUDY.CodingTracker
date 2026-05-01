using System.Globalization;

namespace STUDY.CodingTracker.Helper;

internal static class Verification
{
    public static (bool correct, DateTime date) VerifyDate(string dateInput)
    {
        bool correct = false;
        DateTime date = new DateTime();

        if (dateInput != null && DateTime.TryParseExact(dateInput, "HH:mm:ss", new CultureInfo("en-US"), DateTimeStyles.None, out date))
        {
            correct = true;
        }

        return (correct, date);
    }

    public static (bool correct, int id) VerifyId(string idInput)
    {
        bool correct = false;
        int id = 0;

        if (idInput != null && int.TryParse(idInput, out id) && id >= 0)
        {
            correct = true;
        }

        return (correct, id);
    }
}
