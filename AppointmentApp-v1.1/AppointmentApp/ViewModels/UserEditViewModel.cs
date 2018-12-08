using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AppointmentApp.ViewModels
{
    public class UserEditViewModel
    {
        public int? Id { get; set; }
        [Required]
        public string Telephone { get; set; }
        [Required]
        public string Mobile { get; set; }
        [Required]
        public string Address { get; set; }
    }
}