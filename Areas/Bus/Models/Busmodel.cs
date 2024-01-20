﻿using System.ComponentModel.DataAnnotations;

namespace Bus_Ticket_Booking_Management_System.Areas.Bus.Models
{
    public class Busmodel
    {
        public int BusID { get; set; }
        public string? RegistrationNumber { get; set; }
        [Required(ErrorMessage ="Please Select Bus Type")]
        public int BusTypeID { get; set; }
        public string? NameOfManufacturer { get; set; }
        public string? ModelNo { get; set; }

    }
    public class BusTypeDDL
    {
        public int BusTypeID { get; set; }
        public string? BusType { get; set; }
    }
}