using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AppointmentApp.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Telephone { get; set; }
        [Required]
        public string Mobile { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Email { get; set; }
        public bool IsADoctor { get; set; }
        public Role Role { get; set; }
        [Display(Name = "Role")]
        public int RoleId { get; set; }
        public Specialization Specialization { get; set; }
        public int? SpecializationId { get; set; }
        [Required]
        public string Password { get; set; }
        [Display(Name = "Confirm Password")]
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}