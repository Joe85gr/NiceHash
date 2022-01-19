namespace WebClient.Models;

public class TemperatureLimitsOptions
{
    public const string TemperatureLimits = "TemperatureLimits";
    
    public Gpu Gpu { get; set; }
    public Mem Memory { get; set; }
}