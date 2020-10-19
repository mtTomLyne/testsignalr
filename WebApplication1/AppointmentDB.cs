using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1
{
    public class AppointmentsDB
    {
        private static List<Appointment> appointments;

        public AppointmentsDB()
        {
            appointments = new List<Appointment>();
        }

        public static List<Appointment> Appointments()
        {
            return appointments;
        }
    }
}
