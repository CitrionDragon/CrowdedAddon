using CrowdedAddon.Patches;
using HarmonyLib;

namespace Lotus.Patches;

[HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Start))]
internal static class MeetingHudStartPatch
{
    public static void Postfix(MeetingHud __instance)
    {
        Crowded.MeetingHudStartPatch.Postfix(__instance);
    }
}