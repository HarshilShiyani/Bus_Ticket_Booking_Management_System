using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;

namespace Bus_Ticket_Booking_Management_System.DAL
{
    public class DAL_Buses : DAL_Helper
    {
        public DataTable PR_AllBusesList()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_AllBusesList");
                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dt.Load(dr);
                }
                return dt;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
