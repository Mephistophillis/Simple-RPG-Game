using System.Collections.Generic;
using UnityEngine;

public class Skill_DomainExpansion : Skill_Base
{
  [SerializeField] private GameObject domainPrefab;

  [Header("Slowing down Upgrade")]
  [SerializeField] private float slowDownPercent = 0.8f;
  [SerializeField] private float slowDownDomainDuration = 5f;

  [Header("Shard cast Upgrade")]
  [SerializeField] private int shardsToCast = 10;
  [SerializeField] private float shardCastDomainSlow = 1f;
  [SerializeField] private float shardCastDomainDuration = 8f;
  private float spellCastTimer;
  private float spellsPerSecond;

  [Header("Time echo cast Upgrade")]
  [SerializeField] private int echoeToCast = 8;
  [SerializeField] private float echoCastDomainSlow = 6;
  [SerializeField] private float echoCastDomainDuration = 6f;
  [SerializeField] private float healthToRestoreWithEcho = .05f;

  [Header("Domain details")]
  public float maxDomainSize = 15f;
  public float expandSpeed = 3f;

  private List<Enemy> trappedTargets = new List<Enemy>();
  private Transform currentTarget;

  public void CreateDomain()
  {
    spellsPerSecond = GetSpellsToCast() / GetDomainDuration();

    GameObject domain = Instantiate(domainPrefab, transform.position, Quaternion.identity);
    domain.GetComponent<SkillObject_DomainExpansion>().SetupDomain(this);
  }

  public void DoSpellCasting()
  {
    spellCastTimer -= Time.deltaTime;

    if (currentTarget == null)
      currentTarget = FindTargetInDomain();

    if (currentTarget != null && spellCastTimer <= 0)
    {
      CastSpell(currentTarget);
      spellCastTimer = 1f / spellsPerSecond;
      currentTarget = null;
    }
  }

  private void CastSpell(Transform target)
  {
    if (upgradeType == SkillUpgradeType.Domain_EchoSpam)
    {
      Vector3 offset = Random.value < .5f ? new Vector2(1, 0) : new Vector2(-1, 0);

      skillManager.timeEcho.CreateTimeEcho(target.position + offset);
    }

    if (upgradeType == SkillUpgradeType.Domain_ShardSpam)
    {
      skillManager.shard.CreateRawShard(target, true);
    }
    
  }

  private Transform FindTargetInDomain()
  {
    trappedTargets.RemoveAll(target => target == null || target.health.isDead);

    if (trappedTargets.Count == 0)
      return null;

    int randomIndex = Random.Range(0, trappedTargets.Count);
    Transform target = trappedTargets[randomIndex].transform;

    return target;
  }

  public float GetDomainDuration() => upgradeType switch
  {
    SkillUpgradeType.Domain_SlowingDown => slowDownDomainDuration,
    SkillUpgradeType.Domain_ShardSpam => shardCastDomainDuration,
    SkillUpgradeType.Domain_EchoSpam => echoCastDomainDuration,
    _ => 0
  };

  public float GetSlowPercentage() => upgradeType switch
  {
    SkillUpgradeType.Domain_SlowingDown => slowDownPercent,
    SkillUpgradeType.Domain_ShardSpam => shardCastDomainSlow,
    SkillUpgradeType.Domain_EchoSpam => echoCastDomainSlow,
    _ => 0
  };

  private int GetSpellsToCast() => upgradeType switch
  {
    SkillUpgradeType.Domain_ShardSpam => shardsToCast,
    SkillUpgradeType.Domain_EchoSpam => echoeToCast,
    _ => 0
  };

  public bool InstantDomain()
  {
    return upgradeType != SkillUpgradeType.Domain_EchoSpam
        && upgradeType != SkillUpgradeType.Domain_ShardSpam;
  }


  public void AddTarget(Enemy targetToAdd)
  {
    trappedTargets.Add(targetToAdd);
  }

  public void ClearTargets()
  {
    foreach (Enemy enemy in trappedTargets)
      enemy.StopSlowDown();

    trappedTargets = new List<Enemy>();
  }
}
