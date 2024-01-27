using Bus_Ticket_Booking_Management_System.Areas.Ticket.Models;
using Bus_Ticket_Booking_Management_System.BAL;
using Bus_Ticket_Booking_Management_System.DAL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Bus_Ticket_Booking_Management_System.Areas.Ticket.Controllers
{
    [CheckAccess]
    [Area("Ticket")]
    [Route("Ticket/[controller]/[action]")]
    public class TicketController : Controller
    {
        #region SearchTicket
        public IActionResult SearchTicket()
        {
            DAL_Station dAL_Station = new DAL_Station();
            ViewBag.Station_DDL = dAL_Station.Station_DDL();
            return View("SearchTicket");
        }
        #endregion
        
        #region DisplaySerchedTicket
        public JsonResult DisplaySerchedTicket(TicketSearchmodel ticketSearchModel)
        {
            DAL_Ticket dAL_Ticket = new DAL_Ticket();
            List<DiaplaySerchedRouteDetail> diaplaySerchedRouteDetail = dAL_Ticket.SerchTicket(ticketSearchModel);
            return Json(JsonConvert.SerializeObject(diaplaySerchedRouteDetail));
        }

        #endregion

        #region  RenderTicketPartial
        public IActionResult RenderTicketPartial(string ticket)
        {
            var ticketModel = JsonConvert.DeserializeObject<DiaplaySerchedRouteDetail>(ticket);
            return PartialView("_SearchedTicket", ticketModel);
        }
        #endregion

        public IActionResult SeatSelection(int routeId, int seat)
        {
            return View("SeatSelection", seat);
        }
    }
}
