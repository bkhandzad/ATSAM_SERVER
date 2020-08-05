using System;
using System.Data;

namespace Atsam
{
    public abstract class _Table
    {
        protected int _PK_TableCode;
        protected string _TableName;
        protected string _Alias;
        protected string _Caption;
        protected int _FK_TableTypeCode;
        protected string _Description;
        protected Boolean _Visible;
        protected int _FK_ParentTableCode;
        protected int _FK_FieldsTableCode;
        protected int _Rank;
        protected string _LinkPage;
        protected string _Filter;
        protected string _Order;

        protected FormStatus _FormStatus;
        protected TableStatus _TableStatus;
        protected Boolean _IsCancelled;
        protected string _ReturnValue;
        protected Boolean[] _Permission = new Boolean[System.Enum.GetValues(typeof(Atsam.Action)).Length];

        public _Field __Field;

        public DataRow drParent = null;

        public _Table(DataRow drDataRow, FormStatus fsFormStatus = FormStatus.fsMain)
        {
            _PK_TableCode = Convert.ToInt32(drDataRow["PK_TableCode"].ToString());
            _TableName = drDataRow["TableName"].ToString();
            _Alias = drDataRow["Alias"].ToString();
            _Caption = drDataRow["Caption"].ToString();
            _FK_TableTypeCode = Convert.ToInt32(drDataRow["FK_TableTypeCode"].ToString());
            _Description = drDataRow["Description"].ToString();
            _Visible = Convert.ToBoolean(drDataRow["Visible"].ToString());
            _FK_ParentTableCode = Convert.ToInt32(drDataRow["FK_ParentTableCode"].ToString());
            _FK_FieldsTableCode = Convert.ToInt32(drDataRow["FK_FieldsTableCode"].ToString());
            _Rank = Convert.ToInt32(drDataRow["Rank"].ToString());
            _LinkPage = drDataRow["LinkPage"].ToString();
            _Filter = drDataRow["Filter"].ToString();

            _FormStatus = fsFormStatus;
            _TableStatus = TableStatus.tsNone;
            _IsCancelled = false;
            _ReturnValue = string.Empty;
            _Order = string.Empty;

            _Permission = AUser._Permission.GetPermission(AUser.WorkGroupCode, PK_TableCode);
        }

        public _Table Copy()
        {
            return ((_Table)this.MemberwiseClone());
        }

        public Boolean Permission(Atsam.Action aAction)
        {
            return _Permission[(int)aAction];
        }
        #region Properties

        public int PK_TableCode
        {
            get
            {
                return _PK_TableCode;
            }
        }

        public string TableName
        {
            get
            {
                return _TableName;
            }
        }

        public string Alias
        {
            get
            {
                return _Alias;
            }
        }

        public string Caption
        {
            get
            {
                return _Caption;
            }
        }

        public int FK_TableTypeCode
        {
            get
            {
                return _FK_TableTypeCode;
            }
        }

        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
            }
        }

        public Boolean Visible
        {
            get
            {
                return _Visible;
            }
        }

        public int FK_ParentTableCode
        {
            get
            {
                return _FK_ParentTableCode;
            }
        }

        public int FK_FieldsTableCode
        {
            get
            {
                return _FK_FieldsTableCode;
            }
        }

        public int Rank
        {
            get
            {
                return _Rank;
            }
        }

        public string LinkPage
        {
            get
            {
                return _LinkPage;
            }
        }

        public string Filter
        {
            get
            {
                return (_Filter);
            }
            set
            {
                _Filter = value;
            }
        }

        public FormStatus FormStatus
        {
            get
            {
                return _FormStatus;
            }
        }

        public TableStatus TableStatus
        {
            get
            {
                return _TableStatus;
            }

            set
            {
                _TableStatus = value;
            }
        }

        public Boolean IsCancelled
        {
            get
            {
                return _IsCancelled;
            }

            set
            {
                _IsCancelled = value;
            }
        }

        public string ReturnValue
        {
            get
            {
                return _ReturnValue;
            }

            set
            {
                _ReturnValue = value;
            }
        }

        public string Order
        {
            get
            {
                return _Order;
            }

            set
            {
                _Order = value;
            }
        }

        #endregion
    }

    public abstract class _Field
    {
        protected int _pk_FKTableCode;
        protected int _pk_FieldCode;
        protected string _FieldName;
        protected string _Caption;
        protected int _FK_KeyTypeCode;
        protected int _FK_FieldTypeCode;
        protected int _FK_FieldOperationCode;
        protected int _FieldLength;
        protected Boolean _Required;
        protected string _DefaultValue;
        protected Boolean _Visible;
        protected Boolean _Enabled;
        protected string _Description;
        protected string _LookupSQL;
        protected string _MasterReferenceField;

        private string _Value;
        private Boolean _Active = false;
        private DataTable _DataTable;

        public void SetDataField(int intFieldCode)
        {
            DataRow[] drDataRow = _DataTable.Select("pk_FieldCode = " + intFieldCode.ToString());
            if (drDataRow.Length == 1)
            {
                _pk_FKTableCode = Convert.ToInt32(drDataRow[0]["pk_FkTableCode"].ToString());
                _pk_FieldCode = Convert.ToInt32(drDataRow[0]["pk_FieldCode"].ToString());
                _FieldName = drDataRow[0]["FieldName"].ToString();
                _Caption = drDataRow[0]["Caption"].ToString();
                _FK_KeyTypeCode = Convert.ToInt32(drDataRow[0]["FK_KeyTypeCode"].ToString());
                _FK_FieldTypeCode = Convert.ToInt32(drDataRow[0]["FK_FieldTypeCode"].ToString());
                _FK_FieldOperationCode = Convert.ToInt32(drDataRow[0]["FK_FieldOperationCode"].ToString());
                _FieldLength = Convert.ToInt32(drDataRow[0]["FieldLength"].ToString());
                _Required = (drDataRow[0]["Required"].ToString() == string.Empty) ? false : Convert.ToBoolean(drDataRow[0]["Required"].ToString());
                _DefaultValue = drDataRow[0]["DefaultValue"].ToString();
                _Visible = (drDataRow[0]["Visible"].ToString() == string.Empty) ? false : Convert.ToBoolean(drDataRow[0]["Visible"].ToString());
                _Enabled = (drDataRow[0]["Enabled"].ToString() == string.Empty) ? false : Convert.ToBoolean(drDataRow[0]["Enabled"].ToString());
                _Description = drDataRow[0]["Description"].ToString();
                _LookupSQL = drDataRow[0]["LookupSQL"].ToString();
                _MasterReferenceField = drDataRow[0]["MasterReferenceField"].ToString();
            }
        }

        public void SetDataField(string strFiledName)
        {
            DataRow[] drDataRow = _DataTable.Select("FieldName = '" + strFiledName.Trim() + "'");
            if (drDataRow.Length == 1)
            {
                _pk_FKTableCode = Convert.ToInt32(drDataRow[0]["pk_FkTableCode"].ToString());
                _pk_FieldCode = Convert.ToInt32(drDataRow[0]["pk_FieldCode"].ToString());
                _FieldName = drDataRow[0]["FieldName"].ToString();
                _Caption = drDataRow[0]["Caption"].ToString();
                _FK_KeyTypeCode = Convert.ToInt32(drDataRow[0]["FK_KeyTypeCode"].ToString());
                _FK_FieldTypeCode = Convert.ToInt32(drDataRow[0]["FK_FieldTypeCode"].ToString());
                _FK_FieldOperationCode = Convert.ToInt32(drDataRow[0]["FK_FieldOperationCode"].ToString());
                _FieldLength = Convert.ToInt32(drDataRow[0]["FieldLength"].ToString());
                _Required = Convert.ToBoolean(drDataRow[0]["Required"].ToString());
                _DefaultValue = drDataRow[0]["DefaultValue"].ToString();
                _Visible = Convert.ToBoolean(drDataRow[0]["Visible"].ToString());
                _Enabled = Convert.ToBoolean(drDataRow[0]["Enabled"].ToString());
                _Description = drDataRow[0]["Description"].ToString();
                _LookupSQL = drDataRow[0]["LookupSQL"].ToString();
                _MasterReferenceField = drDataRow[0]["MasterReferenceField"].ToString();
            }
        }

        #region Properties

        public int pk_FKTableCode
        {
            get
            {
                return _pk_FKTableCode;
            }
        }

        public int pk_FieldCode
        {
            get
            {
                return _pk_FieldCode;
            }
        }

        public string FieldName
        {
            get
            {
                return _FieldName;
            }
        }

        public string Caption
        {
            get
            {
                return _Caption;
            }
        }

        public int FK_KeyTypeCode
        {
            get
            {
                return _FK_KeyTypeCode;
            }
        }

        public int FK_FieldTypeCode
        {
            get
            {
                return _FK_FieldTypeCode;
            }
        }

        public int FK_FieldOperationCode
        {
            get
            {
                return _FK_FieldOperationCode;
            }
        }

        public int FieldLength
        {
            get
            {
                return _FieldLength;
            }
        }

        public Boolean Required
        {
            get
            {
                return _Required;
            }
        }

        public string DefaultValue
        {
            get
            {
                return _DefaultValue;
            }
        }

        public Boolean Visible
        {
            get
            {
                return _Visible;
            }
        }

        public Boolean Enabled
        {
            get
            {
                return _Enabled;
            }
        }

        public string Description
        {
            get
            {
                return _Description;
            }
        }

        public string LookupSQL
        {
            get
            {
                return _LookupSQL;
            }
        }

        public string MasterReferenceField
        {
            get
            {
                return _MasterReferenceField;
            }
        }

        public Boolean Active
        {
            get
            {
                return _Active;
            }
        }

        public DataTable DataTable
        {
            get
            {
                return _DataTable;
            }
        }

        public string Value
        {
            get
            {
                return _Value;
            }

            set
            {
                _Value = value;
            }
        }

        #endregion
    }
}
