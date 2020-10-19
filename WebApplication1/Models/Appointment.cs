using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Appointment
    {
        public String Id { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public Appointment()
        {
            Id = Guid.NewGuid().ToString();
            Start = DateTime.Now;
            End = DateTime.Now;
        }

        public static Appointment app;
    }
}
