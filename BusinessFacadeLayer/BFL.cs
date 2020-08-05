using Atsam;
using System;
using System.Data;
using System.Data.SqlClient;

using BusinessRuleLayer;
using ServerCommonLayer;

namespace BusinessFacadeLayer
{
    public class BFL : MarshalByRefObject, IBFL
    {    
        private BRL pBRL = new BRL();
        private SCL pCL = new SCL();


        public SqlException GetDataTable(ref DataTable dtDataTable, string strTableName)
        {
            return (pBRL.GetDataTable(ref dtDataTable, strTableName));
        }

        public SqlException GetDataTable(ref DataTable dtDataTable, string strTableName, string strFilter)
        {
            return (pBRL.GetDataTable(ref dtDataTable, strTableName, strFilter));
        }

        public SqlException GetDataTable(ref DataTable dtDataTable, string strTableName, string strFilter, string strOrder)
        {
            return (pBRL.GetDataTable(ref dtDataTable, strTableName, strFilter, strOrder));
        }

        public SqlException SetDataTable(DataTable dtDataTable, string strTableName)
        {
            return (pBRL.SetDataTable(dtDataTable, strTableName));
        }

        public Boolean[] GetPermission(int intWorkGroupCode, int intTableCode)
        {
            return (pBRL.GetPermission(intWorkGroupCode, intTableCode));
        }

        public Boolean IsDuplicate(string strTableName, string[] strFieldName, string[] strFieldValue, string[] strTextValue, TableStatus tsTableStatus)
        {
            return (pBRL.IsDuplicated(strTableName, strFieldName, strFieldValue, strTextValue, tsTableStatus));
        }

        public SqlException Logging(int intUserCode, int intTableCode, Atsam.Action pAction, string strIP)
        {
            return (pBRL.Logging(intUserCode, intTableCode, pAction, strIP));
        }

        public ErrorCode CreateUser(string strUserID, string strPassword, ref int intUserCode, ref string strUserName, ref int intWorkGroupCode, ref string strWorkGroup, ref int intWorkStationCode, ref string strWorkStation, ref Boolean IsUserChanged, ref UserType utUserType, string strIP)
        {
            return (pBRL.CreateUser(strUserID, strPassword, ref intUserCode, ref strUserName, ref intWorkGroupCode, ref strWorkGroup, ref intWorkStationCode, ref strWorkStation, ref IsUserChanged, ref utUserType, strIP));
        }

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
