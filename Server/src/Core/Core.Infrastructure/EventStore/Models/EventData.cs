namespace Core.Infrastructure.EventStore.Models
{
    public class EventData
    {
        public EventData(string stream, string @event, byte[] data)
        {
            Stream = stream;
            Event = @event;
            Data = data;
        }

        public string Id { get; set; }
        public string Stream { get; set; }
        public string Event { get; set; }
        public byte[] Data { get; set; }
        public int Timestamp { get; set; }
    }
}
