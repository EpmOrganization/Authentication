using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.Authentication.Common.Date
{
    public class DateTimeUtility
    {
        public static DateTime BaseDateTime { get; } = new DateTime(1970, 1, 1, 0, 0, 0);

        /// <summary>
        /// 获取时间戳 1970.1.1 00:00:00为基准
        /// 单位为秒
        /// </summary>
        /// <returns></returns>
        public static long GetTimestampBase1970(DateTime? datetime = null)
        {
            if (datetime == null)
                datetime = DateTime.UtcNow;
            return (long)((datetime.Value - BaseDateTime).TotalSeconds);
        }

        public static long GetNewTimestamp(int times)
        {
            return GetTimestampBase1970() + (long)Math.Pow(2, times);
        }
        public static void GetWeek(out DateTime monday, out DateTime sunday, DateTime? date = null)
        {
            if (!date.HasValue)
                date = DateTime.Now;
            int today = (int)date.Value.DayOfWeek;
            if (today == 0)
                monday = date.Value.AddDays(-6 - today).Date;
            else
                monday = date.Value.AddDays(1 - today).Date;
            sunday = monday.AddDays(6).Date;
        }

        public static int GetSecondsToDaysEnd(DateTime? date = null)
        {
            if (!date.HasValue)
                date = DateTime.Now;
            var todayEnd = date.Value.Date.AddDays(1);
            return (int)(todayEnd - date.Value).TotalSeconds;
        }
        public static int GetSecondsToWeekEnd()
        {
            DateTime now = DateTime.Now;
            DateTime temp = new DateTime(now.Year, now.Month, now.Day);
            int count = now.DayOfWeek - DayOfWeek.Sunday;
            if (count != 0) count = 7 - count;
            var sundayEnd = temp.AddDays(count + 1).Date;
            return (int)(sundayEnd - now).TotalSeconds;
        }
    }
}
