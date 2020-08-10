using Atsam;
using Macro;
using Atsam.Server;
using System;
using System.Data;
using System.Data.SqlClient;

using DataAccessLayer;
using ServerCommonLayer;

namespace BusinessRuleLayer
{
    public class BRL : ABRL
    {
        public BRL()
        {
            this.pSA = new SA();
            this.pDAL = new DAL();
            this.pSCL = new SCL();
        }

        public override ErrorCode CreateUser(string strUserID, string strPassword, ref int intUserCode, ref string strUserName, ref int intWorkGroupCode, ref string strWorkGroup, ref int intWorkStationCode, ref string strWorkStation, ref bool IsUserChanged, ref UserType utUserType, string strIP)
        {
            int intOldUserCode = intUserCode;
            ErrorCode erErrorCode = ErrorCode.ecNone;
            if ((strUserID == pSA.getUserID()) && (strPassword == pSA.getPassword()))
            {
                intUserCode = 0;
                strUserName = pSCL.GetResourceString("strServerAdministrator");
                intWorkGroupCode = 0;
                strWorkGroup = string.Empty;
                intWorkStationCode = 1;
                strWorkStation = string.Empty;
                utUserType = UserType.utServerAdministrator;
            }
            else
            {
                string strFilter = "(UserID = '" + strUserID + "') AND (UserPassword = '" + strPassword + "')";
                DataTable dtDataTable = new DataTable();
                GetDataTable(ref dtDataTable, "v_User", strFilter);
                if (dtDataTable.Rows.Count > 0)
                {
                    intUserCode = Convert.ToInt32(dtDataTable.Rows[0]["PK_UserID"].ToString());
                    strUserName = dtDataTable.Rows[0]["FirstName"].ToString().Trim() + " " + dtDataTable.Rows[0]["LastName"].ToString().Trim();
                    intWorkGroupCode = Convert.ToInt32(dtDataTable.Rows[0]["PK_WorkGroupCode"].ToString());
                    strWorkGroup = dtDataTable.Rows[0]["WorkGroup"].ToString().Trim();
                    intWorkStationCode = Convert.ToInt32(dtDataTable.Rows[0]["PK_WorkStationCode"].ToString());
                    strWorkStation = dtDataTable.Rows[0]["WorkStation"].ToString().Trim() + " - " + dtDataTable.Rows[0]["Description"].ToString().Trim();
                    utUserType = UserType.utLocal;
                }
                else
                    erErrorCode = ErrorCode.ecUserNotDefined;
            }
            IsUserChanged = !(intOldUserCode == intUserCode);
            if (erErrorCode == ErrorCode.ecNone)
            {
                if ((IsUserChanged == true) && (intOldUserCode > 0))
                    Logging(intOldUserCode, intWorkStationCode, Macro.Action.aLogOut, strIP);
                Logging(intUserCode, intWorkStationCode, Macro.Action.aLogIn, strIP);
            }
            return (erErrorCode);
        }

        public override SqlException SetDataTable(DataTable dtDataTable, string strTableName)
        {
            string strSQL = "SELECT * FROM " + strTableName;
            return (pDAL.SetDataTable(dtDataTable, strSQL));
        }

        public override SqlException GetDataTable(ref DataTable dtDataTable, string strTableName, string strFilter = "", string strOrder = "")
        {
            string strSQL = string.Empty;
            switch (strTableName)
            {
                default:
                    {
                        strSQL = "SELECT * FROM " + strTableName;
                        break;
                    }
            }
            strSQL = strSQL + ((strFilter == String.Empty) ? strFilter : " WHERE " + strFilter) +
                              ((strOrder == String.Empty) ? strOrder : " ORDER BY " + strOrder);
            return (pDAL.GetDataTable(ref dtDataTable, strSQL));
        }

        public override bool[] GetPermission(int intWorkGroupCode, int intTableCode)
        {
            Boolean[] Permission = new Boolean[System.Enum.GetValues(typeof(Macro.Action)).Length];
            DataTable dtDataTable = new DataTable();
            switch (intWorkGroupCode)
            {
                case -1: break;
                case 0:
                    {
                        for (int Index = 0; (Index < Permission.Length); Index++)
                            Permission[Index] = true;
                    }
                    break;
                default:
                    {
                        string strSQL = "SELECT * FROM _Permission WHERE pk_FKWorkGroupCode = " + intWorkGroupCode.ToString() + " AND pk_FKTableCode = " + intTableCode.ToString();
                        try
                        {
                            SqlException eSqlException = pDAL.GetDataTable(ref dtDataTable, strSQL);
                            if (eSqlException != null)
                            {
                                return (null);
                            }
                        }
                        catch
                        {
                            return (null);
                        }
                        for (int Index = 0; (Index < dtDataTable.Rows.Count); Index++)
                            Permission[Convert.ToInt16(dtDataTable.Rows[Index]["pk_FKActionCode"].ToString())] = true;
                    }
                    break;
            }
            return (Permission);
        }

        public override bool IsDuplicated(string strTableName, string[] strFieldName, string[] strFieldValue, string[] strTextValue, TableStatus tsTableStatus)
        {
            DataTable dtDataTable = new DataTable();
            string strSQL = "SELECT * FROM " + strTableName + " WHERE " + strFieldName[0] + " = " + strTextValue[0];
            for (int intIndex = 1; (intIndex < strFieldName.Length); intIndex++)
                strSQL = strSQL + " AND " + strFieldName[intIndex] + " = " + strTextValue[intIndex];
            try
            {
                SqlException eSqlException = pDAL.GetDataTable(ref dtDataTable, strSQL);
                if (eSqlException != null)
                {
                    return (true);
                }
            }
            catch
            {
                return (true);
            }
            if (dtDataTable.Rows.Count > 0)
            {
                Boolean strEditCriteria = false;
                if (tsTableStatus == TableStatus.tsEdit)
                    for (int intIndex = 0; (intIndex < strFieldName.Length); intIndex++)
                        strEditCriteria = strEditCriteria || (strFieldValue[intIndex] != strTextValue[intIndex]);
                if ((tsTableStatus == TableStatus.tsInsert) || ((tsTableStatus == TableStatus.tsEdit) && strEditCriteria))
                    return (true);
            }
            return (false);
        }

        public override SqlException Logging(int intUserCode, int intTableCode, Macro.Action aAction, string strIP)
        {
            if (intUserCode > 0)
            {
                DataTable dtDataTable = new DataTable();
                DataRow drDataRow;
                string strSQL = "SELECT * FROM _Log WHERE 1 = 0";
                try
                {
                    SqlException eSqlException = pDAL.GetDataTable(ref dtDataTable, strSQL);
                    if (eSqlException != null)
                        return (eSqlException);
                    drDataRow = dtDataTable.NewRow();
                    drDataRow["SolarDate"] = pSCL.GetSolarDate('/');
                    drDataRow["SolarTime"] = pSCL.GetSolarTime(':');
                    drDataRow["FK_TableCode"] = intTableCode;
                    drDataRow["FK_ActionCode"] = aAction;
                    drDataRow["FK_UserID"] = intUserCode;
                    drDataRow["IP"] = strIP;
                    dtDataTable.Rows.Add(drDataRow);
                    eSqlException = pDAL.SetDataTable(dtDataTable, strSQL);
                    if (eSqlException != null)
                        return (eSqlException);
                }
                catch
                {
                    return (null);
                }
            }
            return (null);
        }
        
        public override Int32 GetPartnerCode(PartnerType ptPartnerType)
        {
            try
            {
                var obj = pDAL.ExecuteScalar("SELECT MAX(PK_PartnerCode) PartnerCode FROM m_Partner WHERE FK_PartnerTypeCode = " + (int)ptPartnerType);
                if (obj.ToString().Equals(String.Empty))
                {
                    switch (ptPartnerType)
                    {
                        case PartnerType.ptEmployee: return 10;
                        case PartnerType.ptSupplier: return 1000;
                        case PartnerType.ptCustomer: return 10000;
                    }
                }
                return Convert.ToInt32(obj);
            }
            catch
            {
                return -1;
            }
        }
    }
}
