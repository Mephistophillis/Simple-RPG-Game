using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/Item Data/Item Effect/Grand skill point effect", fileName = "Item effect data - Grand skill point")]
public class ItemEffect_GrandSkillPoint : ItemEffect_DataSO
{
  [SerializeField] private int pointsToAdd = 1;

  override public void ExecuteEffect()
  {
    UI ui = FindAnyObjectByType<UI>();
    ui.skillTreeUI.AddSkillPoints(pointsToAdd);
  }
}
