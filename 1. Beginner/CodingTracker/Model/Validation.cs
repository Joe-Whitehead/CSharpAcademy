using System.Globalization;

namespace CodingTracker.Model;

internal class Validation
{
    public static bool ValidateDate(string date)
    {
        return DateTime.TryParseExact(date, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
    }
}
