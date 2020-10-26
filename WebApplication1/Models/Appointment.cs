using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Appointment
    {
        public string Id { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string PatientName { get; set; }

        public Appointment()
        {
            Id = Guid.NewGuid().ToString();
            Start = DateTime.Now;
            End = DateTime.Now;
            var list = new List<string> { "Hydrogen", "Helium", "Lithium", "Beryllium", "Boron", "Carbon", "Nitrogen", "Oxygen", "Flourine", "Neon", "Sodium", "Magnesium" };
            int index = new Random().Next(list.Count);
            PatientName = list[index];
        }

        public static Appointment app;
    }
}
