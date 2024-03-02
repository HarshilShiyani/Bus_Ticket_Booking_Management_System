using System.ComponentModel.DataAnnotations;

namespace Bus_Ticket_Booking_Management_System.Models
{
    public class UserModel
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string EmailID { get; set; }
        public string ImagePath { get; set; }
        public string MobileNo { get; set; }
        public IFormFile File { get; set; }

    }
}
