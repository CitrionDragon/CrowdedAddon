using Lotus.Addons;
using CrowdedAddon.Version;
using HarmonyLib;
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
        NormalGameOptionsV10.RecommendedImpostors = NormalGameOptionsV10.MaxImpostors = Enumerable.Repeat(128, 128).ToArray();
        NormalGameOptionsV10.MinPlayers = Enumerable.Repeat(4, 128).ToArray();
        HideNSeekGameOptionsV10.MinPlayers = Enumerable.Repeat(4, 128).ToArray();

        ClassInjector.RegisterTypeInIl2Cpp<MeetingHudPagingBehaviour>();
        ClassInjector.RegisterTypeInIl2Cpp<ShapeShifterPagingBehaviour>();
        ClassInjector.RegisterTypeInIl2Cpp<VitalsPagingBehaviour>();
        
        harmony = new Harmony("com.citriondragon.crowdedaddon");
        harmony.PatchAll(Assembly.GetExecutingAssembly());
        
        ClassInjector.RegisterTypeInIl2Cpp<MeetingHudPagingBehaviour>();
        ClassInjector.RegisterTypeInIl2Cpp<ShapeShifterPagingBehaviour>();
        ClassInjector.RegisterTypeInIl2Cpp<VitalsPagingBehaviour>();
    }

    public override string Name { get; } = "Crowded Addon";

    public override VentLib.Version.Version Version { get; } = new CrowdedAddonVersion();
}


