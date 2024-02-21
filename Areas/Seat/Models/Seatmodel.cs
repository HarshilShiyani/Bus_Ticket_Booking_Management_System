using System.ComponentModel.DataAnnotations;

namespace Bus_Ticket_Booking_Management_System.Areas.Seat.Models
{
    public class Seatmodel
    {
        public int SeatID { get; set; }
        public int roueSeatID { get; set; }

        public int isAvailable { get; set; }
    }
    public class PassengerDetails
    {
        public string selectedSeats { get; set; }
        public string PassengerName { get; set; }
        public string EmailID { get; set; }
        public string MobiliNo { get; set; }

        public string onDate { get; set; }

        public string routeId { get; set; }
        public string sourceID { get; set; }

        public string destinationID { get; set; }
        public string fare { get; set; }

    }

}
