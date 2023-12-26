using Bus_Ticket_Booking_Management_System.DAL;
using System.Data;

namespace Bus_Ticket_Booking_Management_System.BAL
{
    public class BAL_Station
    {
        public DataTable PR_AllBusesList(int PageNumber)
        {
            try
            {
                DAL_Station dAL_Station = new DAL_Station();
                DataTable dt = dAL_Station.PR_AllBusesList(PageNumber);
                return dt;
            }
            catch 
            {
                return null;
            }
        }

    }
}
