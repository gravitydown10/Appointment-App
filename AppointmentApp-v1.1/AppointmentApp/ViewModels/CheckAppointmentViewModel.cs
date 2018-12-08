using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AppointmentApp.ViewModels
{
    public class CheckAppointmentViewModel
    {
        public int Id { get; set; }
        public string DoctorName { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string Specialist { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string PatientName { get; set; }
    }
}