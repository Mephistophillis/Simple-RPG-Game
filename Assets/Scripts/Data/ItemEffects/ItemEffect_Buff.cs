using System;
using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/Item Data/Item Effect/Buff effect", fileName = "Item effect data - Buff")]
public class ItemEffect_Buff : ItemEffect_DataSO
{
  [SerializeField] private BuffEffectData[] buffsToApply;
  [SerializeField] private float duration;
  [SerializeField] private string source = Guid.NewGuid().ToString();

  private Player_Stats playerStats;

  override public bool CanBeUsed()
  {
    if (playerStats == null)
      playerStats = FindAnyObjectByType<Player_Stats>();

    if (playerStats.CanApplyBuffOf(source))
      return true;
    else
    {
      Debug.Log("Same buff is already active! " + source);
      return false;
    }
  }

  override public void ExecuteEffect()
  {
    playerStats.ApplyBuff(buffsToApply, duration, source);
  }
}
