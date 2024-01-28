using System.ComponentModel.DataAnnotations;
namespace Bus_Ticket_Booking_Management_System.Models
{
    public class ChangePassword
    {
        [Required]
        public string currentPassword { get; set; }
        [Required]
        public string newPassword { get; set; }
        [Required]
        [Compare("newPassword", ErrorMessage = "Passwords do not match")]
        public string renewPassword { get; set;}
           
    }
}
