using System.ComponentModel.DataAnnotations;

namespace Bus_Ticket_Booking_Management_System.Areas.Ticket.Models
{
    public class TicketSearchmodel
    {
        [Required(ErrorMessage ="Please Select Source City")]
        public int SourceID { get; set; }
        [Required(ErrorMessage = "Please Select Destination City")]
        public int DestinationID { get; set; }
        public DateTime Date { get; set; }

    }

    public class DiaplaySerchedRouteDetail
    {
        public string BusTypeName { get; set; }
        public int routeid { get; set; }
        public int capacity { get; set; }

        public string TripCode { get; set; }
        public string DeptTime { get; set;}
        public string ArrivalTime { get; set; }

        public string Origin { get; set;}
        public string Destination { get; set; }

        public string Duration { get; set;}  
        public double Fare { get; set;}

    }
}
