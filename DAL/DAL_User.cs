using Bus_Ticket_Booking_Management_System.Areas.Station.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace Bus_Ticket_Booking_Management_System.DAL
{
    public class DAL_User : DAL_Helper
    {
        SqlDatabase sqlDatabase = new SqlDatabase(ConnString);

        public int User_RemoveImage(int UserId)
        {

            DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_User_RemoveImage");
            sqlDatabase.AddInParameter(dbCommand, "@userId", DbType.Int32, UserId);
            return sqlDatabase.ExecuteNonQuery(dbCommand);
        }
    }
}
