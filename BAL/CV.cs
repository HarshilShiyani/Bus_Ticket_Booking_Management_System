using Microsoft.AspNetCore.Http;
namespace Bus_Ticket_Booking_Management_System.BAL
{
    public static class CV
    {
        private static IHttpContextAccessor _contextAccessor;
        static CV()
        {
            _contextAccessor = new HttpContextAccessor();
        }

        #region Username
        public static string? username()
        {
            string username = "";
            if (_contextAccessor.HttpContext.Session.GetString("Username") != null)
            {
                username = _contextAccessor.HttpContext.Session.GetString("Username").ToString();
            }
            return username;
        }
        #endregion
        #region UserID
        public static int? UserID()
        {
            int UserID = 0;
            if (_contextAccessor.HttpContext.Session.GetString("UserID") != null)
            {
                UserID = Convert.ToInt32(_contextAccessor.HttpContext.Session.GetString("UserID"));
            }
            return UserID;
        }
        #endregion

        public static string? EmailID()
        {
            string EmailID = "";
            if (_contextAccessor.HttpContext.Session.GetString("EmailID") != null)
            {
                EmailID = _contextAccessor.HttpContext.Session.GetString("EmailID").ToString();
            }
            return EmailID;
        }
        public static string? Role()
        {
            string Role = "";
            if (_contextAccessor.HttpContext.Session.GetString("Role") != null)
            {
                Role = _contextAccessor.HttpContext.Session.GetString("Role").ToString();
            }
            return Role;
        }
        public static string? ImagePath()
        {
            string ImagePath = "";
            if (_contextAccessor.HttpContext.Session.GetString("ImagePath") != null)
            {
                ImagePath = _contextAccessor.HttpContext.Session.GetString("ImagePath").ToString();
            }
            return ImagePath;
        }

        public static string? MobileNo()
        {
            string MobileNo = "";
            if (_contextAccessor.HttpContext.Session.GetString("MobileNo") != null)
            {
                MobileNo = _contextAccessor.HttpContext.Session.GetString("MobileNo").ToString();
            }
            return MobileNo;
        }

    }
}