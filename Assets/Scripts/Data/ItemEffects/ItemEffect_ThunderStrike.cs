using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/Item Data/Item Effect/Thunder strike on doing damage", fileName = "Item effect data - Thunder strike on doing damage")]
public class ItemEffect_ThunderStrike : ItemEffect_DataSO
{
  [SerializeField] private float chance = .15f;
  [SerializeField] private GameObject thunderStrikeVfx;

  override public void Subscribe(Player player)
  {
    base.Subscribe(player);
    player.combat.OnDoingPhysicalDamage += ThunderStrike;
  }

  override public void Unsubscribe()
  {
    base.Unsubscribe();
    player.combat.OnDoingPhysicalDamage -= ThunderStrike;
    player = null;
  }

  private void ThunderStrike(float damage)
  {
    if (Random.Range(0, 100) < chance)
    {
      player.vfx.CreateEffectOf(thunderStrikeVfx, player.transform);
    }
  }
}
