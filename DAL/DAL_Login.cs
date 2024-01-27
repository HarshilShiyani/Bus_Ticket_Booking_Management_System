using Bus_Ticket_Booking_Management_System.Areas.Bus.Models;
using Bus_Ticket_Booking_Management_System.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;


namespace Bus_Ticket_Booking_Management_System.DAL
{
    public class DAL_Login :DAL_Helper
    {
        #region CheckLogin
        public DataTable CheckLogin(Login login)
        {
            SqlDatabase sqlDatabase = new SqlDatabase(ConnString);
            DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("CheckLogin");
            sqlDatabase.AddInParameter(dbCommand, "@Username", DbType.String, login.Username);
            sqlDatabase.AddInParameter(dbCommand, "@Password", DbType.String, login.Password);
            DataTable dt = new DataTable();
            using (IDataReader reader = sqlDatabase.ExecuteReader(dbCommand))
            {
                dt.Load(reader);
                return dt;
            }
        }
        #endregion
    }
}
