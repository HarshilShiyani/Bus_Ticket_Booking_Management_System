using System.ComponentModel.DataAnnotations;

namespace Bus_Ticket_Booking_Management_System.Areas.Bus.Models
{
    public class BusTypemodel
    {
        public int? BusTypeID { get; set; }

        [Required(ErrorMessage ="Please Enter BusType")]
        public string TypeName { get; set; }

        [Required(ErrorMessage = "Please Enter Capacity Of Bus")]
        [Range(1, 100, ErrorMessage = "Please Enter Total Seat")]
        public int Capacity { get; set; }

        public string? Description { get; set; }
    }
}
