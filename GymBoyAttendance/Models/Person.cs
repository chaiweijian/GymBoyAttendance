using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GymBoyAttendance.Models
{
    public class Person
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string LivingArea { get; set; }
        public int PresentCount { get; set; }
    }
}