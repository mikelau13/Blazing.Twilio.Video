namespace Blazing.Twilio.WasmVideo.Shared
{
    /// <summary>
    /// The RoomDetails class is an object that represents a video chat room.
    /// </summary>
    public class RoomDetails
    {
        public string? Id { get; set; } = null!;

        public string? Name { get; set; } = null!;

        public int ParticipantCount { get; set; }

        public int MaxParticipants { get; set; }
    }
}
