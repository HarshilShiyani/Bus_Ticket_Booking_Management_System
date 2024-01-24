using Bus_Ticket_Booking_Management_System.Areas.Bus.Models;
using Bus_Ticket_Booking_Management_System.Areas.Routes.Models;
using Bus_Ticket_Booking_Management_System.Areas.Station.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;

namespace Bus_Ticket_Booking_Management_System.DAL
{
    public class DAL_Route : DAL_Helper
    {
        SqlDatabase sqlDatabase = new SqlDatabase(ConnString);

        #region GelAllRoute
        public DataTable GelAllRoute()
        {
            DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_SelectAllRoute");

            using (IDataReader reader = sqlDatabase.ExecuteReader(dbCommand))
            {
                DataTable dt = new DataTable();
                dt.Load(reader);
                return dt;
            }
        }
        #endregion

        #region SelectRoute_By_RouteId
        public Routemodel SelectRoute_By_RouteId(int? routeId)
        {
            Routemodel routemodel = new Routemodel();
            DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_SelectRoute_By_RouteId");
            sqlDatabase.AddInParameter(dbCommand, "@RouteID", DbType.Int32, routeId);
            using (IDataReader dr = sqlDatabase.ExecuteReader(dbCommand))
            {
                if (dr.Read())
                {
                    routemodel.RouteID = Convert.ToInt32(dr["RouteID"]);
                    routemodel.RouteName = dr["RouteName"].ToString();
                    routemodel.RouteStartTime = DateTime.Parse(dr["Starting_time"].ToString());
                    routemodel.StartDate = DateTime.Parse(dr["Starting_date"].ToString());
                    routemodel.EndDate = Convert.ToDateTime(dr["Ending_date"].ToString());
                    routemodel.FirstStation = Convert.ToInt32(dr["SourceStationID"].ToString());
                    routemodel.LastStation = Convert.ToInt32(dr["DestinationStationID"].ToString());
                    routemodel.BusID = Convert.ToInt32(dr["BusID"]);

                }
            }
            return routemodel;
        }
        #endregion

        #region RouteAddEdit
        public int RouteAddEdit(Routemodel routemodel, int? RouteID)
        {
            if (RouteID != 0)
            {
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Update_RouteByRouteID");
                sqlDatabase.AddInParameter(dbCommand, "@RouteID", DbType.Int32, RouteID);
                sqlDatabase.AddInParameter(dbCommand, "@RouteName", DbType.String, routemodel.RouteName);
                sqlDatabase.AddInParameter(dbCommand, "@SourceStationID", DbType.Int32, routemodel.FirstStation);
                sqlDatabase.AddInParameter(dbCommand, "@DestinationStationID", DbType.Int32, routemodel.LastStation);
                sqlDatabase.AddInParameter(dbCommand, "@Starting_date", DbType.DateTime, routemodel.StartDate);
                sqlDatabase.AddInParameter(dbCommand, "@Ending_date", DbType.DateTime, routemodel.EndDate);
                sqlDatabase.AddInParameter(dbCommand, "@Starting_time", DbType.Time, routemodel.RouteStartTime);
                sqlDatabase.AddInParameter(dbCommand, "@BusID", DbType.Int32, routemodel.BusID);
                return sqlDatabase.ExecuteNonQuery(dbCommand);
            }
            else
            {
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Insert_Route");
                sqlDatabase.AddInParameter(dbCommand, "@RouteName", DbType.String, routemodel.RouteName);
                sqlDatabase.AddInParameter(dbCommand, "@SourceStationID", DbType.Int32, routemodel.FirstStation);
                sqlDatabase.AddInParameter(dbCommand, "@DestinationStationID", DbType.Int32, routemodel.LastStation);
                sqlDatabase.AddInParameter(dbCommand, "@Starting_date", DbType.DateTime, routemodel.StartDate);
                sqlDatabase.AddInParameter(dbCommand, "@Ending_date", DbType.DateTime, routemodel.EndDate);
                sqlDatabase.AddInParameter(dbCommand, "@Starting_time", DbType.DateTime, routemodel.RouteStartTime);
                sqlDatabase.AddInParameter(dbCommand, "@BusID", DbType.Int32, routemodel.BusID);
                return sqlDatabase.ExecuteNonQuery(dbCommand);
            }

        }
        #endregion

        #region DetailRouteByRouteID
        public DataTable DetailRouteByRouteID(int RouteID)
        {
            DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_DetailRoute_By_RouteID");
            sqlDatabase.AddInParameter(dbCommand, "@RouteID", DbType.Int32, RouteID);

            using (IDataReader reader = sqlDatabase.ExecuteReader(dbCommand))
            {
                DataTable dt = new DataTable();
                dt.Load(reader);
                return dt;
            }
        }
        #endregion

        #region StationDetailOfRoute
        public DataTable StationDetailOfRoute(int RouteID)
        {
            DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_StationDetailOfRoute");
            sqlDatabase.AddInParameter(dbCommand, "@RouteID", DbType.Int32, RouteID);

            using (IDataReader reader = sqlDatabase.ExecuteReader(dbCommand))
            {
                DataTable dt = new DataTable();
                dt.Load(reader);
                return dt;
            }
        }
        #endregion

        #region RouteAddEdtRouteStatioin
        public int RouteAddEdtRouteStation(RouteStationModel stationModel)
        {

            if (stationModel.RouteDetailID != 0)
            {
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Update_RouteStation");

                sqlDatabase.AddInParameter(dbCommand, "@RouteDetailID", DbType.Int32, stationModel.RouteDetailID);
                sqlDatabase.AddInParameter(dbCommand, "@StationID", DbType.Int32, stationModel.RouteStationId);
                sqlDatabase.AddInParameter(dbCommand, "@StationOrder", DbType.Int32, stationModel.StationOrder);
                sqlDatabase.AddInParameter(dbCommand, "@StationTime", DbType.Time, stationModel.StationTime);
                sqlDatabase.AddInParameter(dbCommand, "@DistanceFromOrigin", DbType.Decimal, stationModel.DistanceFromOrigin);
                return sqlDatabase.ExecuteNonQuery(dbCommand);

            }
            else
            {
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Insert_RouteStation");

                sqlDatabase.AddInParameter(dbCommand, "@RouteID", DbType.Int32, stationModel.RouteID);
                sqlDatabase.AddInParameter(dbCommand, "@StationID", DbType.Int32, stationModel.RouteStationId);
                sqlDatabase.AddInParameter(dbCommand, "@StationOrder", DbType.Int32, stationModel.StationOrder);
                sqlDatabase.AddInParameter(dbCommand, "@StationTime", DbType.Time, stationModel.StationTime);
                sqlDatabase.AddInParameter(dbCommand, "@DistanceFromOrigin", DbType.Decimal, stationModel.DistanceFromOrigin);
                return sqlDatabase.ExecuteNonQuery(dbCommand);

            }

        }
        #endregion

        #region StationEditModelFillDataOfRoute
        public RouteStationModel StationEditModelFillDataOfRoute(int RouteDetailID)
        {
            RouteStationModel routeStationModel = new RouteStationModel();
            DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_RouteStationDetailBy_RouteDetailId");

            sqlDatabase.AddInParameter(dbCommand, "@RouteDetailID", DbType.Int32, RouteDetailID);
            using (IDataReader dr = sqlDatabase.ExecuteReader(dbCommand))
            {
                if (dr.Read())
                {
                    routeStationModel.RouteID = Convert.ToInt32(dr["RouteID"]);
                    routeStationModel.RouteDetailID = Convert.ToInt32(dr["RouteDetailID"]);
                    routeStationModel.StationOrder = Convert.ToInt32(dr["StationOrder"]);

                    routeStationModel.RouteStationId = Convert.ToInt32(dr["StationID"]);
                    routeStationModel.StationTime = DateTime.Parse(dr["StationTime"].ToString());
                    routeStationModel.DistanceFromOrigin = Convert.ToDouble(dr["DistanceFromOrigin"]);


                }
            }
            return routeStationModel;
        }
        #endregion

        #region Delete_RouteStation
        public int Delete_RouteStation(int RouteDetailID)
        {
            DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Delete_RouteStation");
            sqlDatabase.AddInParameter(dbCommand, "@RouteDetailID", DbType.Int32, RouteDetailID);
            return sqlDatabase.ExecuteNonQuery(dbCommand);
        }

        #endregion


    }
}
