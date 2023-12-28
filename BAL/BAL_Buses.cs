using Bus_Ticket_Booking_Management_System.DAL;
using System.Data;

namespace Bus_Ticket_Booking_Management_System.BAL
{
    public class BAL_Buses
    {
        public DataTable PR_AllBusesList()
        {
            try
            {
                DAL_Buses dAL_Buses = new DAL_Buses();
                DataTable dt=  dAL_Buses.PR_AllBusesList();
                return dt;
            }
            catch
            {
                return null;
            }
        }
    }
}
