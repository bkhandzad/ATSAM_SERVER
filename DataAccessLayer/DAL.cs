using Atsam;
using Macro;
using Atsam.Server;
using ServerCommonLayer;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class DAL : ADAL
    {
        public DAL()
        {
            pSA = new SA();
        }

        public override SqlException ExecuteNonQuery(string strSQL)
        {
            SqlConnection cConnection = new SqlConnection(pSA.getConnectionString());
            try
            {
                SqlCommand cCommand = new SqlCommand();
                cConnection.Open();
                cCommand.Connection = cConnection;
                cCommand.CommandType = CommandType.Text;
                cCommand.CommandText = strSQL;
                cCommand.ExecuteNonQuery();
                cCommand.Dispose();
                return (null);
            }
            catch (SqlException eSqlException)
            {
                return (eSqlException);
            }
            finally
            {
                cConnection.Close();
            }
        }

        public override object ExecuteScalar(string strSQL)
        {
            SqlConnection cConnection = new SqlConnection(pSA.getConnectionString());
            try
            {
                cConnection.Open();
                SqlCommand cCommand = new SqlCommand(strSQL, cConnection);
                return (cCommand.ExecuteScalar());
            }
            catch (SqlException eSqlException)
            {
                return (eSqlException);
            }
            finally
            {
                cConnection.Close();
            }
        }

        public override SqlException GetDataTable(ref DataTable dtDataTable, string strSQL)
        {
            try
            {
                SqlConnection cConnection = new SqlConnection(pSA.getConnectionString());
                SqlDataAdapter daDataAdapter = new SqlDataAdapter(strSQL, cConnection);
                daDataAdapter.Fill(dtDataTable);
                return (null);
            }
            catch (SqlException eSqlException)
            {
                return (eSqlException);
            }
        }

        public override SqlException SetDataTable(DataTable dtDataTable, string strSQL)
        {
            try
            {
                SqlConnection cConnection = new SqlConnection(pSA.getConnectionString());
                SqlDataAdapter daDataAdapter = new SqlDataAdapter(strSQL, cConnection);
                SqlCommandBuilder cbCommandBuilder = new SqlCommandBuilder(daDataAdapter);
                daDataAdapter.Update(dtDataTable);
                return (null);
            }
            catch (SqlException eSqlException)
            {
                return (eSqlException);
            }
        }
    }
}
