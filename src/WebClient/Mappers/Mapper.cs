using System.Collections.Generic;
using WebClient.Models;

namespace WebClient.Mappers;

public static class Mapper
{
    public static Dictionary<string, Dictionary<string, int>> MapTemperatures(TemperatureLimitsOptions limitsOptions)
    {
        return new Dictionary<string, Dictionary<string, int>>
        {
            {
                nameof(limitsOptions.Memory), new Dictionary<string, int>
                {
                    {nameof(limitsOptions.Memory.Danger).ToLower(), int.Parse(limitsOptions.Memory.Danger)},
                    {nameof(limitsOptions.Memory.Warning).ToLower(), int.Parse(limitsOptions.Memory.Warning)}
                }
            },
            {
                nameof(limitsOptions.Gpu), new Dictionary<string, int>
                {
                    {nameof(limitsOptions.Gpu.Danger).ToLower(), int.Parse(limitsOptions.Gpu.Danger)},
                    {nameof(limitsOptions.Gpu.Warning).ToLower(), int.Parse(limitsOptions.Gpu.Warning)}
                }
            }
        };
    }
}