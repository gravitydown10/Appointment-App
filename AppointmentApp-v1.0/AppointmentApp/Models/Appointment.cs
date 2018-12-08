using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AppointmentApp.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        [Display(Name ="Doctor")]
        public int DoctorId { get; set; }
        public int UId { get; set; }
        public DateTime Date { get; set; }
        public Schedule Schedule { get; set; }
        public int ScheduleId { get; set; }
    }
}