using Bus_Ticket_Booking_Management_System.Areas.Ticket.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace Bus_Ticket_Booking_Management_System.DAL
{
    public class DAL_Ticket :DAL_Helper
    {
        SqlDatabase sqlDatabase = new SqlDatabase(ConnString);
        public List<DiaplaySerchedRouteDetail> SerchTicket(TicketSearchmodel ticketSearchmodel)
        {
            DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Search_Route");
            sqlDatabase.AddInParameter(dbCommand, "@SourceId", DbType.Int32, ticketSearchmodel.SourceID);
            sqlDatabase.AddInParameter(dbCommand, "@DestinationId", DbType.Int32, ticketSearchmodel.DestinationID);

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
                    temp.capacity= Convert.ToInt32( row["capacity"].ToString());
                    temp.Fare = Calculatedfare( Double.Parse(row["Distance"].ToString()) );
                    diaplaySerchedRouteDetails.Add(temp);
                }
            }

            return diaplaySerchedRouteDetails;
        }

        public int Calculatedfare(double temp)
        {
            double totalFare = 30 + (temp * 1.5);
            int roundedFare = (int)Math.Round(totalFare);
            return roundedFare;
        }

    }
}
