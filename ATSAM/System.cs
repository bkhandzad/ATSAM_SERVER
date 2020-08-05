using System;
using System.Data;

namespace Atsam
{
    public abstract class Table
    {
        public int PK_TableCode { get; set; }

        public string TableName { get; set; }

        public string Alias { get; set; }

        public string Caption { get; set; }

        public int FK_TableTypeCode { get; set; }

        public string Description { get; set; }

        public bool Visible { get; set; }

        public int FK_ParentTableCode { get; set; }

        public int FK_FieldsTableCode { get; set; }

        public int Rank { get; set; }

        public string LinkPage { get; set; }

        public string Filter { get; set; }

        public string Order { get; set; }



        public FormStatus FormStatus { get; set; }
        public TableStatus TableStatus { get; set; }
        public Boolean IsCancelled { get; set; }
        public string ReturnValue { get; set; }
        public Boolean[] Permission { get; set; }
        public Field _Field { get; set; }
        public DataRow drParent { get; set; }

        public Table(DataRow drDataRow, FormStatus fsFormStatus = FormStatus.fsMain)
        {
            PK_TableCode = Convert.ToInt32(drDataRow["PK_TableCode"].ToString());
            TableName = drDataRow["TableName"].ToString();
            Alias = drDataRow["Alias"].ToString();
            Caption = drDataRow["Caption"].ToString();
            FK_TableTypeCode = Convert.ToInt32(drDataRow["FK_TableTypeCode"].ToString());
            Description = drDataRow["Description"].ToString();
            Visible = Convert.ToBoolean(drDataRow["Visible"].ToString());
            FK_ParentTableCode = Convert.ToInt32(drDataRow["FK_ParentTableCode"].ToString());
            FK_FieldsTableCode = Convert.ToInt32(drDataRow["FK_FieldsTableCode"].ToString());
            Rank = Convert.ToInt32(drDataRow["Rank"].ToString());
            LinkPage = drDataRow["LinkPage"].ToString();
            Filter = drDataRow["Filter"].ToString();
            Permission = new Boolean[System.Enum.GetValues(typeof(Atsam.Action)).Length];

            FormStatus = fsFormStatus;
            TableStatus = TableStatus.tsNone;
            IsCancelled = false;
            ReturnValue = string.Empty;
            Order = string.Empty;

            Permission = AUser._Permission.GetPermission(AUser.WorkGroupCode, PK_TableCode);
        }
        public abstract void SetField();

        public Table Copy()
        {
            return ((Table)this.MemberwiseClone());
        }

        public Boolean getPermission(Atsam.Action aAction)
        {
            return Permission[(int)aAction];
        }
    }

    public class Field
    {
        public int pk_FKTableCode { get; set; }

        public int pk_FieldCode { get; set; }

        public string FieldName { get; set; }

        public string Caption { get; set; }

        public int FK_KeyTypeCode { get; set; }

        public int FK_FieldTypeCode { get; set; }

        public int FK_FieldOperationCode { get; set; }

        public string FieldLength { get; set; }

        public bool Required { get; set; }

        public string DefaultValue { get; set; }

        public bool Visible { get; set; }

        public bool Enabled { get; set; }

        public string Description { get; set; }

        public string LookupSQL { get; set; }

        public string MasterReferenceField { get; set; }

        public string Value { get; set; }
        public Boolean Active { get; set; }
        public DataTable FieldTable { get; }

        public Field(DataTable dtFields)
        {
            this.FieldTable = dtFields;
            if (FieldTable.Rows.Count > 0)
            {
                SetDataField(1);
                Active = true;
            }
        }

        public void SetDataField(int intFieldCode)
        {
            DataRow[] drDataRow = FieldTable.Select("pk_FieldCode = " + intFieldCode.ToString());
            if (drDataRow.Length == 1)
            {
                pk_FKTableCode = Convert.ToInt32(drDataRow[0]["pk_FkTableCode"].ToString());
                pk_FieldCode = Convert.ToInt32(drDataRow[0]["pk_FieldCode"].ToString());
                FieldName = drDataRow[0]["FieldName"].ToString();
                Caption = drDataRow[0]["Caption"].ToString();
                FK_KeyTypeCode = Convert.ToInt32(drDataRow[0]["FK_KeyTypeCode"].ToString());
                FK_FieldTypeCode = Convert.ToInt32(drDataRow[0]["FK_FieldTypeCode"].ToString());
                FK_FieldOperationCode = Convert.ToInt32(drDataRow[0]["FK_FieldOperationCode"].ToString());
                FieldLength = drDataRow[0]["FieldLength"].ToString();
                Required = (drDataRow[0]["Required"].ToString() == string.Empty) ? false : Convert.ToBoolean(drDataRow[0]["Required"].ToString());
                DefaultValue = drDataRow[0]["DefaultValue"].ToString();
                Visible = (drDataRow[0]["Visible"].ToString() == string.Empty) ? false : Convert.ToBoolean(drDataRow[0]["Visible"].ToString());
                Enabled = (drDataRow[0]["Enabled"].ToString() == string.Empty) ? false : Convert.ToBoolean(drDataRow[0]["Enabled"].ToString());
                Description = drDataRow[0]["Description"].ToString();
                LookupSQL = drDataRow[0]["LookupSQL"].ToString();
                MasterReferenceField = drDataRow[0]["MasterReferenceField"].ToString();
            }
        }

        public void SetDataField(string strFiledName)
        {
            DataRow[] drDataRow = FieldTable.Select("FieldName = '" + strFiledName.Trim() + "'");
            if (drDataRow.Length == 1)
            {
                pk_FKTableCode = Convert.ToInt32(drDataRow[0]["pk_FkTableCode"].ToString());
                pk_FieldCode = Convert.ToInt32(drDataRow[0]["pk_FieldCode"].ToString());
                FieldName = drDataRow[0]["FieldName"].ToString();
                Caption = drDataRow[0]["Caption"].ToString();
                FK_KeyTypeCode = Convert.ToInt32(drDataRow[0]["FK_KeyTypeCode"].ToString());
                FK_FieldTypeCode = Convert.ToInt32(drDataRow[0]["FK_FieldTypeCode"].ToString());
                FK_FieldOperationCode = Convert.ToInt32(drDataRow[0]["FK_FieldOperationCode"].ToString());
                FieldLength = drDataRow[0]["FieldLength"].ToString();
                Required = Convert.ToBoolean(drDataRow[0]["Required"].ToString());
                DefaultValue = drDataRow[0]["DefaultValue"].ToString();
                Visible = Convert.ToBoolean(drDataRow[0]["Visible"].ToString());
                Enabled = Convert.ToBoolean(drDataRow[0]["Enabled"].ToString());
                Description = drDataRow[0]["Description"].ToString();
                LookupSQL = drDataRow[0]["LookupSQL"].ToString();
                MasterReferenceField = drDataRow[0]["MasterReferenceField"].ToString();
            }
        }

    }
}
