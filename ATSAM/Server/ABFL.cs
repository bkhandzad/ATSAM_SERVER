using System;
using System.Data;
using System.Data.SqlClient;
using Macro;

namespace Atsam.Server
{
    public abstract class ABFL : MarshalByRefObject,ISCL
    {
        public ISCL pCL;

        public ABRL pBRL;

        public abstract SqlException GetDataTable(ref DataTable dtDataTable, string strTableName);

        public abstract SqlException GetDataTable(ref DataTable dtDataTable, string strTableName, string strFilter);

        public abstract SqlException GetDataTable(ref DataTable dtDataTable, string strTableName, string strFilter, string strOrder);

        public abstract SqlException SetDataTable(DataTable dtDataTable, string strTableName);

        public abstract Boolean[] GetPermission(int intWorkGroupCode, int intTableCode);

        public abstract Boolean IsDuplicate(string strTableName, string[] strFieldName, string[] strFieldValue, string[] strTextValue, TableStatus tsTableStatus);

        public abstract SqlException Logging(int intUserCode, int intTableCode, Macro.Action pAction, string strIP);

        public abstract ErrorCode CreateUser(string strUserID, string strPassword, ref int intUserCode, ref string strUserName, ref int intWorkGroupCode, ref string strWorkGroup, ref int intWorkStationCode, ref string strWorkStation, ref Boolean IsUserChanged, ref UserType utUserType, string strIP);
        public string GetResourceString(string strString)
        {
            return (pCL.GetResourceString(strString));
        }

        public DateTime GetDateTime(Boolean DateAndTime)
        {
            return (pCL.GetDateTime(DateAndTime));
        }

        public string GetSolarDate(DateTime dtDateTime, char chrDivider = '\0')
        {
            return (pCL.GetSolarDate(dtDateTime, chrDivider));
        }

        public string GetSolarDate(char chrDivider = '\0')
        {
            return (pCL.GetSolarDate(chrDivider));
        }

        public DateTime GetJulianDate(string strSolarDate)
        {
            return (pCL.GetJulianDate(strSolarDate));
        }

        public string GetSolarTime(DateTime dtDateTime, char chrDivider = '\0')
        {
            return (pCL.GetSolarTime(dtDateTime, chrDivider));
        }

        public string GetSolarTime(char chrDivider = '\0')
        {
            return (pCL.GetSolarTime(chrDivider));
        }

        public string GetSolarDayOfWeek(DateTime dtDateTime)
        {
            return (pCL.GetSolarDayOfWeek(dtDateTime));
        }

        public string GetSolarDayOfWeek()
        {
            return (pCL.GetSolarDayOfWeek());
        }

        public string GetSolarDayOfWeekEnglish()
        {
            return pCL.GetSolarDayOfWeekEnglish();
        }

        public string GetErrorMessage(SqlException eSqlException, TableStatus tsTableStatus)
        {
            return (pCL.GetErrorMessage(eSqlException, tsTableStatus));
        }

        public string DLToDF(string DL)
        {
            return (pCL.DLToDF(DL));
        }

        public string NormalizeValue(string strValue)
        {
            return (pCL.NormalizeValue(strValue));
        }

        public string GetCodedSolarDate(Boolean IsConverse, int intCodeNumber)
        {
            return (pCL.GetCodedSolarDate(IsConverse, intCodeNumber));
        }

        public string GetResourceString(string strString, string strResourceFile)
        {
            return (pCL.GetResourceString(strString, strResourceFile));
        }

        public int GetSolarDayOfYear()
        {
            return (pCL.GetSolarDayOfYear());
        }

        public int GetSolarDayOfYear(DateTime dtDateTime)
        {
            return (pCL.GetSolarDayOfYear(dtDateTime));
        }
    }
}
