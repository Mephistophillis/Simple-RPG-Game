using System;
using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/Item Data/Item Effect/Buff effect", fileName = "Item effect data - Buff")]
public class ItemEffect_Buff : ItemEffect_DataSO
{
  [SerializeField] private BuffEffectData[] buffsToApply;
  [SerializeField] private float duration;
  [SerializeField] private string source = Guid.NewGuid().ToString();

  override public bool CanBeUsed(Player player)
  {
    if (player.stats.CanApplyBuffOf(source))
    {
      this.player = player;

      return true;
    }
    else
    {
      Debug.Log("Same buff is already active! " + source);
      return false;
    }
  }

  override public void ExecuteEffect()
  {
    player.stats.ApplyBuff(buffsToApply, duration, source);
    player = null;
  }
}
