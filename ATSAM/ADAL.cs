using System.Data;
using System.Data.SqlClient;

namespace Atsam
{
    // Data Access Layer
    public abstract class ADAL 
    {
        protected ISA pSA;
        public abstract SqlException GetDataTable(ref DataTable dtDataTable, string strSQL);

        public abstract SqlException SetDataTable(DataTable dtDataTable, string strSQL);

        public abstract SqlException ExecuteNonQuery(string strSQL);

        public abstract object ExecuteScalar(string strSQL);
    }
}
