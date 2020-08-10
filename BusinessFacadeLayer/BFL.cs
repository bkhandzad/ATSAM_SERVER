using Atsam;
using Macro;
using Atsam.Server;
using System;
using System.Data;
using System.Data.SqlClient;

using BusinessRuleLayer;
using ServerCommonLayer;

namespace BusinessFacadeLayer
{
    public class BFL : ABFL
    {    
        public BFL() {
            this.pBRL = new BRL();
            this.pCL = new SCL();
        }
        
        public override SqlException GetDataTable(ref DataTable dtDataTable, string strTableName)
        {
            return (pBRL.GetDataTable(ref dtDataTable, strTableName));
        }

        public override SqlException GetDataTable(ref DataTable dtDataTable, string strTableName, string strFilter)
        {
            return (pBRL.GetDataTable(ref dtDataTable, strTableName, strFilter));
        }

        public override SqlException GetDataTable(ref DataTable dtDataTable, string strTableName, string strFilter, string strOrder)
        {
            return (pBRL.GetDataTable(ref dtDataTable, strTableName, strFilter, strOrder));
        }

        public override SqlException SetDataTable(DataTable dtDataTable, string strTableName)
        {
            return (pBRL.SetDataTable(dtDataTable, strTableName));
        }

        public override Boolean[] GetPermission(int intWorkGroupCode, int intTableCode)
        {
            return (pBRL.GetPermission(intWorkGroupCode, intTableCode));
        }

        public override Boolean IsDuplicate(string strTableName, string[] strFieldName, string[] strFieldValue, string[] strTextValue, TableStatus tsTableStatus)
        {
            return (pBRL.IsDuplicated(strTableName, strFieldName, strFieldValue, strTextValue, tsTableStatus));
        }

        public override SqlException Logging(int intUserCode, int intTableCode, Macro.Action pAction, string strIP)
        {
            return (pBRL.Logging(intUserCode, intTableCode, pAction, strIP));
        }

        public override ErrorCode CreateUser(string strUserID, string strPassword, ref int intUserCode, ref string strUserName, ref int intWorkGroupCode, ref string strWorkGroup, ref int intWorkStationCode, ref string strWorkStation, ref Boolean IsUserChanged, ref UserType utUserType, string strIP)
        {
            return (pBRL.CreateUser(strUserID, strPassword, ref intUserCode, ref strUserName, ref intWorkGroupCode, ref strWorkGroup, ref intWorkStationCode, ref strWorkStation, ref IsUserChanged, ref utUserType, strIP));
        }
    }
}
