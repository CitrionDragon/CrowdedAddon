


using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Il2CppInterop.Runtime;
using UnityEngine;

namespace CrowdedAddon;

public static class Utils
{

    public static string GetRegionName(IRegionInfo region = null)
    {
        region ??= ServerManager.Instance.CurrentRegion;

        string name = region.Name;

        if (AmongUsClient.Instance.NetworkMode != NetworkModes.OnlineGame)
        {
            name = "Local Game";
            return name;
        }

        if (region.PingServer.EndsWith("among.us", System.StringComparison.Ordinal))
        {
            // Official server
            name = name switch
            {
                "North America" => "NA",
                "Europe" => "EU",
                "Asia" => "AS",
                _ => name
            };

            return name;
        }

        string ip = region.Servers.FirstOrDefault()?.Ip ?? string.Empty;

        if (ip.Contains("aumods.us", System.StringComparison.Ordinal) || ip.Contains("duikbo.at", System.StringComparison.Ordinal))
        {
            // Official Modded Server
            if (ip.Contains("au-eu"))
                name = "MEU";
            else if (ip.Contains("au-as"))
                name = "MAS";
            else
                name = "MNA";

            return name;
        }

        if (name.Contains("Niko", System.StringComparison.OrdinalIgnoreCase))
            name = name.Replace("233(", "-").TrimEnd(')');

        return name;
    }
}
