namespace WebClient.Models;

public class TemperatureLimitsOptions
{
    public const string TemperatureLimits = "TemperatureLimits";
    
    public Gpu Gpu { get; set; }
    public Mem Memory { get; set; }
}

public abstract class Gpu
{
    public string Warning { get; set; }
    public string Danger { get; set; }
}

public abstract class Mem    
{
    public string Warning { get; set; }
    public string Danger { get; set; }
}