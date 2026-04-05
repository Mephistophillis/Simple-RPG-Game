using UnityEngine;

public class Skill_DomainExpansion : Skill_Base
{
  [SerializeField] private GameObject domainPrefab;

  [Header("Slowing down Upgrade")]
  [SerializeField] private float slowDownPercent = 0.9f;
  [SerializeField] private float slowDownDomainDuration = 5f;

  [Header("Spell Casting Upgrade")]
  [SerializeField] private float spellCastingDomainSlowDown = 1f;
  [SerializeField] private float spellCastingDomainDuration = 8f;

  [Header("Domain details")]
  public float maxDomainSize = 15f;
  public float expandSpeed = 3f;

  public float GetDomainDuration()
  {
    if (upgradeType == SkillUpgradeType.Domain_SlowingDown)
      return slowDownDomainDuration;
    else
      return spellCastingDomainDuration;
  }

  public float GetSlowPercentage()
  {
    if (upgradeType == SkillUpgradeType.Domain_SlowingDown)
      return slowDownPercent;
    else
      return spellCastingDomainSlowDown;
  }

  public bool InstantDomain()
  {
    return upgradeType != SkillUpgradeType.Domain_EchoSpam
        && upgradeType != SkillUpgradeType.Domain_ShardSpam;
  }

  public void CreateDomain()
  {
    GameObject domain = Instantiate(domainPrefab, transform.position, Quaternion.identity);
    domain.GetComponent<SkillObject_DomainExpansion>().SetupDomain(this);
  }
}
