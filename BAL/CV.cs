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
    }
}