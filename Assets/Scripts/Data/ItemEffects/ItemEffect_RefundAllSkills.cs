using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/Item Data/Item Effect/Refund all skills effect", fileName = "Item effect data - refund all skills")]
public class ItemEffect_RefundAllSkills : ItemEffect_DataSO
{
  public override void ExecuteEffect()
  {
    UI ui = FindFirstObjectByType<UI>();
    ui.skillTreeUI.RefundAlSkills();
  }
}
