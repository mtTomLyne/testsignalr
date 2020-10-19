using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
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

        [HttpGet]
        public async Task<JsonResult> Get()
        {
            var appointment = Appointment.app;
            await hub.Clients.Groups("testgroup").SendAsync("ReceiveMessage", appointment.Start, appointment.End);
            return new JsonResult(appointment);
        }

        [HttpPost]
        public async Task<JsonResult> Post()
        {
            var appointment = new Appointment();
            AppointmentsDB.Appointments().Add(appointment);
            //await hub.SendMessage(appointment.Id, appointment.Id, appointment.Id);
            return new JsonResult(AppointmentsDB.Appointments());
        }

    }
}
