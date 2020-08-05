using System;
using System.Data;
using System.Data.SqlClient;

namespace Atsam
{
    public interface IBFL : ISCL
    {
        SqlException GetDataTable(ref DataTable dtDataTable, string strTableName);

        SqlException GetDataTable(ref DataTable dtDataTable, string strTableName, string strFilter);

        SqlException GetDataTable(ref DataTable dtDataTable, string strTableName, string strFilter, string strOrder);

        SqlException SetDataTable(DataTable dtDataTable, string strTableName);

        Boolean[] GetPermission(int intWorkGroupCode, int intTableCode);

        Boolean IsDuplicate(string strTableName, string[] strFieldName, string[] strFieldValue, string[] strTextValue, TableStatus tsTableStatus);

        SqlException Logging(int intUserCode, int intTableCode, Action pAction, string strIP);

        ErrorCode CreateUser(string strUserID, string strPassword, ref int intUserCode, ref string strUserName, ref int intWorkGroupCode, ref string strWorkGroup, ref int intWorkStationCode, ref string strWorkStation, ref Boolean IsUserChanged, ref UserType utUserType, string strIP);
    }
}
