using TMPro;
using UnityEngine;

public class UI_StatSlot : MonoBehaviour
{
  private Entity_Stats playerStats;
  private RectTransform rect;
  private UI ui;

  [SerializeField] private StatType statsSlotType;
  [SerializeField] private TextMeshProUGUI statName;
  [SerializeField] private TextMeshProUGUI statValue;

  private void OnValidate()
  {
    gameObject.name = "UI_Stat - " + GetStatNameByType(statsSlotType);
    statName.text = GetStatNameByType(statsSlotType);
  }

  private void Awake()
  {
    rect = GetComponent<RectTransform>();
    ui = GetComponentInParent<UI>();
    playerStats = FindObjectOfType<Entity_Stats>();
  }

  public void UpdateStatValue()
  {
    Stat statToUpdate = playerStats.GetStatByType(statsSlotType);

    if (statToUpdate == null && statsSlotType != StatType.ElementalDamage)
    {
      Debug.LogWarning("Stat not found: " + statsSlotType);
      return;
    }

    float value = 0;

    switch (statsSlotType)
    {
      // Major stats
      case StatType.Strength:
        value = playerStats.major.strength.GetValue();
        break;
      case StatType.Agility:
        value = playerStats.major.agility.GetValue();
        break;
      case StatType.Intelegence:
        value = playerStats.major.intelligence.GetValue();
        break;
      case StatType.Vitality:
        value = playerStats.major.vitality.GetValue();
        break;

      // Offence stats
      case StatType.Damage:
        value = playerStats.GetBaseDamage();
        break;
      case StatType.CritChance:
        value = playerStats.GetCritChance();
        break;
      case StatType.CritPower:
        value = playerStats.GetCritPower();
        break;
      case StatType.ArmorReduction:
        value = playerStats.GetArmorReduction() * 100;
        break;
      case StatType.AttackSpeed:
        value = playerStats.offense.attackSpeed.GetValue() * 100;
        break;

      // Defense stats
      case StatType.MaxHealth:
        value = playerStats.GetMaxHealth();
        break;
      case StatType.HelthRegen:
        value = playerStats.resources.healthRegen.GetValue();
        break;
      case StatType.Evasion:
        value = playerStats.GetEvasion();
        break;
      case StatType.Armor:
        value = playerStats.GetBaseArmor();
        break;

      // Elemental damage stats
      case StatType.FireDamage:
        value = playerStats.offense.fireDamage.GetValue();
        break;
      case StatType.IceDamage:
        value = playerStats.offense.iceDamage.GetValue();
        break;
      case StatType.LightningDamage:
        value = playerStats.offense.lightningDamage.GetValue();
        break;
      case StatType.ElementalDamage:
        value = playerStats.GetElementalDamage(out ElementType element, 1);
        break;

      // Elemental resistance stats
      case StatType.FireResistance:
        value = playerStats.GetElementalResistance(ElementType.Fire) * 100;
        break;
      case StatType.IceResistance:
        value = playerStats.GetElementalResistance(ElementType.Ice) * 100;
        break;
      case StatType.LightningResistance:
        value = playerStats.GetElementalResistance(ElementType.Lightning) * 100;
        break;
    }

    statValue.text = IsPercentageStat(statsSlotType) ? value + "%" : value.ToString();
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
      case StatType.ElementalDamage: return "Элементальный урон";
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
