using System;

namespace MCSUtil.Core
{
    public static class DateTimeHelper
    {
        public static long Timestamp()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }
    }
}