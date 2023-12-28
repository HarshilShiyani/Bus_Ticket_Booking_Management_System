using Bus_Ticket_Booking_Management_System.DAL;
using Bus_Ticket_Booking_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Bus_Ticket_Booking_Management_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            DAL_Count dAL_Count = new DAL_Count();
            ViewBag.BusCount = dAL_Count.GetBusCount();
            ViewBag.StationCount = dAL_Count.GetStationCount();
            ViewBag.RouteCount = dAL_Count.GetRouteCount();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}