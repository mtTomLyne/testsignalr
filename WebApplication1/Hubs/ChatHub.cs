using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        static string a = "";
        public async Task Connect(string roomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "appointment" + roomId);
        }

        public async Task SendMessage(string user, string message, string roomId)
        {
            await Clients.Groups("appointment" + roomId).SendAsync("ReceiveMessage", user, message);
        }

        public async Task Disconnect(string roomId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "appointment" + roomId);
        }

        public async Task UpdateMessage(string message, string roomId)
        {
            //await Clients.Groups("appointment" + roomId).SendAsync("UpdateMessage", message);
        }
    }
}