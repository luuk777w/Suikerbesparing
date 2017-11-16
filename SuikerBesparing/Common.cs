using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace SuikerBesparing
{
    class Common
    {
        private DateTime SetDate;

        public int WeekOfYear(DateTime date)
        {
            var day = (int)CultureInfo.CurrentCulture.Calendar.GetDayOfWeek(date);
            return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date.AddDays(4 - (day == 0 ? 7 : day)), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
    }
}
