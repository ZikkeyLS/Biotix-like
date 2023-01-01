using System;
using UnityEngine;

public enum UnitType
{
    Player,
    AI,
    None
}

[Serializable]
public class UnitPreset
{
    public UnitType Type;
    public Color UnitColor;
}

public class TargetedPatricle
{
    public Transform Unit;
    public Node Target;

    public TargetedPatricle(Transform unit, Node target)
    {
        Unit = unit;
        Target = target;
    }
}

