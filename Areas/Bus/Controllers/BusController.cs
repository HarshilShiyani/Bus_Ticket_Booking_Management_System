using Bus_Ticket_Booking_Management_System.Areas.Bus.Models;
using Bus_Ticket_Booking_Management_System.DAL;
using Microsoft.AspNetCore.Mvc;

namespace Bus_Ticket_Booking_Management_System.Areas.Bus.Controllers
{
    [Area("Bus")]
    [Route("Bus/[controller]/[action]")]
    public class BusController : Controller
    {
        DAL_Buses dAL_Buses = new DAL_Buses();

        #region BusTypeList
        public IActionResult BusTypeList()
        {
            DAL_Buses dAL_Buses = new DAL_Buses();
            return View("BusTypeList", dAL_Buses.BusTypeList());
        }
        #endregion

        #region BusTypeAddEdit
        public ActionResult BusTypeAddEdit(int BusTypeID)
        {
            DAL_Buses dAL_Buses = new DAL_Buses();
            if (BusTypeID != null)
            {
                BusTypemodel busTypemodel = dAL_Buses.SelectBusTypeByBusTypeId(BusTypeID);
                return View(busTypemodel);
            }
            else
            {
                return View();
            }
        }

        #endregion

        #region BusTypeSave
        public ActionResult BusTypeSave(BusTypemodel busTypemodel, int? BusTypeID)
        {
            if (BusTypeID != 0)
            {
                dAL_Buses.BusTypeAddEdit(busTypemodel, BusTypeID);
                ViewData["IsBusTypeAdded"] = "BusType Edited Succesfully";

            }
            else
            {
                dAL_Buses.BusTypeAddEdit(busTypemodel, BusTypeID);
                ViewData["IsBusTypeAdded"] = "BusType Added Succesfully";
            }
            return View("BusTypeAddEdit");
        }
        #endregion

        #region BusTypeDelete
        public ActionResult BusTypeDelete(int BusTypeByID)
        {
            DAL_Buses dAL_Buses=new DAL_Buses();
            dAL_Buses.DeleteBusTypeByID(BusTypeByID);
            return RedirectToAction("BusTypeList");
        }
        #endregion

        #region BusList
        public IActionResult BusList()
        {
            DAL_Buses dAL_Buses = new DAL_Buses();
            return View("BusList", dAL_Buses.BusList());
        }
        #endregion

        #region BusAddEdit
        public IActionResult BusAddEdit(int? BusID)
        {
            ViewBag.BusTypeDDL = dAL_Buses.BusTypeDDL();
            if (BusID != 0)
            {
                Busmodel busmodel= dAL_Buses.SelectBusByBusId(BusID);
                return View(busmodel);
            }
            else
            {
                return View();
            }
            
        }
        #endregion

        #region BusSave
        public ActionResult BusSave(Busmodel busmodel, int? BusID)
        {
            if (BusID != 0)
            {
                dAL_Buses.BusAddEdit(busmodel, BusID);
                TempData["IsBusAdded"] = "Bus Edited Succesfully";

            }
            else
            {
                dAL_Buses.BusAddEdit(busmodel, BusID);
                TempData["IsBusAdded"] = "Bus Added Succesfully";
            }
            return RedirectToAction("BusAddEdit");
        }
        #endregion

        #region BusDelete
        public ActionResult BusDelete(int BusID)
        {
            DAL_Buses dAL_Buses = new DAL_Buses();
            dAL_Buses.DeleteBusByID(BusID);
            return RedirectToAction("BusList");
        }
        #endregion
    }
}
