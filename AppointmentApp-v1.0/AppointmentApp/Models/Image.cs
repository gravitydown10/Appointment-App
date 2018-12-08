using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppointmentApp.Models
{
    public class Image
    {
        public int Id { get; set; }
        public byte[] ImageContent { get; set; }
        public string ImageType { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }

    }
}