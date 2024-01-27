using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;

namespace Bus_Ticket_Booking_Management_System.DAL
{
    public class DAL_Count : DAL_Helper
    {
        #region GetStationCount
        public int GetStationCount()
        {
            SqlDatabase sqlDatabase = new SqlDatabase(ConnString);
            DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Count_Station");
            return Convert.ToInt32(sqlDatabase.ExecuteScalar(dbCommand));
        }
        #endregion

        #region GetBusCount
        public int GetBusCount()
        {
            SqlDatabase sqlDatabase = new SqlDatabase(ConnString);
            DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Count_Buses");
            return Convert.ToInt32(sqlDatabase.ExecuteScalar(dbCommand));
        }
        #endregion

        #region GetRouteCount
        public int GetRouteCount()
        {
            SqlDatabase sqlDatabase = new SqlDatabase(ConnString);
            DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Count_Route");
            return Convert.ToInt32(sqlDatabase.ExecuteScalar(dbCommand));
        }
        #endregion
    }
}
