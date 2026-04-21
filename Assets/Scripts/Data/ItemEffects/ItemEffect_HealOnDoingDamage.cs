using UnityEngine;


[CreateAssetMenu(menuName = "RPG Setup/Item Data/Item Effect/Heal on doing damage", fileName = "Item effect data - Heal on doing damage")]
public class ItemEffect_HealOnDoingDamage : ItemEffect_DataSO
{
  [SerializeField] private float percentHealOnAttack = .2f;

  override public void Subscribe(Player player)
  {
    base.Subscribe(player);
    player.combat.OnDoingPhysicalDamage += HealOnDoingDamage;
  }

  override public void Unsubscribe()
  {
    base.Unsubscribe();
    player.combat.OnDoingPhysicalDamage -= HealOnDoingDamage;
    player = null;
  }

  private void HealOnDoingDamage(float damage)
  {
    player.health.IncreaseHealth(damage * percentHealOnAttack);
  }

}
