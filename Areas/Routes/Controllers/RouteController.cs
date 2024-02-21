using Bus_Ticket_Booking_Management_System.Areas.Routes.Models;
using Bus_Ticket_Booking_Management_System.Areas.Station.Models;
using Bus_Ticket_Booking_Management_System.BAL;
using Bus_Ticket_Booking_Management_System.DAL;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System;
using System.Data;

namespace Bus_Ticket_Booking_Management_System.Areas.Routes.Controllers
{
    [CheckAccess]
    [Area("Routes")]
    [Route("Routes/[controller]/[action]")]
    public class RouteController : Controller
    {
        DAL_Station dAL_Station = new DAL_Station();
        DAL_Route dAL_Route = new DAL_Route();
        DAL_Buses dAL_Buses = new DAL_Buses();

        #region RouteList
        public IActionResult RouteList()
        {
            DAL_Route dAL_Route = new DAL_Route();
            DataTable dt = dAL_Route.GelAllRoute();
            return View("RouteList", dt);
        }
        #endregion

        #region RouteAddEdit
        public IActionResult RouteAddEdit(int? RouteID)
        {
            ViewBag.Station_DDL = dAL_Station.Station_DDL();
            ViewBag.Buses_DDL = dAL_Buses.Buses_DDL();
            if (RouteID != 0)
            {
                Routemodel routemodel = dAL_Route.SelectRoute_By_RouteId(RouteID);
                return View(routemodel);
            }
            else
            {
                return View();
            }
        }
        #endregion

        #region RouteSave
        public IActionResult RouteSave(Routemodel routemodel, int? RouteID)
        {
            if (RouteID == 0)
            {
                if (Convert.ToBoolean(dAL_Route.RouteAddEdit(routemodel, RouteID)))
                {
                    TempData["IsRouteAdded"] = "New Route Added Succesfully";
                }

            }
            else
            {
                if (Convert.ToBoolean(dAL_Route.RouteAddEdit(routemodel, RouteID)))
                {
                    TempData["IsRouteAdded"] = "Route Edited Succesfully";
                }
            }
            return RedirectToAction("RouteAddEdit");
        }
        #endregion

        #region SelectRoute_By_RouteID for dispaly
        public IActionResult SelectRoute_By_RouteID(int RouteID)
        {
            ViewBag.Station_DDL = dAL_Station.Station_DDL();

            TempData["RouteID"] = RouteID;
            DataTable dataTable = dAL_Route.DetailRouteByRouteID(RouteID);
            DataTable dataTable1 = dAL_Route.StationDetailOfRoute(RouteID);
            Bus_Ticket_Booking_Management_System.Areas.Routes.Models.RouteStationModel stationmodel = new Bus_Ticket_Booking_Management_System.Areas.Routes.Models.RouteStationModel();
            var model = new Tuple<DataTable, DataTable, Bus_Ticket_Booking_Management_System.Areas.Routes.Models.RouteStationModel>(dataTable, dataTable1, stationmodel);
            return View("DetailRoute", model);
        }
        #endregion

        #region RouteAddEdtRouteStation
        public IActionResult RouteAddEditRouteStation(int routeID, int? routeDetailID)
        {
            ViewBag.Station_DDL = dAL_Station.Station_DDL();

            return View();
        }
        #endregion

        #region SaveRouteStation
        public IActionResult SaveRouteStation(RouteStationModel formData)
        {

            dAL_Route.RouteAddEdtRouteStation(formData);
            return RedirectToAction("SelectRoute_By_RouteID");

        }
        #endregion

        #region Delete_RouteStation
        public IActionResult Delete_RouteStation(int RouteDetailID)
        {
            dAL_Route.Delete_RouteStation(RouteDetailID);
            return RedirectToAction("SelectRoute_By_RouteID");
        }
        #endregion

        #region StationEditModelFillDataOfRoute
        public IActionResult StationEditModelFillDataOfRoute(int routeID, int routeDetailID)
        {
            RouteStationModel jdata = dAL_Route.StationEditModelFillDataOfRoute(routeDetailID);
            return Json(jdata);
        }
        #endregion

        public IActionResult ExportRouteDetailToExcel()
        {

            DataTable dataTable = dAL_Route.GelAllRoute();
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Students");
                // Add headers
                worksheet.Cell(1, 1).Value = "Route Name";
                worksheet.Cell(1, 2).Value = "Time  ";
                worksheet.Cell(1, 3).Value = "Source";
                worksheet.Cell(1, 4).Value = "Destination";
                worksheet.Cell(1, 5).Value = "Starting Date";
                worksheet.Cell(1, 6).Value = "Valid Till";
                worksheet.Cell(1, 7).Value = "Bus Number";
                worksheet.Cell(1, 8).Value = "Bus Type";
                var headerRow = worksheet.Row(1);
                headerRow.Style.Font.Bold = true;

                // Add data
                int row = 2;
                foreach (DataRow dr in dataTable.Rows)
                {
                    worksheet.Cell(row, 1).Value = dr["RouteName"].ToString();
                    worksheet.Cell(row, 2).Value = dr["starting_time"].ToString();
                    worksheet.Cell(row, 3).Value = dr["Source"].ToString();
                    worksheet.Cell(row, 4).Value = dr["Destination"].ToString();
                    DateTime startingDate = (DateTime)dr["Starting_date"];

                    worksheet.Cell(row, 5).Value = startingDate.ToString("yyyy-MM-dd");
                    DateTime endingdate = (DateTime)dr["Ending_date"];

                    worksheet.Cell(row, 6).Value = endingdate.ToString("yyyy-MM-dd");
                    worksheet.Cell(row, 7).Value = dr["RegistrationNumber"].ToString();
                    worksheet.Cell(row, 8).Value = dr["TypeName"].ToString();
                    // Add other properties...
                    row++;
                }
                // Set content type and filename
                var contentType = "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet";
                var fileName = "RouteList.xlsx";
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, contentType, fileName);
                }
            }
        }

        public IActionResult ExportRouteDetailToPdf()
        {
            DataTable dataTable = dAL_Route.GelAllRoute();

            using (var document = new PdfDocument())
            {
                var page = document.AddPage();
                var gfx = XGraphics.FromPdfPage(page);
                var font = new XFont("Arial", 10, XFontStyle.Regular);

                // Define column widths and heights
                const int cellPadding = 5;
                double[] columnWidths = { 100, 70, 60, 70, 80, 80, 80, 80 };
                const int rowHeight = 20;

                // Draw headers
                int currentX = cellPadding;
                for (int i = 0; i < columnWidths.Length; i++)
                {
                    gfx.DrawString(GetHeaderText(i), font, XBrushes.Black, new XRect(currentX, cellPadding, columnWidths[i], rowHeight), XStringFormats.TopLeft);
                    currentX += (int)columnWidths[i];
                }

                // Draw data rows
                int currentY = rowHeight + cellPadding * 2;
             
                foreach (DataRow dr in dataTable.Rows)
                {
                    currentX = cellPadding;
                    for (int i = 0; i < columnWidths.Length; i++)
                    {
                        string data = dr[GetDataColumnName(i)].ToString();
                        if (i == 4 || i == 5) // Check if it's the Starting Date or Valid Till column
                        {
                            // Parse the date string to DateTime
                            if (DateTime.TryParse(data, out DateTime date))
                            {
                                // Format the date to display only the date part without the time
                                data = date.ToString("yyyy-MM-dd");
                            }
                        }
                        gfx.DrawString(data, font, XBrushes.Black, new XRect(currentX, currentY, columnWidths[i], rowHeight), XStringFormats.TopLeft);
                        currentX += (int)columnWidths[i];
                    }

                    // Add download time to footer
                    var downloadTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    gfx.DrawString($"Download Time: {downloadTime}", font, XBrushes.Black, new XRect(cellPadding, page.Height - 20, page.Width, rowHeight), XStringFormats.BottomLeft);

                    currentY += rowHeight;
                }

                // Set content type and filename
                var contentType = "application/pdf";
                var fileName = "RouteList.pdf";

                // Save the PDF to a memory stream
                using (var memoryStream = new MemoryStream())
                {
                    document.Save(memoryStream);
                    memoryStream.Position = 0;

                    // Return the PDF file
                    return File(memoryStream.ToArray(), contentType, fileName);
                }
            }
        }

        // Helper function to get header text for a specific column
        private string GetHeaderText(int columnIndex)
        {
            switch (columnIndex)
            {
                case 0: return "Route Name";
                case 1: return "Time";
                case 2: return "Source";
                case 3: return "Destination";
                case 4: return "Starting Date";
                case 5: return "Valid Till";
                case 6: return "Bus Number";
                case 7: return "Bus Type";
                default: return "";
            }
        }

        // Helper function to get the corresponding data column name for a specific header
        private string GetDataColumnName(int columnIndex)
        {
            switch (columnIndex)
            {
                case 0: return "RouteName";
                case 1: return "starting_time";
                case 2: return "Source";
                case 3: return "Destination";
                case 4: return "Starting_date";
                case 5: return "Ending_date";
                case 6: return "RegistrationNumber";
                case 7: return "TypeName";
                default: return "";
            }
        }
    }
}
