using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace Bus_Ticket_Booking_Management_System.DAL
{
    public class DAL_Station : DAL_Helper
    {
        public DataTable PR_AllBusesList(int PageNumber)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_SelectAll_Station");
                if(PageNumber  > 0)
                {
                    sqlDatabase.AddInParameter(dbCommand, "@PageNumber", DbType.Int32, PageNumber);

                }
                else
                {
                    sqlDatabase.AddInParameter(dbCommand, "@PageNumber", DbType.Int32, 1);
                }
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
