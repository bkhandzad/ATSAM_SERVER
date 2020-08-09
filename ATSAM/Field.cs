using System;
using System.Data;

namespace Atsam
{

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
