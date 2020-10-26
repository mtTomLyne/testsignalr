using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    public class AppointmentsHub : Hub
    {
        static string a = "";
        public async Task Connect(string appointmentId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, appointmentId);
        }

        public async Task Disconnect(string appointmentId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, appointmentId);
        }
    }
}