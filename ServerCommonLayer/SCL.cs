using Atsam;
using Macro;
using Atsam.Server;
using System;
using System.Resources;
using System.Reflection;
using System.Globalization;
using System.Data.SqlClient;

namespace ServerCommonLayer
{
    // Server Common Layer
    public class SCL : ISCL
    {
        public string DLToDF(string DL)
        {
            const string strL = "0123456789*.";
            const string strF = "۰۱۲۳۴۵۶۷۸۹×/";
            string DF = string.Empty;
            for (int i = 0; (i < DL.Length); i++)
            {
                int k = strL.IndexOf(DL[i]);
                DF += (k >= 0) ? strF[k] : DL[i];
            }
            return (DF);
        }

        public string GetCodedSolarDate(bool IsConverse, int intCodeNumber)
        {
            Int64 intCodedSolarDate = Convert.ToInt64(GetSolarDate().ToString()) + Convert.ToInt64(intCodeNumber.ToString().PadLeft(8, Convert.ToChar(intCodeNumber.ToString())));
            string strCodedSolarDate = intCodedSolarDate.ToString().Substring(intCodedSolarDate.ToString().Length - 8, 8);
            if (IsConverse == true)
            {
                for (int intIndex = 0; intIndex < 8; intIndex++)
                    strCodedSolarDate += strCodedSolarDate.Substring(8 - intIndex - 1, 1);
                strCodedSolarDate = strCodedSolarDate.Substring(8);
            }
            return (strCodedSolarDate);
        }

        public DateTime GetDateTime(bool DateAndTime)
        {
            return ((DateAndTime == true) ? DateTime.Now : DateTime.Today);
        }

        public string GetErrorMessage(SqlException eSqlException, TableStatus tsTableStatus)
        {
            string strErrorMessage = string.Empty;
            switch (eSqlException.Number)
            {
                case 547: // Foreign Key voilation
                    {
                        strErrorMessage = (tsTableStatus == TableStatus.tsEdit) ? "strEditError" : "strDeleteError";
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            return ((strErrorMessage == string.Empty) ? eSqlException.Message : GetResourceString(strErrorMessage));
        }

        public DateTime GetJulianDate(string strSolarDate)
        {
            PersianCalendar pcPersianCalendar = new PersianCalendar();
            int intYear = Convert.ToInt16(strSolarDate.Substring(0, 4));
            int intMonth = Convert.ToInt16(strSolarDate.Substring(5, 2));
            int intDay = Convert.ToInt16(strSolarDate.Substring(8, 2));
            return (pcPersianCalendar.ToDateTime(intYear, intMonth, intDay, 0, 0, 0, 0));
        }

        public string GetResourceString(string strString, string strResourceFile = "ServerCommonLayer.Server")
        {
            ResourceManager rmResourceManager = new ResourceManager(strResourceFile, Assembly.GetExecutingAssembly(), null);
            return (rmResourceManager.GetString(strString));
        }

        public string GetSolarDate(char chrDivider = '\0')
        {
            DateTime dtDateTime = DateTime.Today;
            return (GetSolarDate(DateTime.Today, chrDivider));
        }

        public string GetSolarDate(DateTime dtDateTime, char chrDivider = '\0')
        {
            PersianCalendar pcPersianCalendar = new PersianCalendar();
            string strYear = pcPersianCalendar.GetYear(dtDateTime).ToString().Substring(0, 4);
            string strMonth = pcPersianCalendar.GetMonth(dtDateTime).ToString().PadLeft(2, '0');
            string strDay = pcPersianCalendar.GetDayOfMonth(dtDateTime).ToString().PadLeft(2, '0');
            string strDivider = ((chrDivider == '\0') ? string.Empty : chrDivider.ToString());
            return (strYear + strDivider + strMonth + strDivider + strDay);
        }

        public string GetSolarDayOfWeek()
        {
            return GetSolarDayOfWeek(DateTime.Today);
        }

        public string GetSolarDayOfWeek(DateTime dtDateTime)
        {
            PersianCalendar pcPersianCalendar = new PersianCalendar();
            switch (pcPersianCalendar.GetDayOfWeek(dtDateTime))
            {
                case DayOfWeek.Saturday: return (GetResourceString("wdSaturday"));
                case DayOfWeek.Sunday: return (GetResourceString("wdSunday"));
                case DayOfWeek.Monday: return (GetResourceString("wdMonday"));
                case DayOfWeek.Tuesday: return (GetResourceString("wdTuesday"));
                case DayOfWeek.Wednesday: return (GetResourceString("wdWednesday"));
                case DayOfWeek.Thursday: return (GetResourceString("wdThursday"));
                case DayOfWeek.Friday: return (GetResourceString("wdFriday"));
                default: return (String.Empty);
            }
        }

        public string GetSolarDayOfWeekEnglish()
        {
            DateTime dtDateTime = DateTime.Today;
            PersianCalendar pcPersianCalendar = new PersianCalendar();
            switch (pcPersianCalendar.GetDayOfWeek(dtDateTime))
            {
                case DayOfWeek.Saturday: return "Saturday";
                case DayOfWeek.Sunday: return "Sunday";
                case DayOfWeek.Monday: return "Monday";
                case DayOfWeek.Tuesday: return "Tuesday";
                case DayOfWeek.Wednesday: return "Wednesday";
                case DayOfWeek.Thursday: return "Thursday";
                case DayOfWeek.Friday: return "Friday";
                default: return (String.Empty);
            }
        }

        public int GetSolarDayOfYear()
        {
            return (GetSolarDayOfYear(DateTime.Today));
        }

        public int GetSolarDayOfYear(DateTime dtDateTime)
        {
            PersianCalendar pcPersianCalendar = new PersianCalendar();
            return (pcPersianCalendar.GetDayOfYear(dtDateTime));
        }

        public string GetSolarTime(char chrDivider = '\0')
        {
            return GetSolarTime(DateTime.Today, chrDivider);
        }

        public string GetSolarTime(DateTime dtDateTime, char chrDivider = '\0')
        {
            PersianCalendar pcPersianCalendar = new PersianCalendar();
            string strHour = pcPersianCalendar.GetHour(dtDateTime).ToString().PadLeft(2, '0');
            string strMinute = pcPersianCalendar.GetMinute(dtDateTime).ToString().PadLeft(2, '0');
            string strSecond = pcPersianCalendar.GetSecond(dtDateTime).ToString().PadLeft(2, '0');
            string strDivider = ((chrDivider == '\0') ? string.Empty : chrDivider.ToString());
            return (strHour + strDivider + strMinute + strDivider + strSecond);
        }

        public string NormalizeValue(string strValue)
        {
            string[] strData = strValue.Split('.');
            string strI = string.Empty;
            string strF = string.Empty;
            for (int intIndex = 0; intIndex < strData[0].Length; intIndex++)
                if (strData[0].Substring(intIndex, 1) != "0")
                {
                    strI = strData[0].Substring(intIndex);
                    break;
                }
            for (int intIndex = strData[1].Length - 1; intIndex >= 0; intIndex--)
                if (strData[1].Substring(intIndex, 1) != "0")
                {
                    strF = strData[1].Substring(0, intIndex + 1);
                    break;
                }
            strValue = ((strI == string.Empty) ? "0" : strI) + ((strF == string.Empty) ? strF : "." + strF);
            return (strValue);
        }
    }
}
