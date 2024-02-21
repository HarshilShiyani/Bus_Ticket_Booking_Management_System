using Bus_Ticket_Booking_Management_System.Areas.Seat.Models;
using Bus_Ticket_Booking_Management_System.Areas.Ticket.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace Bus_Ticket_Booking_Management_System.DAL
{
    public class DAL_Ticket : DAL_Helper
    {
        SqlDatabase sqlDatabase = new SqlDatabase(ConnString);

        #region SerchTicket
        public List<DiaplaySerchedRouteDetail> SerchTicket(TicketSearchmodel ticketSearchmodel)
        {

            DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Search_Route");
            sqlDatabase.AddInParameter(dbCommand, "@SourceId", DbType.Int32, ticketSearchmodel.SourceID);
            sqlDatabase.AddInParameter(dbCommand, "@DestinationId", DbType.Int32, ticketSearchmodel.DestinationID);
            sqlDatabase.AddInParameter(dbCommand, "@date", DbType.Date, ticketSearchmodel.JourneyDate);

            List<DiaplaySerchedRouteDetail> diaplaySerchedRouteDetails = new List<DiaplaySerchedRouteDetail>();
            using (IDataReader row = sqlDatabase.ExecuteReader(dbCommand))
            {
                while (row.Read())
                {
                    DiaplaySerchedRouteDetail temp = new DiaplaySerchedRouteDetail();

                    temp.routeid = Convert.ToInt32(row["routeid"].ToString());
                    temp.BusTypeName = row["TypeName"].ToString();
                    temp.TripCode = row["RouteName"].ToString();

                    temp.Origin = row["Source"].ToString();
                    temp.Destination = row["Destination"].ToString();

                    temp.DeptTime = row["SourceStationTime"].ToString();
                    temp.ArrivalTime = row["DestinationStationTime"].ToString();
                    temp.Duration = row["TimeDifferenceInHHMM"].ToString();
                    temp.capacity = Convert.ToInt32(row["capacity"].ToString());
                    temp.Fare = Calculatedfare(Double.Parse(row["Distance"].ToString()));
                    diaplaySerchedRouteDetails.Add(temp);
                }
            }

            return diaplaySerchedRouteDetails;
        }
        #endregion

        #region Calculatedfare
        public int Calculatedfare(double temp)
        {
            double totalFare = 30 + (temp * 1.5);
            int roundedFare = (int)Math.Round(totalFare);
            return roundedFare;
        }
        #endregion

        public int InsertTicketDetailwithPassengerInfo(PassengerDetails passengerDetails)
        {
            DbCommand dbc = sqlDatabase.GetStoredProcCommand("PR_InsertTicketDetailwithPassengerInfo");
            sqlDatabase.AddInParameter(dbc, "@PassengerName", DbType.String, passengerDetails.PassengerName);
            sqlDatabase.AddInParameter(dbc, "@EmailID", DbType.String, passengerDetails.EmailID);
            sqlDatabase.AddInParameter(dbc, "@MobiliNo", DbType.String, passengerDetails.MobiliNo);
            sqlDatabase.AddInParameter(dbc, "@onDate", DbType.Date, Convert.ToDateTime(passengerDetails.onDate));
            sqlDatabase.AddInParameter(dbc, "@routeId", DbType.Int32, passengerDetails.routeId);
            sqlDatabase.AddInParameter(dbc, "@sourceID", DbType.Int32, passengerDetails.sourceID);
            sqlDatabase.AddInParameter(dbc, "@destinationID", DbType.Int32, passengerDetails.destinationID);
            sqlDatabase.AddInParameter(dbc, "@fare", DbType.Double, passengerDetails.fare);
            sqlDatabase.AddInParameter(dbc, "@bookedseat", DbType.String, passengerDetails.selectedSeats);

            return Convert.ToInt32(sqlDatabase.ExecuteScalar(dbc));
        }

        public DataTable PR_SelectTicketByTicketID(int ticketid)
        {
            DbCommand dbc = sqlDatabase.GetStoredProcCommand("PR_SelectTicketByTicketID");
            sqlDatabase.AddInParameter(dbc, "@ticketId", DbType.Int32, ticketid);
            DataTable dataTable = new DataTable();
            using (IDataReader reader = sqlDatabase.ExecuteReader(dbc))
            {
                 dataTable.Load(reader);
            }
            return dataTable;
        }
    }
}
