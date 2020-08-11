using Atsam;
using Macro;
using Atsam.Data;
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
                var obj = pDAL.ExecuteScalar("SELECT MAX(PK_PartnerCode) + 1 PartnerCode FROM m_Partner WHERE FK_PartnerTypeCode = " + (int)ptPartnerType);
                if (obj.ToString().Equals(String.Empty))
                {
                    switch (ptPartnerType)
                    {
                        case PartnerType.ptEmployee: return 11;
                        case PartnerType.ptSupplier: return 1001;
                        case PartnerType.ptCustomer: return 10001;
                    }
                }
                return Convert.ToInt32(obj);
            }
            catch
            {
                return -1;
            }
        }

        public override SqlException InsertInvoice(ref Invoice iInvoice)
        {
            try
            {
                Int32 InvoiceID = Convert.ToInt32(pDAL.ExecuteScalar("SELECT ISNULL(MAX(PK_InvoiceID),0) + 1 InvoiceID FROM m_Invoice"));
                iInvoice.PK_InvoiceID = InvoiceID;
                foreach (InvoiceLine Line in iInvoice.Lines)
                {
                    Line.FK_InvoiceID = InvoiceID;
                }
                iInvoice.InvoiceCode = GenerateInvoiceCode(iInvoice.FK_InvoiceStateCode, iInvoice.FK_InvoiceTypeCode);
                iInvoice.SolarDate = pSCL.GetSolarDate('/');
                iInvoice.SolarTime = pSCL.GetSolarTime(':');
                DataTable dtInvoice = new DataTable();
                pDAL.GetDataTable(ref dtInvoice, "SELECT * FROM m_Invoice WHERE 1 = 0");
                DataRow drInvoice = dtInvoice.NewRow();
                drInvoice["PK_InvoiceID"] = iInvoice.PK_InvoiceID;
                drInvoice["FK_InvoiceStateCode"] = (int)iInvoice.FK_InvoiceStateCode;
                drInvoice["FK_InvoiceTypeCode"] = (int)iInvoice.FK_InvoiceTypeCode;
                drInvoice["FK_PartnerCode"] = iInvoice.FK_PartnerCode;
                drInvoice["InvoiceCode"] = iInvoice.InvoiceCode;
                drInvoice["DeliverDate"] = iInvoice.DeliverDate;
                drInvoice["InvoiceNumber"] = iInvoice.InvoiceNumber;
                drInvoice["VATValue"] = iInvoice.VATValue;
                drInvoice["Discount"] = iInvoice.Discount;
                drInvoice["SolarDate"] = iInvoice.SolarDate;
                drInvoice["SolarTime"] = iInvoice.SolarTime;
                drInvoice["FK_UserID"] = iInvoice.FK_UserID;
                dtInvoice.Rows.Add(drInvoice);
                SqlException eSqlException = pDAL.SetDataTable(dtInvoice, "SELECT * FROM m_Invoice");
                if (eSqlException == null)
                {
                    dtInvoice.AcceptChanges();
                    DataTable dtInvoiceLine = new DataTable();
                    pDAL.GetDataTable(ref dtInvoiceLine, "SELECT * FROM s_InvoiceLine WHERE 1 = 0");
                    for (int i = 0; i < iInvoice.Lines.Count; i++)
                    {
                        DataRow drInvoiceLine = dtInvoiceLine.NewRow();
                        drInvoiceLine["FK_InvoiceID"] = iInvoice.Lines[i].FK_InvoiceID;
                        drInvoiceLine["FK_ProductCode"] = iInvoice.Lines[i].FK_ProductCode;
                        drInvoiceLine["FK_DyeCode"] = iInvoice.Lines[i].FK_DyeCode;
                        drInvoiceLine["Price"] = iInvoice.Lines[i].Price;
                        dtInvoiceLine.Rows.Add(drInvoiceLine);
                    }
                    eSqlException = pDAL.SetDataTable(dtInvoice, "SELECT * FROM s_InvoiceLine");
                    return (eSqlException);
                }
                else
                    return eSqlException;
            
            }
            catch
            {
                return (null);
            }
        }

        public String GenerateInvoiceCode(InvoiceState isInvoiceState, InvoiceType itInvoiceType)
        {
            try
            {
                string strNNNN = GetInvoiceCode(isInvoiceState, itInvoiceType);
                if (strNNNN == string.Empty)
                    return (string.Empty);
                string strRRR = ((int)itInvoiceType).ToString() + ((int)isInvoiceState).ToString().PadLeft(2, '0');
                string strYY = pSCL.GetSolarDate().Trim().Substring(2, 2).Trim();
                string strDDD = pSCL.GetSolarDayOfYear().ToString().PadLeft(3, '0');
                String strInvoiceCode = strYY + strDDD + strRRR + strNNNN;
                return strInvoiceCode;
            }
            catch
            {
                return (String.Empty);
            }
        }

        private string GetInvoiceCode(InvoiceState isInvoiceState, InvoiceType itInvoiceType)
        {
            try
            {
                return (pDAL.ExecuteScalar("SELECT ISNULL(CAST(MAX(SUBSTRING(InvoiceCode, 9, 4)) AS Int),0) + 1 InvoiceID FROM m_Invoice WHERE SolarDate = '" + pSCL.GetSolarDate('/') + "' AND FK_InvoiceTypeCode = " + (int)itInvoiceType + "AND FK_InvoiceStateCode = " + (int)isInvoiceState).ToString().PadLeft(4, '0'));
            }
            catch
            {
                return (String.Empty);
            }
        }
    }
}
