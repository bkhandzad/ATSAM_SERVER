using System;
using System.Data;
using Macro;

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
            Permission = new Boolean[System.Enum.GetValues(typeof(Macro.Action)).Length];

            FormStatus = fsFormStatus;
            TableStatus = TableStatus.tsNone;
            IsCancelled = false;
            ReturnValue = string.Empty;
            Order = string.Empty;
        }
        public abstract void SetField();

        public Table Copy()
        {
            return ((Table)this.MemberwiseClone());
        }

        public Boolean getPermission(Macro.Action aAction)
        {
            return Permission[(int)aAction];
        }
    }
}
