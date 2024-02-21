using Bus_Ticket_Booking_Management_System.Areas.Seat.Models;
using DocumentFormat.OpenXml.VariantTypes;
using Humanizer;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace Bus_Ticket_Booking_Management_System.DAL
{
    public class DAL_Seat : DAL_Helper
    {
        SqlDatabase sqlDatabase = new SqlDatabase(ConnString);
        public List<Seatmodel> SeatLayout(int routeId, DateTime onDate)
        {
            if(routeId != 0)
            {
                List<Seatmodel> seatmodels = new List<Seatmodel>();
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_FetchSeatLayout");
                sqlDatabase.AddInParameter(dbCommand, "@ondate", DbType.DateTime, onDate);
                sqlDatabase.AddInParameter(dbCommand, "@routeId", DbType.Int32, routeId);
                using (IDataReader dr = sqlDatabase.ExecuteReader(dbCommand))
                {
                    while (dr.Read())
                    {
                        Seatmodel seatmodel = new Seatmodel();
                        seatmodel.SeatID = Convert.ToInt32(dr["seatid"]);
                        seatmodel.roueSeatID = Convert.ToInt32(dr["RouteseatID"]);
                        seatmodel.isAvailable = Convert.ToInt32(dr["isavailable"]);
                        seatmodels.Add(seatmodel);
                    }
                }
                return seatmodels;
            }
            else
            {
                return null;
            }
            
        }

        public int UpdateSlectedSeatStatus(int SeatID)
        {
            DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_UpdateSlectedSeatStatus");
            sqlDatabase.AddInParameter(dbCommand, "@SeatID", DbType.Int32, SeatID);
            return sqlDatabase.ExecuteNonQuery(dbCommand);
        }

        public object CheckSeatListIsInserted(int routeid,DateTime Date)
        {
            DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_CheckSeatIsInserted");
            sqlDatabase.AddInParameter(dbCommand, "@Date", DbType.DateTime, @Date);
            sqlDatabase.AddInParameter(dbCommand, "@routeid", DbType.Int32, routeid);
            object result = sqlDatabase.ExecuteScalar(dbCommand);
            return sqlDatabase.ExecuteScalar(dbCommand);

        }

        public object no_Of_Seat_In_Bus(int routeid)
        {
            DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Find_numberOfSeatInRoute");
            sqlDatabase.AddInParameter(dbCommand, "@routeid", DbType.Int32, routeid);
            return sqlDatabase.ExecuteScalar(dbCommand);

        }

        public int Insert_Seat_Detail_RouteWiseDateWiseSeat(int routeid,DateTime dateTime ,int routeseatid)
        {
            DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Insert_RouteWiseDateWiseSeat");
            sqlDatabase.AddInParameter(dbCommand, "@routeid", DbType.Int32, routeid);
            sqlDatabase.AddInParameter(dbCommand, "@Date", DbType.DateTime, dateTime);
            sqlDatabase.AddInParameter(dbCommand, "@RouteSeatID", DbType.Int32, routeseatid);
            return sqlDatabase.ExecuteNonQuery(dbCommand);
        }

    }
}
