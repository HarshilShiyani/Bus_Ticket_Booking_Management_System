using Bus_Ticket_Booking_Management_System.Areas.Station.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace Bus_Ticket_Booking_Management_System.DAL
{
    public class DAL_Station : DAL_Helper
    {
        SqlDatabase sqlDatabase = new SqlDatabase(ConnString);

        #region SelectAllStationList
        public DataTable PR_AllStationList(int page = 1)
        {
            DataTable dataForPage = GetDataForPage(page);
            return dataForPage;
        }
        public DataTable GetDataForPage(int page)
        {
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
            DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Count_Station");
            return Convert.ToInt32(sqlDatabase.ExecuteScalar(dbCommand));
        }
        #endregion

        #region DeleteStation
        public int DeleteStationByID(int StationID)
        {
            DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Delete_StationById");
            sqlDatabase.AddInParameter(dbCommand, "@StationID", DbType.Int32, StationID);
            return sqlDatabase.ExecuteNonQuery(dbCommand);
        }
        #endregion

        #region StationAddEdit
        public int StationAddEdit(Stationmodel station,int? StationID)
        {
            if (StationID != null)
            {
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_UpadteStationByID");
                sqlDatabase.AddInParameter(dbCommand, "@StationID", DbType.String, StationID);
                sqlDatabase.AddInParameter(dbCommand, "@StationName", DbType.String, station.StationName);
                sqlDatabase.AddInParameter(dbCommand, "@Location", DbType.String, station.Location);
                sqlDatabase.AddInParameter(dbCommand, "@Description", DbType.String, station.Description);
                return sqlDatabase.ExecuteNonQuery(dbCommand);
            }
            else
            {
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Insert_Station");
                sqlDatabase.AddInParameter(dbCommand, "@StationName", DbType.String, station.StationName);
                sqlDatabase.AddInParameter(dbCommand, "@Location", DbType.String, station.Location);
                sqlDatabase.AddInParameter(dbCommand, "@Description", DbType.String, station.Description);
                return sqlDatabase.ExecuteNonQuery(dbCommand);
            }
            
        }
        #endregion

        #region SelectStationByID
        public Stationmodel SelectStationByID(int? StationID)
        {
            SqlDatabase sqlDatabase = new SqlDatabase(ConnString);
            DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_SelectStationByStationID");
            sqlDatabase.AddInParameter(dbCommand, "@StationID", DbType.Int32, StationID);
            Stationmodel stationmodel = new Stationmodel();
            using (IDataReader reader = sqlDatabase.ExecuteReader(dbCommand))
            {
                if(reader.Read())
                {
                    stationmodel.StationName = reader["StationName"].ToString();
                    stationmodel.Location = reader["Location"].ToString();
                    stationmodel.Description = reader["Description"].ToString();
                }

            }
            return stationmodel;

        }
        #endregion

        #region Station_DDL
        public List<StationDDL> Station_DDL()
        {
            List<StationDDL> stationDDL = new List<StationDDL>();
            DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Station_DDL");
            using(IDataReader dr =sqlDatabase.ExecuteReader(dbCommand))
            {
                while(dr.Read())
                {
                    StationDDL tempStation = new StationDDL();
                    tempStation.Stid = Convert.ToInt32(dr["StationID"]);
                    tempStation.StationName = dr["StationName"].ToString();
                    stationDDL.Add(tempStation);
                }
                
            }
            return stationDDL;
        }
        #endregion

    }
}
