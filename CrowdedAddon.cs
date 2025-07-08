using Lotus.Addons;
using Lotus.GameModes.Standard;
using CrowdedAddon.Version;
using Lotus.Roles;
using System.Collections.Generic;
using Lotus.GameModes;
using HarmonyLib;
using HarmonyLib.Tools;
using System.Reflection;
using AmongUs.GameOptions;
using System.Linq;

namespace CrowdedAddon;

public class CrowdedAddon: LotusAddon
{
    public static CrowdedAddon Instance = null!;

    private Harmony harmony;
    public override void Initialize()
    {
        /*
        // Create instances first
        List<CustomRole> allRoles = new List<CustomRole>() {};

        // Add your role to the gamemmode of your choice (Standard in this case.)
        allRoles.ForEach(StandardRoles.AddRole);

        // Register roles
        ExportCustomRoles(allRoles, typeof(StandardGameMode));
        
        // Export gamemode
        ExportGameModes(new List<IGameMode>() {});
        _ = new BombTagOptionHolder();*/
        
        harmony = new Harmony("com.citriondragon.crowdedaddon");
        harmony.PatchAll(Assembly.GetExecutingAssembly());
    }

    public override string Name { get; } = "Crowded Addon";

    public override VentLib.Version.Version Version { get; } = new CrowdedAddonVersion();
}


