using Bus_Ticket_Booking_Management_System.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;
using System.Drawing.Printing;


namespace Bus_Ticket_Booking_Management_System.Areas.Station.Controllers
{
    [Area("Station")]
    [Route("Station/[controller]/[action]")]
    public class StationController : Controller
    {
        public static string ConnString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build()
            .GetConnectionString("connectionString");
        public ActionResult StationList(int page = 1)
        {
            DAL_Station dAL_Station = new DAL_Station();
            int totalRows = dAL_Station.GetTotalRowCount();
            int totalPages = (int)Math.Ceiling((double)totalRows / 10);
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            DataTable dt = dAL_Station.PR_AllStationList(page);
            return View("StationList", dt);
        }

    }
}


