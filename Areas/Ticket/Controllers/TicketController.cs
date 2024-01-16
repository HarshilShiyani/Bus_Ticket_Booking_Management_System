using Bus_Ticket_Booking_Management_System.Areas.Ticket.Models;
using Bus_Ticket_Booking_Management_System.DAL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Bus_Ticket_Booking_Management_System.Areas.Ticket.Controllers
{
    [Area("Ticket")]
    [Route("Ticket/[controller]/[action]")]
    public class TicketController : Controller
    {
        public IActionResult SearchTicket()
        {
            DAL_Station dAL_Station = new DAL_Station();
            ViewBag.Station_DDL = dAL_Station.Station_DDL();
            return View("SearchTicket");
        }
        public IActionResult SeatSelection(int routeId) 
        { 
            return View("SeatSelection"); 
        }        
        public JsonResult DisplaySerchedTicket(TicketSearchmodel ticketSearchModel)
        {
            DAL_Ticket dAL_Ticket = new DAL_Ticket();
            List<DiaplaySerchedRouteDetail> diaplaySerchedRouteDetail = dAL_Ticket.SerchTicket(ticketSearchModel);
            Console.WriteLine(diaplaySerchedRouteDetail);
            return Json(JsonConvert.SerializeObject( diaplaySerchedRouteDetail));
        }
         public IActionResult RenderTicketPartial(string ticket)
         {
                var ticketModel = JsonConvert.DeserializeObject<DiaplaySerchedRouteDetail>(ticket);
                return PartialView("_SearchedTicket", ticketModel);
          }
    }
}
