using TMPro;
using UnityEngine;

public class UI_StatToolTip : UI_ToolTip
{
  private Player_Stats playerStats;
  private TextMeshProUGUI statToolTipText;

  protected override void Awake()
  {
    base.Awake();
    playerStats = FindFirstObjectByType<Player_Stats>();
    statToolTipText = GetComponentInChildren<TextMeshProUGUI>();
  }

  public void ShowToolTip(bool show, RectTransform targetRect, StatType statType)
  {
    base.ShowToolTip(show, targetRect);
    
    statToolTipText.text = GetStatTextByType(statType);
  }

  public string GetStatTextByType(StatType type)
  {
    switch(type)
    {
      // Major stats
      case StatType.Strength:
        return "Увеличивает физический урон на 1% за каждый пункт" +
              "\nУвеличивает шанс крита на 0.5% за каждый пункт";
      case StatType.Agility:
        return "Увеличивает шанс крита на 0.3% за каждый пункт" +
              "\nУвеличивает шанс уклонения на 0.5% за каждый пункт";
      case StatType.Intelegence:
        return "Увеличивает элементальный урон на 0.5% за каждый пункт" +
              "\nДобавляет 1 к элементальному урону за каждый пункт";
      case StatType.Vitality:
        return "Увеличивает максимальное здоровье на 5 пунктов" +
              "\nУвеличивает броню на 1% за каждый пункт";

      // Physical damage
      case StatType.Damage:
        return "Увеличивает физический урон ваших атак";
      case StatType.AttackSpeed:
        return "Увеличивает скорость атаки";
      case StatType.CritChance:
        return "Увеличивает шанс критического урона";
      case StatType.CritPower:
        return "Увеличивает силу критического урона";
      case StatType.ArmorReduction:
        return "Увеличивает пробивание брони врагов";

      // Defense stats
      case StatType.MaxHealth:
        return "Увеличивает максимальное здоровье";
      case StatType.HelthRegen:
        return "Увеличивает регенерацию здоровья";
      case StatType.Evasion:
        return "Увеличивает шанс уклонения" +
              "\nОграничено 85%.";
      case StatType.Armor:
        return "Увеличивает броню, снижая входящий урон" +
              "\nОграничено 85%." +
              "\nТекущее снижение урона: " + playerStats.GetArmorReduction() * 100 + "%";

      // Elemental damage
      case StatType.FireDamage:
        return "Увеличивает огненный урон";
      case StatType.IceDamage:
        return "Увеличивает ледяной урон";
      case StatType.LightningDamage:
        return "Увеличивает электрический урон";

      // Elemental resistance
      case StatType.IceResistance:
        return "Увеличивает сопротивление льду";
      case StatType.FireResistance:
        return "Увеличивает сопротивление огню";
      case StatType.LightningResistance:
        return "Увеличивает сопротивление электричеству";

      // Other
      case StatType.ElementalDamage:
        return "Комбинирует все три стихийных элемента." +
                "\nБудет применен самый сильный стихийный урон." +
                "\nОстальные нанесут только половину урона.";
      default:
        return "Неизвестная характеристика";
    }
  }
}
