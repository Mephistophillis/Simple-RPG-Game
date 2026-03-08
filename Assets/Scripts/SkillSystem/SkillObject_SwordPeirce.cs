using UnityEngine;

public class SkillObject_SwordPeirce : SkillObject_Sword
{
    private int amountToPierce;

    public override void SetupSword(Skill_SwordThrow skillManager, Vector2 direction)
    {
        base.SetupSword(skillManager, direction);
        amountToPierce = skillManager.pierceAmount;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        bool groundHit = collision.gameObject.layer == LayerMask.NameToLayer("Ground");
        float radius = .3f;

        if (amountToPierce <= 0 || groundHit)
        {
            DamageEnemiesInRadius(transform, radius);
            StopSword(collision);
            return;
        }

        amountToPierce--;
        DamageEnemiesInRadius(transform, radius);
    }
}
