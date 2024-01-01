using Bus_Ticket_Booking_Management_System.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Bus_Ticket_Booking_Management_System.Areas.Routes.Controllers
{

    [Area("Routes")]
    [Route("Routes/[controller]/[action]")]
    public class RouteController : Controller
    {
        public IActionResult RouteList()
        {
            DAL_Route dAL_Route = new DAL_Route();
            DataTable dt = dAL_Route.GelAllRoute();
            return View("RouteList",dt);
        }
    }
}
