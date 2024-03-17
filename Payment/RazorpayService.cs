//using Razorpay.Api;
//using Microsoft.Extensions.Configuration;

//namespace Bus_Ticket_Booking_Management_System.Payment
//{
//    public class RazorpayService
//    {
//        private readonly IConfiguration _config;

//        public RazorpayService(IConfiguration config)
//        {
//            _config = config;
//        }

//        public RazorpayClient GetClient()
//        {
//            string keyId = _config["Razorpay:KeyId"];
//            string keySecret = _config["Razorpay:KeySecret"];

//            return new RazorpayClient(keyId, keySecret);
//        }
//    }
//}
