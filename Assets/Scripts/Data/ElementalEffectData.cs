using System;
using UnityEngine;

[Serializable]
public class ElementalEffectData
{
    public float chillDuration;
    public float chillSlowMulipier;

    public float burnDuration;
    public float totalBurnDamage;

    public float shokDuration;
    public float shokDamage;
    public float shokCharge;

    public ElementalEffectData(Entity_Stats entityStats, DamageScaleData damageScale)
    {
        chillDuration = damageScale.chillDuration;
        chillSlowMulipier = damageScale.chillSlowMultipier;

        burnDuration = damageScale.burnDuration;
        totalBurnDamage = entityStats.offense.fireDamage.GetValue() * damageScale.burnDamageScale;

        shokDuration = damageScale.shockDuration;
        shokDamage = entityStats.offense.lightningDamage.GetValue() * damageScale.shockDamageScale;
        shokCharge = damageScale.shockCharge;
    }
}
