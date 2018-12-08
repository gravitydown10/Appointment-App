using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppointmentApp.Models;
using System.ComponentModel.DataAnnotations;

namespace AppointmentApp.ViewModels
{
    public class AppointmentViewModel
    {
        public IList<Specialization> Specializations { get; set; }
        public Appointment Appointment { get; set; }
        public IList<User> Users { get; set; }
        public IList<Schedule> Schedules { get; set; }
        [Display(Name ="Type")]
        [Required]
        public int SpecialId { get; set; }

       


    }
}