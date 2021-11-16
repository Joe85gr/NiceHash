using System.Text.Json.Serialization;

namespace Library.Models
{
    public class ServerTime
    {
        [JsonPropertyName("serverTime")]
        public double Value { get; set; }
    }

}
