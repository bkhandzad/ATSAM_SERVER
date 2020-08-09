using System;
using System.Data;
using System.Data.SqlClient;
using Macro;

namespace Atsam.Server
{
    public abstract class ABRL
    {
        protected ISA pSA;
        protected ADAL pDAL;
        protected ISCL pSCL;

        public abstract SqlException GetDataTable(ref DataTable dtDataTable, string strTableName, string strFilter = "", string strOrder = "");

        public abstract SqlException SetDataTable(DataTable dtDataTable, string strTableName);

        public abstract Boolean IsDuplicated(string strTableName, string[] strFieldName, string[] strFieldValue, string[] strTextValue, TableStatus tsTableStatus);

        public abstract Boolean[] GetPermission(int intWorkGroupCode, int intTableCode);

        public abstract SqlException Logging(int intUserCode, int intTableCode, Macro.Action aAction, string strIP);

        public abstract ErrorCode CreateUser(string strUserID, string strPassword, ref int intUserCode, ref string strUserName, ref int intWorkGroupCode, ref string strWorkGroup, ref int intWorkStationCode, ref string strWorkStation, ref Boolean IsUserChanged, ref UserType utUserType, string strIP);
    }
}
