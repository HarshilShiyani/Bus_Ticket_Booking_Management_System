using Microsoft.AspNetCore.Mvc;

namespace Bus_Ticket_Booking_Management_System.Areas.Routes.Controllers
{

    [Area("Routes")]
    [Route("Routes/[controller]/[action]")]
    public class RouteController : Controller
    {
        public IActionResult RouteList()
        {
            return View();
        }
    }
}
