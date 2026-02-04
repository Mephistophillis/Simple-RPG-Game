using System;
using System.Collections;
using UnityEngine;

public class Skill_Shard : Skill_Base
{
    private SkillObject_Shard currentShard;
    [SerializeField] private GameObject shardPrefab;
    [SerializeField] private float detonateTime = 2;

    [Header("Moving Shard Upgrade")]
    [SerializeField] private float shardSpeed = 7;

    [Header("Multicast Shard Upgrade")]
    [SerializeField] private int maxCharges = 3;
    [SerializeField] private int currentCharges;
    [SerializeField] private bool isRecharging;

    public override void TryUseSkill()
    {
        if (CanUseSkill() == false) return;

        if (Unlocked(SkillUpgradeType.Shard))
            HandleShardRegular();

        if (Unlocked(SkillUpgradeType.Shard_MoveToEnemy))
            HandleShardMoving();

        if (Unlocked(SkillUpgradeType.Shard_Multicast))
            HandleShardMulticast();
    }

    public void CreateShard()
    {
        GameObject shard = Instantiate(shardPrefab, transform.position, Quaternion.identity);

        currentShard = shard.GetComponent<SkillObject_Shard>();
        currentShard.SetupShard(detonateTime);
    }

    private void HandleShardRegular()
    {
        CreateShard();
        SetSkillOnCooldown();
    }

    private void HandleShardMoving()
    {
        CreateShard();
        currentShard.MoveTowardsClosestTarget(shardSpeed);

        SetSkillOnCooldown();
    }

    private void HandleShardMulticast()
    {
        if (currentCharges <= 0) return;

        CreateShard();
        currentShard.MoveTowardsClosestTarget(shardSpeed);
        currentCharges--;

        if (isRecharging == false)
            StartCoroutine(ShardRechargeCo());
    }

    private IEnumerator ShardRechargeCo()
    {
        isRecharging = true;

        while (currentCharges < maxCharges)
        {
            yield return new WaitForSeconds(cooldown);

            currentCharges++;
        }

        isRecharging = false;
    }
}
