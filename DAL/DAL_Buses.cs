using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using Bus_Ticket_Booking_Management_System.Areas.Bus.Models;
using Bus_Ticket_Booking_Management_System.Areas.Station.Models;

namespace Bus_Ticket_Booking_Management_System.DAL
{
    public class DAL_Buses : DAL_Helper
    {
        SqlDatabase sqlDatabase = new SqlDatabase(ConnString);

        #region BusTypeList
        public DataTable BusTypeList()
        {
            try
            {
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_SelectAllBusType");
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

        #endregion

        #region BusTypeAddEdit
        public int BusTypeAddEdit(BusTypemodel busTypemodel, int? BusTypeID)
        {
            if (BusTypeID != 0)
            {
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_UpadteBusType_By_BusTypeID");
                sqlDatabase.AddInParameter(dbCommand, "@BusTypeId", DbType.Int32, BusTypeID);
                sqlDatabase.AddInParameter(dbCommand, "@Typename", DbType.String, busTypemodel.TypeName);
                sqlDatabase.AddInParameter(dbCommand, "@Capacity", DbType.Int32, busTypemodel.Capacity);
                sqlDatabase.AddInParameter(dbCommand, "@Description", DbType.String, busTypemodel.Description);
                return sqlDatabase.ExecuteNonQuery(dbCommand);
            }
            else
            {
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Insert_BusType");
                sqlDatabase.AddInParameter(dbCommand, "@Typename", DbType.String, busTypemodel.TypeName);
                sqlDatabase.AddInParameter(dbCommand, "@Capacity", DbType.Int32, busTypemodel.Capacity);
                sqlDatabase.AddInParameter(dbCommand, "@Description", DbType.String, busTypemodel.Description);
                return sqlDatabase.ExecuteNonQuery(dbCommand);
            }

        }
        #endregion

        #region SelectBusTypeByBusTypeId
        public BusTypemodel SelectBusTypeByBusTypeId(int? BusTypeId)
        {
            DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_SelectBusType_By_BustypeID");
            sqlDatabase.AddInParameter(dbCommand, "@BusTypeId", DbType.Int32, BusTypeId);
            BusTypemodel busTypemodel = new BusTypemodel();
            using (IDataReader reader = sqlDatabase.ExecuteReader(dbCommand))
            {
                if (reader.Read())
                {
                    busTypemodel.TypeName = reader["TypeName"].ToString();
                    busTypemodel.Capacity = Convert.ToInt32(reader["Capacity"]);
                    busTypemodel.Description = reader["Description"].ToString();
                }

            }
            return busTypemodel;

        }
        #endregion

        #region DeleteBusTypeById
        public int DeleteBusTypeByID(int BusTypeByID)
        {
            DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_DeleteBusType");
            sqlDatabase.AddInParameter(dbCommand, "@BusTypeId", DbType.Int32, BusTypeByID);
            return sqlDatabase.ExecuteNonQuery(dbCommand);
        }
        #endregion

        #region BusTypeDDL
        public List<BusTypeDDL> BusTypeDDL()
        {
            DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_BusType_DDL");
            List<BusTypeDDL> busTypeDDL = new List<BusTypeDDL>();
            using (IDataReader dr = sqlDatabase.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    BusTypeDDL temp = new BusTypeDDL();
                    temp.BusTypeID = Convert.ToInt32(dr["bustypeid"]);
                    temp.BusType = dr["typename"].ToString();
                    busTypeDDL.Add(temp);
                }
            }
            return busTypeDDL;

        }
        #endregion

        #region BusList
        public DataTable BusList()
        {
            try
            {
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_SelectAllBuses");
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
        #endregion

        #region SelectBusByBusId
        public Busmodel SelectBusByBusId(int? BusId)
        {
            DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_SelectBusByBusId");
            sqlDatabase.AddInParameter(dbCommand, "@BusId", DbType.Int32, BusId);
            Busmodel busmodel = new Busmodel();
            using (IDataReader reader = sqlDatabase.ExecuteReader(dbCommand))
            {
                if (reader.Read())
                {
                    busmodel.RegistrationNumber = reader["RegistrationNumber"].ToString();
                    busmodel.BusTypeID = Convert.ToInt32(reader["BusTypeID"]);
                    busmodel.NameOfManufacturer= reader["Manufacturer"].ToString();
                    busmodel.ModelNo = reader["ModelNo"].ToString();

                }
            }
            return busmodel;

        }
        #endregion

        #region BusAddEdit
        public int BusAddEdit(Busmodel busmodel, int? BusID)
        {
            if (BusID != 0)
            {
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_UpdateBus_By_BusID");
                sqlDatabase.AddInParameter(dbCommand, "@BusId", DbType.Int32, BusID);
                sqlDatabase.AddInParameter(dbCommand, "@RegistrationNumber", DbType.String, busmodel.RegistrationNumber);
                sqlDatabase.AddInParameter(dbCommand, "@BusTypeID", DbType.Int32, busmodel.BusTypeID);
                sqlDatabase.AddInParameter(dbCommand, "@Manufacturer", DbType.String, busmodel.NameOfManufacturer);
                sqlDatabase.AddInParameter(dbCommand, "@ModelNo", DbType.String, busmodel.ModelNo);

                return sqlDatabase.ExecuteNonQuery(dbCommand);
            }
            else
            {
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Insert_Bus");
                sqlDatabase.AddInParameter(dbCommand, "@RegistrationNumber", DbType.String, busmodel.RegistrationNumber);
                sqlDatabase.AddInParameter(dbCommand, "@BusTypeID", DbType.Int32, busmodel.BusTypeID);
                sqlDatabase.AddInParameter(dbCommand, "@Manufacturer", DbType.String, busmodel.NameOfManufacturer);
                sqlDatabase.AddInParameter(dbCommand, "@ModelNo", DbType.String, busmodel.ModelNo);

                return sqlDatabase.ExecuteNonQuery(dbCommand);
            }

        }
        #endregion

        #region DeleteBusById
        public int DeleteBusByID(int BusID)
        {
            DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_DeleteBus_By_BusID");
            sqlDatabase.AddInParameter(dbCommand, "@BusID", DbType.Int32, BusID);
            return sqlDatabase.ExecuteNonQuery(dbCommand);
        }
        #endregion

        #region Buses_DDL
        public List<Buses_DDL> Buses_DDL()
        {
            List<Buses_DDL> buses_DDLs = new List<Buses_DDL>();
            DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Buses_DDL");
            using (IDataReader dr = sqlDatabase.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    Buses_DDL temp = new Buses_DDL();
                    temp.BusID = Convert.ToInt32(dr["BusID"]);
                    temp.BusBumber = dr["RegistrationNumber"].ToString();
                    buses_DDLs.Add(temp);
                }

            }
            return buses_DDLs;
        }
        #endregion
    }
}
