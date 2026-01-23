using UnityEngine;

public class Skill_Base : MonoBehaviour
{
    [Header("General details")]
    [SerializeField] private float cooldown;
    private float lastTimeUsed;

    protected virtual void Awake()
    {
        lastTimeUsed -= cooldown;
    }

    public bool CanUseSkill()
    {
        if (OnCooldown())
        {
            Debug.LogWarning("On Cooldown");
            return false;
        }

        return true;
    }

    private bool OnCooldown() => Time.time < lastTimeUsed + cooldown;
    public void SetSkillOnCooldown() => lastTimeUsed = Time.time;
    public void ResetCooldownBy(float cooldownReduction) => lastTimeUsed += cooldownReduction;
    public void ResetCooldown() => lastTimeUsed = Time.time;

}
