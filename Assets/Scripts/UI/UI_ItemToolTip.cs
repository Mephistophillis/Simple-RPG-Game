using System.Text;
using TMPro;
using UnityEngine;

public class UI_ItemToolTip : UI_ToolTip
{
  [SerializeField] private TextMeshProUGUI itemName;
  [SerializeField] private TextMeshProUGUI itemType;
  [SerializeField] private TextMeshProUGUI itemInfo;

  public void ShowToolTip(bool show, RectTransform targetRect, Inventory_Item itemToShow)
  {
    base.ShowToolTip(show, targetRect);

    itemName.text = itemToShow.itemData.itemName;
    itemType.text = itemToShow.itemData.itemType.ToString();
    itemInfo.text = GetItemInfo(itemToShow);
  }

  public string GetItemInfo(Inventory_Item item)
  {
    if (item.itemData.itemType == ItemType.Material)
      return "Используется для создания предметов";

    StringBuilder sb = new StringBuilder();

    sb.AppendLine("");

    foreach(var mod in item.modifiers)
    {
      string modType = GetStatNameByType(mod.statType);
      string modValue = mod.value.ToString();

      if (IsPercentageStat(mod.statType))
        modValue += "%";

      sb.AppendLine("+ " + modValue + " " + modType);
    }

    return sb.ToString();
  }

  private string GetStatNameByType(StatType type)
  {
    switch(type)
    {
      case StatType.MaxHealth: return "Макс. здоровье";
      case StatType.HelthRegen: return "Реген. здоровья";
      case StatType.Strength: return "Сила";
      case StatType.Agility: return "Ловкость";
      case StatType.Intelegence: return "Интеллект";
      case StatType.Vitality: return "Живучесть";
      case StatType.AttackSpeed: return "Скорость атаки";
      case StatType.Damage: return "Урон";
      case StatType.CritChance: return "Шанс крита";
      case StatType.CritPower: return "Сила крита";
      case StatType.ArmorReduction: return "Снижение брони";
      case StatType.FireDamage: return "Огненный урон";
      case StatType.IceDamage: return "Ледяной урон";
      case StatType.LightningDamage: return "Электрический урон";
      case StatType.Armor: return "Броня";
      case StatType.Evasion: return "Уклонение";
      case StatType.IceResistance: return "Сопротивление льду";
      case StatType.FireResistance: return "Сопротивление огню";
      case StatType.LightningResistance: return "Сопротивление электричеству";
      default: return "Неизвестная характеристика";
    }
  }

  private bool IsPercentageStat(StatType type)
  {
    switch (type)
    {
      case StatType.AttackSpeed:
      case StatType.Evasion:
      case StatType.CritChance:
      case StatType.CritPower:
      case StatType.ArmorReduction:
      case StatType.IceResistance:
      case StatType.FireResistance:
      case StatType.LightningResistance:
        return true;
      default:
        return false;
    }
  }
}
