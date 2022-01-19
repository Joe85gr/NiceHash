namespace WebClient.Models;

public class TemperatureLimitsOptions
{
    public const string TemperatureLimits = "TemperatureLimits";
    
    public Gpu GPU { get; set; }
    public Mem Memory { get; set; }
}

public class Gpu
{
    public string Warning { get; set; }
    public string Danger { get; set; }
}

public class Mem    
{
    public string Warning { get; set; }
    public string Danger { get; set; }
}