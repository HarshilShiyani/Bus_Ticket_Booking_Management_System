using System.ComponentModel.DataAnnotations;

namespace Bus_Ticket_Booking_Management_System.Models
{
    public class Login
    {
        public int UserID { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
        public string Role { get; set; }

    }
}
