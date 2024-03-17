namespace Bus_Ticket_Booking_Management_System.Models
{
    public class OrderModel
    {

        public decimal OrderAmount { get; set; }
        public decimal OrderAmountInSubUnits
        {
            get
            {
                return Convert.ToInt32( OrderAmount )* 100;
            }
        }
        public string Currency { get; set; }
        public int Payment_Capture { get; set; }
        public Dictionary<string, string> Notes { get; set; }

    }
}
