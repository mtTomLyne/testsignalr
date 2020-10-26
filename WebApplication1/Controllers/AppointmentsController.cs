using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SignalRChat.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        IHubContext<AppointmentsHub> hub;

        public AppointmentsController(IServiceProvider _provider)
        {
            hub = _provider.GetRequiredService<IHubContext<AppointmentsHub>>();
        }

        [HttpGet("{id}")]
        public async Task<JsonResult> GetAll(string id)
        {
            Appointment appt = AppointmentsDB.Appointments().FirstOrDefault(x => x.Id == id);
            //await hub.Clients.Groups("testgroup").SendAsync("ReceiveMessage", appt.Start, appt.End);
            return new JsonResult(appt);
        }

        [HttpGet]
        public async Task<JsonResult> Get()
        {
            List<Appointment> l = AppointmentsDB.Appointments();
            //await hub.Clients.Groups("testgroup").SendAsync("ReceiveMessage", l, l);
            return new JsonResult(AppointmentsDB.Appointments());
        }

        [HttpPost]
        public async Task<JsonResult> Post()
        {
            Appointment appointment = new Appointment();
            AppointmentsDB.Appointments().Add(appointment);

            List<Appointment> appointments = AppointmentsDB.Appointments();

            string jsonStr = JsonConvert.SerializeObject(appointments, Formatting.Indented);
            await hub.Clients.Groups("AllAppointments").SendAsync("AllAppointments", jsonStr);
            return new JsonResult(appointment);
        }

        [HttpPost("{id}")]
        public async Task<JsonResult> ModifyOne(string id)
        {
            Appointment appt = AppointmentsDB.Appointments().FirstOrDefault(x => x.Id == id);

            if (Request.Form["PatientName"].Count > 0)
            {
                appt.PatientName = Request.Form["PatientName"];
            }

            List<Appointment> appointments = AppointmentsDB.Appointments();
            string jsonStr = JsonConvert.SerializeObject(appointments, Formatting.Indented);

            await hub.Clients.Groups(appt.Id).SendAsync("UpdateApptInfo", appt.Id, appt);
            //await hub.Clients.Groups("AllAppointments").SendAsync("AllAppointments", jsonStr);

            return new JsonResult(appt);
        }

    }
}
