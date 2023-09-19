using System.Text.Json.Serialization;

namespace Library.Models;

public class ServerTime
{
    public ServerTime(double value)
    {
        Value = value;
    }

    [JsonPropertyName("serverTime")]
    public double Value { get; private set; }
}