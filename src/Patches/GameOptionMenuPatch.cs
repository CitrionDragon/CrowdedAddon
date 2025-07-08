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
        NormalGameOptionsV09.MinPlayers = Enumerable.Repeat(4, 128).ToArray();
        HideNSeekGameOptionsV09.MinPlayers = Enumerable.Repeat(4, 128).ToArray();
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
        /*
        if (ModGameOptionsMenu.OptionList.TryGetValue(__instance, out int index))
        {
            OptionItem item = OptionItem.AllOptions[index];
            __instance.TitleText.text = item.GetName();
            item.OptionBehaviour = __instance;
            return false;
        }*/

        return true;
    }
    /*
    [HarmonyPatch(nameof(NumberOption.UpdateValue))]
    [HarmonyPrefix]
    private static bool UpdateValuePrefix(NumberOption __instance)
    {
        if (ModGameOptionsMenu.OptionList.TryGetValue(__instance, out int index))
        {
            OptionItem item = OptionItem.AllOptions[index];

            switch (item)
            {
                case IntegerOptionItem integerOptionItem:
                    integerOptionItem.SetValue(integerOptionItem.Rule.GetNearestIndex(__instance.GetInt()));
                    break;
            }
            return false;
        }

        return true;
    }
    */
    /*
    [HarmonyPatch(nameof(NumberOption.FixedUpdate))]
    [HarmonyPrefix]
    private static bool FixedUpdatePrefix(NumberOption __instance)
    {
        if (ModGameOptionsMenu.OptionList.TryGetValue(__instance, out int index))
        {
            __instance.MinusBtn.SetInteractable(true);
            __instance.PlusBtn.SetInteractable(true);

            if (!Mathf.Approximately(__instance.oldValue, __instance.Value))
            {
                __instance.oldValue = __instance.Value;
                __instance.ValueText.text = GetValueString(__instance, __instance.Value, OptionItem.AllOptions[index]);
            }

            return false;
        }

        return true;
    }
    */
    /*
    private static string GetValueString(NumberOption __instance, float value, OptionItem item)
    {
        if (__instance.ZeroIsInfinity && Mathf.Abs(value) < 0.0001f) return "<b>∞</b>";

        return item == null ? value.ToString(__instance.FormatString) : item.GetString();
    }
*/
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