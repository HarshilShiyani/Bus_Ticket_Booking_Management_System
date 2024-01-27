using Microsoft.AspNetCore.Mvc;

namespace Bus_Ticket_Booking_Management_System.Controllers
{
    public class ErrorController : Controller
    {
        #region ErrorPageNotFound
        public IActionResult ErrorPageNotFound()
        {
            return View();
        }
        #endregion
    }
}
