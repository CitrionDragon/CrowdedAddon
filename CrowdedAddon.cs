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
using Il2CppInterop.Runtime.Injection;
using CrowdedAddon.Patches;

namespace CrowdedAddon;

public class CrowdedAddon: LotusAddon
{
    public static CrowdedAddon Instance = null!;

    private Harmony harmony;
    public override void Initialize()
    {
        NormalGameOptionsV09.RecommendedImpostors = NormalGameOptionsV09.MaxImpostors = Enumerable.Repeat(128, 128).ToArray();
        NormalGameOptionsV09.MinPlayers = Enumerable.Repeat(4, 128).ToArray();
        HideNSeekGameOptionsV09.MinPlayers = Enumerable.Repeat(4, 128).ToArray();

        harmony = new Harmony("com.citriondragon.crowdedaddon");
        harmony.PatchAll(Assembly.GetExecutingAssembly());
        
        ClassInjector.RegisterTypeInIl2Cpp<MeetingHudPagingBehaviour>();
        ClassInjector.RegisterTypeInIl2Cpp<ShapeShifterPagingBehaviour>();
        ClassInjector.RegisterTypeInIl2Cpp<VitalsPagingBehaviour>();
    }

    public override string Name { get; } = "Crowded Addon";

    public override VentLib.Version.Version Version { get; } = new CrowdedAddonVersion();
}


