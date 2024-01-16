using System.ComponentModel.DataAnnotations;

namespace Bus_Ticket_Booking_Management_System.Areas.Station.Models
{
    public class Stationmodel
    {
        public int StationID { get; set; }
        [Required(ErrorMessage ="Please Enter Station Name")]
        public string? StationName { get; set; }
        [Required(ErrorMessage = "Please Enter Station Location")]
        public string? Location { get; set; }
        [Required(ErrorMessage = "Please Enter Station Description")]
        public string? Description { get; set; }
    }

    public class StationDDL
    {
        public int Stid { get; set; }

        public string? StationName { get; set; }

    }

}