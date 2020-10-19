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

        public async Task SendMessage(string user, string message, string appointmentId)
        {
            await Clients.Groups(appointmentId).SendAsync("ReceiveMessage", user, message);
        }

        public async Task Disconnect(string appointmentId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, appointmentId);
        }
    }
}