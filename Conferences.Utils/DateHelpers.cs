﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public class DateHelpers
    {
        public static IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

        public static DateTime GetDateDifference(DateTime startDate, DateTime endDate, ref int years, ref int months)
        {


            if (endDate < startDate)
            {
                years = 0;
                months = 0;
                return DateTime.Now;
            }
            TimeSpan Span = endDate - startDate;
            DateTime diffDate = DateTime.MinValue + Span;

            //The month can be one out as different months have different days in them. 
            int daysInStartDateMonth = DateTime.DaysInMonth(startDate.Year, startDate.Month);
            int daysInEndDateMonth = DateTime.DaysInMonth(endDate.Year, endDate.Month);
            int diff = daysInEndDateMonth - daysInStartDateMonth;
            diffDate = diffDate.AddDays(diff);//So adjust the diffDate manually

            years = diffDate.Year - 1;
            months = diffDate.Month - 1;
            return diffDate;
        }

        /// <summary>
        /// Attempts to create date from string representation such as +7days or -2months
        /// </summary>
        public static DateTime? GetCalculatedDateTime(object dateRepresentation)
        {
            try
            {
                string val = dateRepresentation.ToString().ToLower();
                if (val == "now")
                    return DateTime.Now;
                int integerVal = 0;
                bool isMinus = val.StartsWith("-");
                bool isPlus = val.StartsWith("+");
                val = val.Replace("-", "").Replace("+", "");
                if ((isMinus) || (isPlus))
                {
                    if (val.Contains("days"))
                    {
                        integerVal = val.Replace("days", "").ConvertToInt32(0);
                        if (integerVal != 0)
                            return (isMinus) ? DateTime.Now.AddDays(-integerVal) : DateTime.Now.AddDays(integerVal);
                    }
                    else if (val.Contains("months"))
                    {
                        integerVal = val.Replace("months", "").ConvertToInt32(0);
                        if (integerVal != 0)
                            return (isMinus) ? DateTime.Now.AddMonths(-integerVal) : DateTime.Now.AddDays(integerVal);
                    }
                    else if (val.Contains("years"))
                    {
                        integerVal = val.Replace("years", "").ConvertToInt32(0);
                        if (integerVal != 0)
                            return (isMinus) ? DateTime.Now.AddYears(-integerVal) : DateTime.Now.AddDays(integerVal);
                    }
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static DateTime GetMondayThisWeek()
        {
            int substract = (1 - (int)DateTime.Now.DayOfWeek);
            DateTime dt = DateTime.Now.AddDays(substract);
            return new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
        }


        public static TimeSpan GetWeeks(int weeks)
        {
            return TimeSpan.FromDays(weeks * 7);
        }

        public static TimeSpan GetDays(int days)
        {
            return TimeSpan.FromDays(days);
        }

        public static TimeSpan GetHours(int hours)
        {
            return TimeSpan.FromHours(hours);
        }

        public static TimeSpan GetMinutes(int minutes)
        {
            return TimeSpan.FromMinutes(minutes);
        }
    }
}
