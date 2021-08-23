using Blazing.Twilio.WasmVideo.Shared;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Blazing.Twilio.WasmVideo.Server.Hubs
{
    /// <summary>
    /// When a user creates a room in the application their client-side code will notify the server and, 
    /// ultimately, other clients of the new room. This is done with a SignalR notification hub.
    /// </summary>
    public class NotificationHub : Hub
    {
        /// <summary>
        /// The NotificationHub will asynchronously send a message to all other clients notifying them 
        /// when a room is added.
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public Task RoomsUpdated(string room) =>
            Clients.All.SendAsync(HubEndpoints.RoomsUpdated, room);
    }
}
