using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace Bus_Ticket_Booking_Management_System.DAL
{
    public class DAL_Station : DAL_Helper
    {
        public DataTable PR_AllStationList(int page = 1)
        {
            DataTable dataForPage = GetDataForPage(page);
            return dataForPage;
        }
        public DataTable GetDataForPage(int page)
        {
            SqlDatabase sqlDatabase = new SqlDatabase(ConnString);
            DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_SelectAll_Station");
            sqlDatabase.AddInParameter(dbCommand, "@PageNumber", DbType.Int32, page);
            sqlDatabase.AddInParameter(dbCommand, "@PageSize", DbType.Int32, 10);

            using (IDataReader reader = sqlDatabase.ExecuteReader(dbCommand))
            {
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                return dataTable;
            }
        }
        public int GetTotalRowCount()
        {
            SqlDatabase sqlDatabase = new SqlDatabase(ConnString);
            DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Count_Station");
            return Convert.ToInt32(sqlDatabase.ExecuteScalar(dbCommand));
        }





    }
}
