using System.ComponentModel.DataAnnotations;

namespace Bus_Ticket_Booking_Management_System.Areas.Routes.Models
{
    public class RouteStationModel
    {
        [Required]
        public int RouteDetailID { get; set; }
        [Required]
        public int RouteID { get; set; }
        [Required]
        public int RouteStationId { get; set; }
        [Required]
        public int StationOrder { get; set; }
        [Required]
        public DateTime StationTime { get; set; }
        [Required]
            
        public double DistanceFromOrigin { get; set; }

    }
}
