using Bus_Ticket_Booking_Management_System.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;
using Bus_Ticket_Booking_Management_System.BAL;

namespace Bus_Ticket_Booking_Management_System.DAL
{
    public class DAL_Login :DAL_Helper
    {
        SqlDatabase sqlDatabase = new SqlDatabase(ConnString);

        #region CheckLogin
        public DataTable CheckLogin(Login login)
        {
            SqlDatabase sqlDatabase = new SqlDatabase(ConnString);
            DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_CheckLogin");
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

        #region CheckPasswordAtChangePassword
        public DataTable CheckPasswordAtChangePassword(string currentPassword)
        {
            DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_CheckLogin");
            sqlDatabase.AddInParameter(dbCommand, "@Username", DbType.String, @CV.username());
            sqlDatabase.AddInParameter(dbCommand, "@Password", DbType.String, currentPassword);
            DataTable dt = new DataTable();
            using (IDataReader reader = sqlDatabase.ExecuteReader(dbCommand))
            {
                dt.Load(reader);
                return dt;
            }
        }
        #endregion

        #region ChangePasswordByCurrentPassword
        public bool ChangePasswordByCurrentPassword(string newPassword)
        {
            DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_ChangePasswordByCurrentPassword");
            sqlDatabase.AddInParameter(dbCommand, "@userid", DbType.String, @CV.UserID());
            sqlDatabase.AddInParameter(dbCommand, "@newPassword", DbType.String, newPassword);
            if(Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        public string? FetchPasswordByUsername(string username)
        {
            DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_FetchPasswordByUsername");
            sqlDatabase.AddInParameter(dbCommand, "@username", DbType.String, username);
            dynamic password=sqlDatabase.ExecuteScalar(dbCommand);
            return password;
        }

        public int PR_UpdateUserDetail(UserModel userModel)
        {
            DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_UpdateUserDetail");
            sqlDatabase.AddInParameter(dbCommand, "@Username", DbType.String, userModel.Username);
            sqlDatabase.AddInParameter(dbCommand, "@EmailID", DbType.String, userModel.EmailID);
            sqlDatabase.AddInParameter(dbCommand, "@MobileNo", DbType.String, userModel.MobileNo);
            sqlDatabase.AddInParameter(dbCommand, "@UserID", DbType.Int32, Convert.ToInt32( @CV.UserID()));
            sqlDatabase.AddInParameter(dbCommand, "@ImagePath", DbType.String, userModel.ImagePath);
            return sqlDatabase.ExecuteNonQuery(dbCommand);

        }

    }
}
