﻿using Bus_Ticket_Booking_Management_System.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Bus_Ticket_Booking_Management_System.Areas.Station.Models;
using static System.Collections.Specialized.BitVector32;

namespace Bus_Ticket_Booking_Management_System.Areas.Station.Controllers
{
    [Area("Station")]
    [Route("Station/[controller]/[action]")]
    public class StationController : Controller
    {
        DAL_Station station = new DAL_Station();
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
        public ActionResult StationAddEdit(int? StationID)
        {
            if(StationID != null)
            {
                Stationmodel model = station.SelectStationByID(StationID);
                return View(model);
            }
            else
            {
                return View();
            }
            
        }
        public ActionResult StationDelete(int StationID)
        {
            DAL_Station dAL_Station=new DAL_Station();
            dAL_Station.DeleteStationByID(StationID);
            return RedirectToAction("StationList");
        }
        public ActionResult StationSave(Stationmodel stationmodel,int? StationID)
        {
            if(StationID != null)
            {
                station.StationAddEdit(stationmodel,StationID);
                ViewData["IsStationAdded"] = "Station Edited Succesfully";

            }
            else
            {
                station.StationAddEdit(stationmodel, StationID);
                ViewData["IsStationAdded"] = "Station Added Succesfully";
            }
            return View("StationAddEdit");
        }
    }
}
