using System.Security.Cryptography;
using System.Text;
using System;
using System.Collections.Generic;

namespace Bus_Ticket_Booking_Management_System.Payment
{
    public class Utils
    {
        public static bool ValidateWebhookSignature(Dictionary<string, string> attributes, string secret)
        {
            string signature = attributes["razorpay_signature"];
            attributes.Remove("razorpay_signature");

            string message = string.Join("|", attributes.Values);
            byte[] key = Encoding.UTF8.GetBytes(secret);

            using (HMACSHA256 hmac = new HMACSHA256(key))
            {
                byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
                string generatedSignature = BitConverter.ToString(hash).Replace("-", "").ToLower();
                return generatedSignature.Equals(signature);
            }
        }
    }
}
