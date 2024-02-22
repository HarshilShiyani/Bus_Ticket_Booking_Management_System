using Bus_Ticket_Booking_Management_System.Areas.Seat.Models;
using Bus_Ticket_Booking_Management_System.DAL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Text;
using SelectPdf;
using Bus_Ticket_Booking_Management_System.BAL;
using Bus_Ticket_Booking_Management_System.Models;
using System.Net.Mail;
using System.Net;
using Bus_Ticket_Booking_Management_System.Areas.Routes.Models;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;

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

                using (MailMessage mm = new MailMessage("harshilshiyani5@gmail.com", row["EmailID"].ToString()))
                {
                    try
                    {
                        
                        mm.Subject = "Hooray! Your tour booking is confirmed.";
                        string htmlBody = $@"
<div class='modal modal-lg' id='staticBackdrop' data-bs-backdrop='static' data-bs-keyboard='false' tabindex='-1' aria-labelledby='staticBackdropLabel'>
    <div class=""modal-dialog"">
            <div class=""modal-content"">
                <div class=""modal-header bg-success text-light"">
                    <h1 class=""modal-title fs-5"" id=""staticBackdropLabel""><i class=""bi bi-check-circle-fill"" id=""message""></i> </h1>
                    <button type=""button"" class=""btn-close btn-close-white"" data-bs-dismiss=""modal"" aria-label=""Close""></button>
                </div>
                <div class=""modal-body"">
                    <div class=""row"">
                        <div class=""col-md-6"">
                            <p><i class=""bi bi-card-text""></i> <strong>Ticket ID:</strong> <span id=""ticketId"">{row["TicketID"].ToString()}</span></p>
                            <p><i class=""bi bi-geo-alt""></i> <strong>Route Code:</strong> <span id=""routename"">{row["RouteName"].ToString()}</span></p>

                            <p><i class=""bi bi-person""></i> <strong>Passenger Name:</strong> <span id=""psgname"">{row["PassengerName"].ToString()}</span></p>
                            <p><i class=""bi bi-envelope""></i> <strong>Email ID:</strong> <span id=""email"">{row["EmailID"].ToString()}</span></p>
                            <p><i class=""bi bi-telephone""></i> <strong>Mobile No:</strong> <span id=""mobileNo"">{row["MobiliNo"].ToString()}</span></p>
                            <p><i class=""bi bi-geo-alt""></i> <strong>Source:</strong> <span id=""source"">{row["Source"].ToString()}</span></p>
                            <p><i class=""bi bi-geo-alt""></i> <strong>Destination:</strong> <span id=""destination"">{row["Destination"].ToString()}</span></p>
                        </div>
                        <div class=""col-md-6"">
                            <p><i class=""bi bi-calendar3""></i> <strong>Departure Date:</strong> <span id=""departureDate"">{row["onDate"].ToString()}</span></p>
                            <p><i class=""bi bi-clock""></i> <strong>Departure Time:</strong> <span id=""departureTime"">{row["DepartureTime"].ToString()}</span></p>
                            <p><i class=""bi bi-clock""></i> <strong>Arrival Time:</strong> <span id=""arrivalTime"">{row["Arrivaltime"].ToString()}</span></p>
                            <p><i class=""bi bi-patch-check""></i> <strong>Booked Seat:</strong> <span id=""bookedSeat"">{row["BookedSeat"].ToString()}</span></p>
                            <p><i class=""bi bi-currency-dollar""></i> <strong>Fare:</strong> <span id=""fare"">{row["fare"].ToString()}</span></p>
                            <p><i class=""bi bi-calendar2-check""></i> <strong>Transaction Date:</strong> <span id=""transactionDate"">{row["Transactiondate"].ToString()}</span></p>
                        </div>
                    </div>
                </div>
                <div class=""modal-footer"">
                </div>
            </div>
        </div>
</div>";
                        mm.Body = htmlBody;
                        mm.IsBodyHtml = true;

                        using (SmtpClient smtp = new SmtpClient())
                        {
                            smtp.Host = "smtp.gmail.com";
                            smtp.EnableSsl = true;
                            NetworkCredential NetworkCred = new NetworkCredential("harshilshiyani5@gmail.com", "rxoqekpraeztcncr");
                            smtp.UseDefaultCredentials = false;
                            smtp.Credentials = NetworkCred;
                            smtp.Port = 587;
                            smtp.Send(mm);
                        }
                    }
                    catch (SmtpException ex)
                    {
                        Console.WriteLine("Error sending email: " + ex.Message);
                    }
                }


                // Return a success response with an appropriate message or data
                return Json(new
                {
                    success = true,
                    message = " Success! Your Ticket Confirmed.",
                    ticketId = row["TicketID"].ToString(),
                    psgname = row["PassengerName"].ToString(),
                    emailID = row["EmailID"].ToString(),
                    mobiliNo = row["MobiliNo"].ToString(),
                    routeName = row["RouteName"].ToString(),

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
