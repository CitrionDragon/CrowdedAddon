using System;
using System.Collections.Generic;
using System.Linq;
using AmongUs.GameOptions;
using CrowdedAddon;
using CrowdedAddon.Patches;
using HarmonyLib;
using UnityEngine;

namespace Lotus.Patches;

public static class ModGameOptionsMenu
{
    public static int TabIndex;
    public static Dictionary<OptionBehaviour, int> OptionList = new();
    public static Dictionary<int, OptionBehaviour> BehaviourList = new();
    public static Dictionary<int, CategoryHeaderMasked> CategoryHeaderList = new();
}

// This patch allows host to have bigger range when setting options
[HarmonyPatch(typeof(NumberOption))]
public static class NumberOptionPatch
{
    private static int IncrementMultiplier
    {
        get
        {
            if (Input.GetKeyInt(KeyCode.LeftShift) || Input.GetKeyInt(KeyCode.RightShift)) return 5;
            if (Input.GetKeyInt(KeyCode.LeftControl) || Input.GetKeyInt(KeyCode.RightControl)) return 10;
            return 1;
        }
    }

    [HarmonyPatch(nameof(NumberOption.Initialize))]
    [HarmonyPrefix]
    private static bool InitializePrefix(NumberOption __instance)
    {
        switch (__instance.Title)
        {
            case StringNames.GameNumImpostors:
                __instance.ValidRange = new(0, Crowded.MaxImpostors);
                __instance.Value = (float)Math.Round(__instance.Value, 2);
                break;
            case StringNames.CapacityLabel:
                __instance.ValidRange = new(4, 127);
                break;
        }
        return true;
    }

    [HarmonyPatch(nameof(NumberOption.Increase))]
    [HarmonyPrefix]
    public static bool IncreasePrefix(NumberOption __instance)
    {
        if (Mathf.Approximately(__instance.Value, __instance.ValidRange.max))
        {
            __instance.Value = __instance.ValidRange.min;
            __instance.UpdateValue();
            __instance.OnValueChanged.Invoke(__instance);
            return false;
        }

        float increment = IncrementMultiplier * __instance.Increment;

        if (__instance.Value + increment < __instance.ValidRange.max)
        {
            __instance.Value += increment;
            __instance.UpdateValue();
            __instance.OnValueChanged.Invoke(__instance);
            return false;
        }

        return true;
    }

    [HarmonyPatch(nameof(NumberOption.Decrease))]
    [HarmonyPrefix]
    public static bool DecreasePrefix(NumberOption __instance)
    {
        if (Mathf.Approximately(__instance.Value, __instance.ValidRange.min))
        {
            __instance.Value = __instance.ValidRange.max;
            __instance.UpdateValue();
            __instance.OnValueChanged.Invoke(__instance);
            return false;
        }

        float increment = IncrementMultiplier * __instance.Increment;

        if (__instance.Value - increment > __instance.ValidRange.min)
        {
            __instance.Value -= increment;
            __instance.UpdateValue();
            __instance.OnValueChanged.Invoke(__instance);
            return false;
        }

        return true;
    }
}