using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GymBoyAttendance.Models
{
    public class Attendance
    {
        public int ID { get; set; }
        public string TrainingLocation { get; set; }
        public DateTime? TrainingDate { get; set; }
        public int TotalPresent { get; set; }
    }
}