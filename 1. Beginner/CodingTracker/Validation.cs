using System.Globalization;

namespace CodingTracker;

internal class Validation
{
    public DateTime GetValidatedDateTime(string datePrompt, string timePrompt)
    {
        string date = GetValidatedInput(datePrompt, ValidateDate, "Invalid date format. Please try again.");
        string time = GetValidatedInput(timePrompt, ValidateTime, "Invalid time format. Please try again.");
        return DateTime.ParseExact($"{date} {time}", "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
    }

    public void ValidateDateTimeRange(DateTime startDateTime, DateTime endDateTime)
    {
        if (startDateTime > DateTime.Now)
        {
            throw new ArgumentException("Start datetime cannot be in the future.");
        }

        if (endDateTime > DateTime.Now)
        {
            throw new ArgumentException("End datetime cannot be in the future.");
        }

        if (endDateTime < startDateTime)
        {
            throw new ArgumentException("End datetime cannot be earlier than start datetime.");
        }
    }

    private string GetValidatedInput(string prompt, Func<string, bool> validationFunction, string errorMessage)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine()!;
            if (validationFunction(input))
            {
                return input;
            }
            Console.WriteLine(errorMessage);
        }
    }
    public bool ValidateDate(string date)
    {
        return DateTime.TryParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
    }

    public bool ValidateTime(string time)
    {
        return DateTime.TryParseExact(time, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
    }

    public bool ValidateDateTime(string dateTime)
    {
        return DateTime.TryParseExact(dateTime, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
    }
}
