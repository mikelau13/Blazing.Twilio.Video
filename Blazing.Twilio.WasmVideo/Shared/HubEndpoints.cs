namespace Blazing.Twilio.WasmVideo.Shared
{
    /// <summary>
    /// SignalR notification hub end point.
    /// </summary>
    public class HubEndpoints
    {
        public const string NotificationHub = "/notifications";
        public const string RoomsUpdated = nameof(RoomsUpdated);
    }
}
