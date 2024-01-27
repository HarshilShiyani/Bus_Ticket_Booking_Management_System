using Bus_Ticket_Booking_Management_System.Areas.Routes.Models;
using Bus_Ticket_Booking_Management_System.Areas.Station.Models;
using Bus_Ticket_Booking_Management_System.BAL;
using Bus_Ticket_Booking_Management_System.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
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
            var model=new Tuple<DataTable, DataTable, Bus_Ticket_Booking_Management_System.Areas.Routes.Models.RouteStationModel>(dataTable, dataTable1, stationmodel);
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
            RouteStationModel  jdata= dAL_Route.StationEditModelFillDataOfRoute(routeDetailID);
            return Json(jdata);
        }
        #endregion
    }
}
