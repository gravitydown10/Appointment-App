using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppointmentApp.Models;


namespace AppointmentApp.ViewModels
{
    public class CreateUserViewModel
    {
        public IList<Role> Roles { get; set; }
        public User User { get; set; }

    }
}