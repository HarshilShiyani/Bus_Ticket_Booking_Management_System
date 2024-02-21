using Bus_Ticket_Booking_Management_System.Areas.Seat.Models;
using Bus_Ticket_Booking_Management_System.DAL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Text;
using SelectPdf;

namespace Bus_Ticket_Booking_Management_System.Areas.Seat.Controllers
{
    public class SeatController : Controller
    {
        DAL_Seat dAL_Seat = new DAL_Seat();
        DAL_Ticket dAL_Ticket = new DAL_Ticket();

        [Area("Seat")]
        [Route("Seat/[controller]/[action]")]

        public IActionResult SeatLayout(string routeId, string onDate)
        {

            string decodedRouteId;
            string decodedonDate;
            byte[] data = Convert.FromBase64String(routeId);
            decodedRouteId = Encoding.UTF8.GetString(data).Trim('"');

            byte[] data1 = Convert.FromBase64String(onDate);
            decodedonDate = Encoding.UTF8.GetString(data1).Trim('"');

            object count = dAL_Seat.CheckSeatListIsInserted(int.Parse(decodedRouteId), DateTime.Parse(decodedonDate));
            if (Convert.ToInt32(count) == 0)
            {
                object noofseat = dAL_Seat.no_Of_Seat_In_Bus(int.Parse(decodedRouteId));
                for (int i = 1; i <= Convert.ToInt32(noofseat); i++)
                {
                    dAL_Seat.Insert_Seat_Detail_RouteWiseDateWiseSeat(int.Parse(decodedRouteId), DateTime.Parse(decodedonDate), i);
                }

            }
            List<Seatmodel> seatlayout = new List<Seatmodel>();
            if (int.Parse(decodedRouteId) != 0)
            {
                seatlayout = dAL_Seat.SeatLayout(int.Parse(decodedRouteId), DateTime.Parse(decodedonDate));
            }
            return View(seatlayout);


        }

        public IActionResult UpdateSlectedSeatStatus(List<int> seatIDs)
        {
            foreach (int tempid in seatIDs)
            {
                dAL_Seat.UpdateSlectedSeatStatus(tempid);
            }
            return RedirectToAction("SeatLayout");
        }

        public IActionResult RenderPassengerDetailsForm(string selectedSeats)
        {
            List<Seatmodel> seatmodel = JsonConvert.DeserializeObject<List<Seatmodel>>(selectedSeats);
            return PartialView("_PassengerDetailsForm", seatmodel);
        }

        [HttpPost]
        public IActionResult submitPassengerDetails(PassengerDetails passengerDetails)
        {
            byte[] routeIdData = Convert.FromBase64String(passengerDetails.routeId);
            string decodedRouteId = Encoding.UTF8.GetString(routeIdData).Trim('"');

            // Decode onDate
            byte[] onDateData = Convert.FromBase64String(passengerDetails.onDate);
            string decodedOnDate = Encoding.UTF8.GetString(onDateData).Trim('"');

            byte[] sourceID = Convert.FromBase64String(passengerDetails.sourceID);
            string decodedsourceID = Encoding.UTF8.GetString(sourceID).Trim('"');

            byte[] destinationID = Convert.FromBase64String(passengerDetails.destinationID);
            string decodeddestinationID = Encoding.UTF8.GetString(destinationID).Trim('"');

            byte[] fare = Convert.FromBase64String(passengerDetails.fare);
            string decodedfare = Encoding.UTF8.GetString(fare).Trim('"');



            // Update passengerDetails with decoded values
            passengerDetails.routeId = decodedRouteId;
            passengerDetails.onDate = decodedOnDate;
            passengerDetails.sourceID = decodedsourceID;
            passengerDetails.destinationID = decodeddestinationID;
            passengerDetails.fare = decodedfare;

            int tempticketId = dAL_Ticket.InsertTicketDetailwithPassengerInfo(passengerDetails);
            if (tempticketId > 0)
            {
                DataTable dataTable = dAL_Ticket.PR_SelectTicketByTicketID(tempticketId);
                DataRow row = dataTable.Rows[0];

                // Return a success response with an appropriate message or data
                return Json(new
                {
                    success = true,
                    message = " Success! Your Ticket Confirmed.",
                    ticketId = row["TicketID"].ToString(),
                    psgname = row["PassengerName"].ToString(),
                    emailID = row["EmailID"].ToString(),
                    mobiliNo = row["MobiliNo"].ToString(),
                    routeName= row["RouteName"].ToString(),

                    onDate = row["onDate"].ToString(),
                    bookedSeat = row["BookedSeat"].ToString(),
                    transactiondate = row["Transactiondate"].ToString(),
                    fare = row["fare"].ToString(),
                    departureTime = row["DepartureTime"].ToString(),

                    arrivaltime = row["Arrivaltime"].ToString(),
                    source = row["Source"].ToString(),
                    destination = row["Destination"].ToString(),


                }
                );
            }
            else
            {
                // Return an error response if insertion fails
                return Json(new { success = false, message = "Failed to submit passenger details." });
            }
        }

        public IActionResult GeneratePDF(string html)
        {
            html = html.Replace("StrTag", "<").Replace("EndTag", ">");
            HtmlToPdf htmlToPdf = new HtmlToPdf();
            PdfDocument pdfDocument = htmlToPdf.ConvertHtmlString(html);
            byte[] pdf = pdfDocument.Save();
            pdfDocument.Close();
            return File(
                    pdf,
                    "application/pdf",
                    "dshdkshdk.pdf"
                );

        }
    }
}
