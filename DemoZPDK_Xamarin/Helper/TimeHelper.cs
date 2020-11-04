using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoZPDK_Xaramin_V2.Helper
{
    public class TimeHelper
    {
        public static long GetTimeStamp(DateTime date)
        {
            return (long)(date.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds;
        }

        public static long GetTimeStamp()
        {
            return GetTimeStamp(DateTime.Now);
        }
    }
}