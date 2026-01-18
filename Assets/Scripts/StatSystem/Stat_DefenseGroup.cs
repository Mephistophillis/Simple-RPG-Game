using System;
using UnityEngine;

[Serializable]
public class Stat_DefenseGroup
{
    // Physical defense
    public Stat armor;
    public Stat evasion;

    // Elemental resistance
    public Stat fireResistance;
    public Stat iceResistance;
    public Stat lightningResistance;
}
