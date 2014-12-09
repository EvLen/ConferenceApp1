using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    
    public static class DateTimes
    {
 
        public static DateTime StartOfCurrentMonth(this DateTime value)
        {
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, 01);
        }
 
 
        public static DateTime StartOfMonth(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, 01, 0, 0, 0);
        }
 
        public static DateTime EndOfCurrentmonth(this DateTime value)
        {
            var retVal = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 01);
 
            retVal = retVal.AddMonths(1).AddDays(-1);
            return new DateTime(retVal.Year, retVal.Month, retVal.Day);
        }
 
        public static DateTime EndOfMonth(this DateTime value)
        {
            var retVal = value.StartOfMonth();
 
            retVal = retVal.AddMonths(1).AddDays(-1);
            return new DateTime(retVal.Year, retVal.Month, retVal.Day, 23, 59, 59);
        }
 
 
        public static DateTime ToStartOfDay(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
        }
 
        public static DateTime ToEndOfDay(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);
        }
 
        public static DateTime ToEndOfWorkingDay(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, 17, 30, 00);
        }
 
       
 
 
        public static DateTime StartOfWeek(this DateTime dt)
        {
            int substract = (1 - (int)dt.DayOfWeek);
            DateTime newdt = dt.AddDays(substract);
            return new DateTime(newdt.Year, newdt.Month, newdt.Day, 0, 0, 0);
        }
 
        public static DateTime EndOfWeek(this DateTime dt)
        {
            double offset = 0;
 
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    offset = 6;
                    break;
                case DayOfWeek.Tuesday:
                    offset = 5;
                    break;
                case DayOfWeek.Wednesday:
                    offset = 4;
                    break;
                case DayOfWeek.Thursday:
                    offset = 3;
                    break;
                case DayOfWeek.Friday:
                    offset = 2;
                    break;
                case DayOfWeek.Saturday:
                    offset = 1;
                    break;
                case DayOfWeek.Sunday:
                    offset = 0;
                    break;
            }
 
            return dt.AddDays(offset);
        }
 
 
        public static bool IsWeekDay(this DateTime dt)
        {
            int day = (int)dt.DayOfWeek;
            return ((day > 0) && (day < 6));
        }
 
        public static bool IsTheSameDate(this DateTime dt, DateTime obj)
        {
            if ((dt != null) && (obj != null))
                return (bool)((dt.Year == obj.Year) && (dt.Month == obj.Month) && (dt.Day == obj.Day));
            return false;
        }
 
    }
}


