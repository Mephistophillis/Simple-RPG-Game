using System;
using System.Text;
using UnityEngine;

[Serializable]
public class Inventory_Item
{
  private string itemId;

  public ItemDataSO itemData;
  public int stackSize = 1;

  public ItemModifier[] modifiers { get; private set; }
  public ItemEffect_DataSO itemEffect;

  public int buyPrice { get; private set; }
  public float sellPrice { get; private set; }

  public Inventory_Item(ItemDataSO itemData)
  {
    this.itemData = itemData;
    itemEffect = itemData.itemEffect;

    buyPrice = itemData.itemPrice;
    sellPrice = itemData.itemPrice * .35f;

    modifiers = EquipmentData()?.modifiers;
    itemId = itemData.itemName + " - " + Guid.NewGuid().ToString();
  }

  public void AddModifiers(Entity_Stats playerStats)
  {
    foreach (var mod in modifiers)
    {
      Stat statToModify = playerStats.GetStatByType(mod.statType);
      statToModify.AddModifier(mod.value, itemId);
    }
  }

  public void RemoveModifiers(Entity_Stats playerStats)
  {
    foreach (var mod in modifiers)
    {
      Stat statToModify = playerStats.GetStatByType(mod.statType);
      statToModify.RemoveModifier(itemId);
    }
  }

  public void AddItemEffect(Player player) => itemEffect?.Subscribe(player);
  public void RemoveItemEffect() => itemEffect?.Unsubscribe();

  private EquipmentDataSO EquipmentData()
  {
    if (itemData is EquipmentDataSO equipment)
      return equipment;

    return null;
  }

  public bool CanAddStack() => stackSize < itemData.maxStackSize;

  public void AddStack() => stackSize++;
  public void RemoveStack() => stackSize--;

  public string GetItemInfo()
  {
    StringBuilder sb = new StringBuilder();

    if (itemData.itemType == ItemType.Material)
    {
      sb.AppendLine("");
      sb.AppendLine("Используется для крафта");
      sb.AppendLine("");
      sb.AppendLine("");
      return sb.ToString();
    }

    if (itemData.itemType == ItemType.Consumable)
    {

      sb.AppendLine("");
      sb.AppendLine(itemData.itemEffect.effectDescription);
      sb.AppendLine("");
      sb.AppendLine("");
      return sb.ToString();
    }

    sb.AppendLine("");

    foreach(var mod in modifiers)
    {
      string modType = GetStatNameByType(mod.statType);
      string modValue = mod.value.ToString();

      if (IsPercentageStat(mod.statType))
        modValue += "%";

      sb.AppendLine("+ " + modValue + " " + modType);
    }

    if (itemEffect != null)
    {
      sb.AppendLine("");
      sb.AppendLine("Эффект: ");
      sb.AppendLine(itemEffect.effectDescription);
    }

    sb.AppendLine("");
    sb.AppendLine("");

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
