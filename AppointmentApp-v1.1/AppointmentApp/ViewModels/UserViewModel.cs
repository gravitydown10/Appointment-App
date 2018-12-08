using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppointmentApp.Models;

namespace AppointmentApp.ViewModels
{
    public class UserViewModel
    {
        public Image Image { get; set; }
        public User User { get; set; }
        public Specialization Specialization { get; set; }

    }
}