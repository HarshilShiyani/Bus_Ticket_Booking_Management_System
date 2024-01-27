using Bus_Ticket_Booking_Management_System.DAL;
using Bus_Ticket_Booking_Management_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

using Bus_Ticket_Booking_Management_System.BAL;
namespace Bus_Ticket_Booking_Management_System.Controllers
{
    public class LoginController : Controller
    {
        DAL_Login dAL_Login = new DAL_Login();

        [IsLogged]
        #region LoginPage
        public IActionResult LoginPage()
        {
            return View();
        }
        #endregion

        #region CheckLogin
        public IActionResult CheckLogin(Login login)
        {
            string error = null;

            if (login.Username == null)
            {
                error += "<i class=\"bi bi-x-circle\"></i> Username is Required";
            }
            if (login.Password == null)
            {
                error += " <br /> <i class=\"bi bi-x-circle\"></i> Password is Required";
            }
            if(error != null )
            {
                TempData["error"] = error;
                TempData["error"] = error;

                return RedirectToAction("LoginPage");
            }
            else
            {
                DataTable dataTable = dAL_Login.CheckLogin(login);
                if (dataTable.Rows.Count>0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        HttpContext.Session.SetString("UserID", row["UserID"].ToString());
                        HttpContext.Session.SetString("Username", row["Username"].ToString());
                        HttpContext.Session.SetString("Password", row["Password"].ToString());
                        HttpContext.Session.SetString("Role", row["Password"].ToString());

                        break;
                    }
                }
                else
                {
                    TempData["error"] = "UserName Or Password Is Invalid";
                }
                if(HttpContext.Session.GetString("Username") != null && HttpContext.Session.GetString("Password") != null)
                {
                    return RedirectToAction("Index","Home");
                }
            }
            return RedirectToAction("LoginPage");
        }
        #endregion

        #region Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("LoginPage","Login");
        }
        #endregion
    }
}
