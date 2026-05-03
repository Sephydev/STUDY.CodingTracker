using System.Globalization;

namespace STUDY.CodingTracker.Helper;

internal static class Verification
{
    public static (bool correct, DateTime date) VerifyDate(string dateInput)
    {
        bool correct = false;
        DateTime date = new DateTime();

        if (dateInput != null && DateTime.TryParseExact(dateInput, "dd/MM/yyyy HH:mm", new CultureInfo("en-US"), DateTimeStyles.None, out date))
        {
            correct = true;
        }

        return (correct, date);
    }

    public static (bool correct, DateTime date) VerifyEndDate(string endDateInput, DateTime startDate)
    {
        bool correct = false;
        var formatVerificationResult = VerifyDate(endDateInput);

        if (formatVerificationResult.correct && DateTime.Compare(formatVerificationResult.date, startDate) > 0)
        {
            correct = true;
        }

        return (correct, formatVerificationResult.date);
    }

    public static (bool correct, int id) VerifyId(string idInput)
    {
        bool correct = false;
        int id = 0;

        if (idInput != null && int.TryParse(idInput, out id) && id > 0)
        {
            correct = true;
        }

        return (correct, id);
    }

    public static (bool correct, int periodNum) VerifyWeek(string weekInput)
    {
        bool correct = false;
        int weekNum = 0;

        if (weekInput != null && int.TryParse(weekInput, out weekNum) && weekNum > 0 && weekNum < 54)
            correct = true;

        return (correct, weekNum);
    }

    public static (bool correct, int periodNum) VerifyDay(string dayInput)
    {
        bool correct = false;
        int dayNum = 0;

        if (dayInput != null && int.TryParse(dayInput, out dayNum) && dayNum > 0 && dayNum < 32)
            correct = true;

        return (correct, dayNum);
    }

    public static (bool correct, int periodNum) VerifyYear(string yearInput)
    {
        bool correct = false;
        int yearNum = 0;

        if (yearInput != null && int.TryParse(yearInput, out yearNum) && yearNum >= 1900 && yearNum <= DateTime.Now.Year)
            correct = true;

        return (correct, yearNum);
    }
}
