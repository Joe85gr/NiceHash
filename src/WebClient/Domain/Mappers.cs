using System.Collections.Generic;
using WebClient.Models;

namespace WebClient.Domain;

public class Mappers
{
    public static Dictionary<string, Dictionary<string, int>> MapTemperatures(TemperatureLimitsOptions temperatureLimits)
    {
        return new Dictionary<string, Dictionary<string, int>>()
        {
            {
                "GPU Temperture", new Dictionary<string, int>
                {
                    { nameof(temperatureLimits.GPU.Danger), int.Parse(temperatureLimits.GPU.Danger)}, 
                    { nameof(temperatureLimits.GPU.Warning), int.Parse(temperatureLimits.GPU.Warning)}
                }
            },
            {
                "VRAM Temperture", new Dictionary<string, int>
                {
                    { nameof(temperatureLimits.Memory.Danger), int.Parse(temperatureLimits.Memory.Danger)}, 
                    { nameof(temperatureLimits.Memory.Warning), int.Parse(temperatureLimits.Memory.Warning) }
                }
            },
        };
    }
}