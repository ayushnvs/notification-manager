using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationManager.Hybrid.Platforms.Android.Helpers;

public class DateTimeHelper
{
    public static DateTime FromTimestamp(long timestamp)
    {
        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(timestamp);
        return dateTimeOffset.DateTime;
    }
}
