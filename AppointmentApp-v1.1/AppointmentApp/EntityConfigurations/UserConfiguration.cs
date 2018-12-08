using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using AppointmentApp.Models;

namespace AppointmentApp.EntityConfigurations
{
    public class UserConfiguration:EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            Property(u => u.Name).HasMaxLength(255).IsRequired();
            Property(u => u.Email).IsRequired().HasMaxLength(255);
            Property(u => u.Telephone).IsRequired();
            Property(u => u.Mobile).IsRequired();
            Property(u => u.Address).IsRequired().IsMaxLength().HasMaxLength(255);
            Property(u => u.Password).IsRequired().HasMaxLength(255);
            Property(u => u.ConfirmPassword).IsRequired().HasMaxLength(255);
        }
    }
}