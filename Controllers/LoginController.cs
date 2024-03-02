using Bus_Ticket_Booking_Management_System.DAL;
using Bus_Ticket_Booking_Management_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

using Bus_Ticket_Booking_Management_System.BAL;
using NuGet.Protocol.Plugins;
using System.Net.Mail;
using System.Net;
using static System.Net.WebRequestMethods;

namespace Bus_Ticket_Booking_Management_System.Controllers
{
    public class LoginController : Controller
    {
        DAL_Login dAL_Login = new DAL_Login();
        DAL_User dAL_User = new DAL_User();

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
            if (error != null)
            {
                TempData["error"] = error;
                TempData["error"] = error;

                return RedirectToAction("LoginPage");
            }
            else
            {
                DataTable dataTable = dAL_Login.CheckLogin(login);
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        HttpContext.Session.SetString("UserID", row["UserID"].ToString());
                        HttpContext.Session.SetString("Username", row["Username"].ToString());
                        HttpContext.Session.SetString("Password", row["Password"].ToString());
                        HttpContext.Session.SetString("Role", row["Role"].ToString());
                        HttpContext.Session.SetString("ImagePath", row["ImagePath"].ToString());
                        HttpContext.Session.SetString("EmailID", row["EmailID"].ToString());
                        HttpContext.Session.SetString("MobileNo", row["MobileNo"].ToString());
                        break;
                    }
                }
                else
                {
                    TempData["error"] = "UserName Or Password Is Invalid";
                }
                if (HttpContext.Session.GetString("Username") != null && HttpContext.Session.GetString("Password") != null)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("LoginPage");
        }
        #endregion

        #region Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("LoginPage", "Login");
        }
        #endregion

        [IsLogged]

        public ActionResult ForgotPasswordPage()
        {

            return View();
        }
        public ActionResult ForgotPasswordAction(Login login)
        {
            string password = dAL_Login.FetchPasswordByUsername(login.Username);
            if (login.Username != null)
            {
                if (password != null)
                {
                    using (MailMessage mm = new MailMessage("harshilshiyani5@gmail.com", login.Username.ToString()))
                    {
                        try
                        {
                            mm.Subject = "Welcome To Bus Admin";
                            mm.Body = "Your username is " + login.Username.ToString() + " and Password is=" + password;
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
                                TempData["ForgotPasswordmeaasge"] = "Password is Sended to your ragistered EmailID";
                            }
                        }
                        catch (SmtpException ex)
                        {
                            Console.WriteLine("Error sending email: " + ex.Message);
                        }
                    }

                }
                else
                {
                    TempData["ForgotPasswordmeaasge"] = "Your user name is invalid Or You are not ragistered Please Contact your Manager";
                }

            }
            return RedirectToAction("ForgotPasswordPage");

        }

        public IActionResult User_RemoveImage()
        {
            dAL_User.User_RemoveImage(Convert.ToInt32(@CV.UserID()));
            TempData["ImageRemoved"] = "Profile Image Removed Succesfully";
            HttpContext.Session.Remove("ImagePath");
            return RedirectToAction("Profile", "Home");
        }

        [HttpPost]
        public IActionResult Update_User_Detail(UserModel userModel)
        {
            
                string filePath = System.IO.Path.GetFullPath(userModel.File.FileName);
                string directoryPath = @"D:\.Net\Bus_Ticket_Booking_Management_System\wwwroot";
                if (!Directory.Exists(directoryPath))
                {
                    //create directory at specified path
                    Directory.CreateDirectory(directoryPath);
                }
                string folderPath = Path.Combine("wwwroot/" + filePath + "/", userModel.File.FileName);
                using (FileStream fs = System.IO.File.Create(folderPath))
                {
                    userModel.File.CopyTo(fs);
                }
                userModel.ImagePath = "/" + filePath + "/" + userModel.File.FileName;

            int id = dAL_Login.PR_UpdateUserDetail(userModel);
            TempData["ProfileUpdated"] = "Profile updated Succesfully";
            HttpContext.Session.SetString("Username", userModel.Username.ToString());
            HttpContext.Session.SetString("Role", userModel.Role.ToString());
            if (userModel.Password != null)
            {
                HttpContext.Session.SetString("ImagePath", userModel.ImagePath.ToString());

            }
            if (userModel.EmailID != null)
            {
                HttpContext.Session.SetString("EmailID", userModel.EmailID.ToString());

            }
            if (userModel.MobileNo != null)
            {
                HttpContext.Session.SetString("MobileNo", userModel.MobileNo.ToString());

            }
            return RedirectToAction("Profile", "Home");


        }
    }

}

