using System.ComponentModel.DataAnnotations;

namespace Bus_Ticket_Booking_Management_System.Areas.Routes.Models
{
    public class Routemodel
    {


        public int RouteID { get; set; }
        public int BusID { get; set; }

        public string RouteName { get; set; }
        [Required]
        public DateTime RouteStartTime  { get; set; }
        [Required]
        public int FirstStation { get; set; }
        [Required]
        public int LastStation { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

    }
}
