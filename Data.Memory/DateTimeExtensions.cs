namespace Data.Memory;

public static class DateTimeExtensions
{
    public static bool IsSameDay(this DateTime date1, DateTime date2)
        => date1.Year == date2.Year
        && date1.Month == date2.Month
        && date1.Day == date2.Day;
}