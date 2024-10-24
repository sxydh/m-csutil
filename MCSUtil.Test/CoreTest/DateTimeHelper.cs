using System;

namespace MCSUtil.Test.CoreTest
{
    public static class DateTimeHelper
    {
        public static long Timestamp()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }
    }
}