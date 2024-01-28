using Bus_Ticket_Booking_Management_System.DAL;
using Bus_Ticket_Booking_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Bus_Ticket_Booking_Management_System.BAL;
using NuGet.Protocol.Plugins;
using System.Data;
using System.Net.Mail;
using System.Net;

namespace Bus_Ticket_Booking_Management_System.Controllers
{
    [CheckAccess]
    public class HomeController : Controller
    {
        DAL_Login dAL_Login = new DAL_Login();
        #region Index(DashBoard Page)
        public IActionResult Index()
        {
            DAL_Count dAL_Count = new DAL_Count();
            ViewBag.BusCount = dAL_Count.GetBusCount();
            ViewBag.StationCount = dAL_Count.GetStationCount();
            ViewBag.RouteCount = dAL_Count.GetRouteCount();
            return View();
        }
        #endregion

        #region Contact
        public IActionResult Contact()
        {
            return View();
        }
        #endregion

        #region FAQ
        public IActionResult FAQ()
        {
            return View();
        }
        #endregion

        #region Profile
        public IActionResult Profile()
        {
            return View();
        }
        #endregion

        [CheckAccess]
        public IActionResult ChangePassword(ChangePassword changePassword)
        {
            if (ModelState.IsValid)
            {
                DataTable dataTable = dAL_Login.CheckPasswordAtChangePassword(changePassword.currentPassword);
                if (dataTable.Rows.Count > 0)
                {
                    if (dAL_Login.ChangePasswordByCurrentPassword(changePassword.newPassword))
                    {
                        TempData["ChangePassWordsuccess"] = "Password Changed Succesfully";
                        using (MailMessage mm = new MailMessage("harshilshiyani5@gmail.com", @CV.EmailID().ToString()))
                        {
                            try
                            {
                                mm.Subject = "Password Changed Succesfully";
                                mm.Body = "Your Password Changed Succesfully, Your Username is "+ @CV.EmailID().ToString()+" OR "+@CV.username().ToString()+" And Password is="+changePassword.newPassword;
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
                        return RedirectToAction("Profile", "Home");
                    }
                }
                else
                {
                    TempData["ChangePassWordserror"] = "Current Password Is Wrong";
                    return RedirectToAction("Profile", "Home");
                }
            }
            return RedirectToAction("Profile");
        }

    }
}