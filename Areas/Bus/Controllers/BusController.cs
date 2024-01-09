using Bus_Ticket_Booking_Management_System.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;


namespace Bus_Ticket_Booking_Management_System.Areas.Bus.Controllers
{
    [Area("Bus")]
    [Route("Bus/[controller]/[action]")]
    public class BusController : Controller
    {
        #region BusTypeList
        public IActionResult BusTypeList()
        {
            DAL_Buses dAL_Buses = new DAL_Buses();
            return View("BusTypeList",dAL_Buses.BusTypeList());
        }
        #endregion

        #region BusList
        public IActionResult BusList()
        {
            DAL_Buses dAL_Buses = new DAL_Buses();
            return View("BusList", dAL_Buses.BusList());
        }
        #endregion
    }
}
