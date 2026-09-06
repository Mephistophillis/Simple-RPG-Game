using UnityEngine;

public class Player_SkillManager : MonoBehaviour
{
  public Skill_Dash dash { get; private set; }
  public Skill_Shard shard { get; private set; }
  public Skill_SwordThrow swordThrow { get; private set; }
  public Skill_TimeEcho timeEcho { get; private set; }
  public Skill_DomainExpansion domainExpansion { get; private set; }

  public Skill_Base[] allSkills { get; private set; }

  private void Awake()
  {
    dash = GetComponentInChildren<Skill_Dash>();
    shard = GetComponentInChildren<Skill_Shard>();
    swordThrow = GetComponentInChildren<Skill_SwordThrow>();
    timeEcho = GetComponentInChildren<Skill_TimeEcho>();
    domainExpansion = GetComponentInChildren<Skill_DomainExpansion>();

    allSkills = GetComponentsInChildren<Skill_Base>();
  }

  public void ReduceAllCooldownsBy(float amount)
  {
    foreach (Skill_Base skill in allSkills)
    {
      skill.ReduceCooldownBy(amount);
    }
  }

  public Skill_Base GetSkillByType(SkillType type)
  {
    switch (type)
    {
      case SkillType.Dash: return dash;
      case SkillType.TimeShard: return shard;
      case SkillType.TimeEcho: return timeEcho;
      case SkillType.SwordThrow: return swordThrow;
      case SkillType.DomainExpansion: return domainExpansion;

      default:
        Debug.LogWarning($"Skill type {type} is not implemented");
        return null;
    }
  }
}
