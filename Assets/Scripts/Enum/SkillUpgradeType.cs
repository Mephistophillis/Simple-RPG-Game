using UnityEngine;

public enum SkillUpgradeType
{
    None,
    // --- Dash Tree ---
    Dash, // Dash to avoid damage
    Dash_CloneOnStart, // Create a clone when dash starts
    Dash_CloneOnStartAndArrival, // Create a clone when dash starts and ends
    Dash_ShardOnStart, // Create a shard when dash starts
    Dash_ShardOnStartAndArrival, // Create a shard when starts and ends

    // --- Shard Tree ---
    Shard, // The shard explodes when touched by an enemy (?) or time goes up
    Shard_MoveToEnemy, // Shard will move towards nearest enemy
    Shard_Multicast, // Shard ability can have up to N charges. You can cast them all in a raw
    Shard_Teleport, // You can swap places with the last shard you created
    Shard_TeleportHpRewind, // When you swap places with shard, your HP % is same as it was when you created shard

    // --- Sword Tree ---
    SwordThrow, // You can throw sword to damage enemies from range
    SwordThrow_Spin, // Your sword will spin at one point and gamage enemies. Like a chainsaw
    SwordThrow_Pierce, // Pierce sword will pierce N targets
    SwordThrow_Bounce, // Bounce sword will bounce between enemies

    // --- Time Echo ---
    TimeEcho, // Create a clone of a player. It can take damage from enemies
    TimeEcho_SingleAttack, // Time Echo can perform a single attack
    TimeEcho_MultiAttack, // Time Echo can perform N attaks
    TimeEcho_ChanceToDuplicate, // Time Echo has a chance to create another time echo when attaks

    TimeEcho_HealWisp, // When time echo it creates a wisp that flies towards the player to heal it.
                       // Heal is = to percentage of damage taken when died
    TimeEcho_CleanseWisp, // Wisp will now remove negative effects from player
    TimeEcho_CooldownWisp, // Wisp will reduce cooldown of all skills by N seconds
}
