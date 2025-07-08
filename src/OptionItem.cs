using System;
using System.Collections.Generic;
using UnityEngine;

namespace CrowdedAddon;
/*
public abstract class OptionItem
{
    public static IReadOnlyList<OptionItem> AllOptions => Options;
    private static readonly List<OptionItem> Options = new(1024);

    
}

public class IntegerOptionItem(int id, string name, IntegerValueRule rule, int defaultValue, TabGroup tab, bool isSingleValue = false) : OptionItem(id, name, rule.GetNearestIndex(defaultValue), tab, isSingleValue)
{

}

public abstract class ValueRule<T>(T minValue, T maxValue, T step)
{
    public ValueRule((T, T, T) tuple)
        : this(tuple.Item1, tuple.Item2, tuple.Item3) { }

    public T MinValue { get; protected set; } = minValue;
    public T MaxValue { get; protected set; } = maxValue;
    public T Step { get; protected set; } = step;

    public abstract int RepeatIndex(int value);
    public abstract T GetValueByIndex(int index);
    public abstract int GetNearestIndex(T num);
}

public class IntegerValueRule : ValueRule<int>
{
    public IntegerValueRule(int minValue, int maxValue, int step)
        : base(minValue, maxValue, step) { }

    public IntegerValueRule((int, int, int) tuple)
        : base(tuple) { }

    public static implicit operator IntegerValueRule((int, int, int) tuple)
    {
        return new(tuple);
    }

    public override int RepeatIndex(int value)
    {
        int MaxIndex = (MaxValue - MinValue) / Step;
        value %= MaxIndex + 1;
        if (value < 0) value = MaxIndex;

        return value;
    }

    public override int GetValueByIndex(int index)
    {
        return (RepeatIndex(index) * Step) + MinValue;
    }

    public override int GetNearestIndex(int num)
    {
        return (int)Math.Round((num - MinValue) / (float)Step);
    }
}*/