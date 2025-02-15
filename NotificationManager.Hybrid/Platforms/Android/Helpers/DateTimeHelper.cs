namespace NotificationManager.Hybrid.Platforms.Android.Helpers;

public class DateTimeHelper
{
    public static DateTime FromTimestamp(long timestamp)
    {
        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(timestamp);
        return dateTimeOffset.LocalDateTime;
    }
}
