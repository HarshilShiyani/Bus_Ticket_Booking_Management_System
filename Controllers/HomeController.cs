using Bus_Ticket_Booking_Management_System.DAL;
using Bus_Ticket_Booking_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Bus_Ticket_Booking_Management_System.BAL;

namespace Bus_Ticket_Booking_Management_System.Controllers
{
    [CheckAccess]
    public class HomeController : Controller
    {
        #region Index(DashBoard Page)
        public IActionResult Index()
        {
            DAL_Count dAL_Count = new DAL_Count();
            ViewBag.BusCount = dAL_Count.GetBusCount();
            ViewBag.StationCount = dAL_Count.GetStationCount();
            ViewBag.RouteCount = dAL_Count.GetRouteCount();
            return View();
        }
        #endregion

        #region Contact
        public IActionResult Contact()
        {
            return View();
        }
        #endregion

        #region FAQ
        public IActionResult FAQ()
        {
            return View();
        }
        #endregion

        #region Profile
        public IActionResult Profile()
        {
            return View();
        }
        #endregion

    }
}