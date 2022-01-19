using System.Collections.Generic;
using WebClient.Models;

namespace WebClient.Mappers;

public static class Mapper
{
    public static Dictionary<string, Dictionary<string, int>> MapTemperatures(TemperatureLimitsOptions limitsOptions)
    {
        return new Dictionary<string, Dictionary<string, int>>()
        {
            {
                nameof(limitsOptions.Memory), new Dictionary<string, int>
                {
                    {nameof(limitsOptions.Memory.Danger), int.Parse(limitsOptions.Memory.Danger)},
                    {nameof(limitsOptions.Memory.Warning), int.Parse(limitsOptions.Memory.Warning)}
                }
            },
            {
                nameof(limitsOptions.GPU), new Dictionary<string, int>
                {
                    {nameof(limitsOptions.GPU.Danger), int.Parse(limitsOptions.GPU.Danger)},
                    {nameof(limitsOptions.GPU.Warning), int.Parse(limitsOptions.GPU.Warning)}
                }
            },
        };
    }
}