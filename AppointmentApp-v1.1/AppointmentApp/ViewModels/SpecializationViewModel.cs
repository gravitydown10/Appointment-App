using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppointmentApp.Models;
using System.ComponentModel.DataAnnotations;

namespace AppointmentApp.ViewModels
{
    public class SpecializationViewModel
    {
        public int Id { get; set; }
        public IList<Specialization> Specializations { get; set; }
        [Required]
        [Display(Name ="Add You Working Field")]
        public int SpecialField { get; set; }
    }
}