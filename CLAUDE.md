# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

Unity 2D action RPG built with Unity 6000.3.0f1. Uses a **Hierarchical State Machine** pattern with component-based entity design.

## Architecture

### State Machine Hierarchy

```
EntityState (abstract base)
├── PlayerState → 11 player states (Idle, Move, Jump, Fall, Dash, WallSlide, WallJump, BasicAttack, JumpAttack, CounterAttack, Dead)
└── EnemyState → 6 enemy states (Idle, Move, Battle, Attack, Stunned, Dead)
```

- `StateMachine.cs` - Manages active state and transitions
- `EntityState.cs` - Base class with timer and animation sync
- States use `stateTimer` for time-based transitions

### Entity System

All actors inherit from `Entity` MonoBehaviour:
- `Entity.cs` - Physics, collision detection (ground/walls via raycasts), facing direction, knockback
- `Entity_Stats.cs` - RPG stat calculations with modifiers, crit system, elemental damage selection
- `Entity_Health.cs` - Damage reduction (armor caps at 85%, elemental resistance at 75%), evasion, health regen
- `Entity_Combat.cs` - Attack execution, target detection via `IDamagable` interface
- `Entity_StatusHandler.cs` - Elemental effects: Chill (slow), Burn (DoT), Electrify (charge buildup)

### Stat System

```
Stat (individual with baseValue + modifiers list)
├── Stat_ResourceGroup (maxHealth, healthRegen)
├── Stat_MajorGroup (strength, agility, intelligence, vitality)
├── Stat_OffensiveGroup (damage, attackSpeed, crit stats, elemental damages)
└── Stat_DefenseGroup (armor, evasion, elemental resistances)
```

Configured via `Stat_SetupSO` ScriptableObjects in `Assets/Data/`.

### Input System

Uses Unity's New Input System with `PlayerInputSet` generated class:
- Movement (Vector2), Jump, Dash, Attack, CounterAttack

## Key Entry Points

| System | File |
|--------|------|
| Player controller | `Assets/Scripts/Player/Player.cs` |
| Enemy controller | `Assets/Scripts/Enemy/Enemy.cs` |
| State machine core | `Assets/Scripts/StateMachine/` |
| Combat logic | `Assets/Scripts/Entity/Entity_Combat.cs` |
| Damage/health | `Assets/Scripts/Entity/Entity_Health.cs` |
| Stats/calculations | `Assets/Scripts/Entity/Entity_Stats.cs` |

## Adding New Content

**New player/enemy state**: Create class inheriting from `PlayerState`/`EnemyState`, implement `Enter()`, `Update()`, `Exit()` methods, add animator bool parameter matching state name.

**New stat**: Add to appropriate `Stat_*Group.cs`, register in `Entity_Stats.cs`, add to `StatType.cs` enum.

**New status effect**: Extend `Entity_StatusHandler.cs` with coroutine pattern.

## Known Issues

- Typos in `StatType.cs`: `HelthRegen` (should be HealthRegen), `Intelegence` (should be Intelligence)
- Russian TODO comments in `Entity_Stats.GetElementalDamage()` and `Entity_StatusHandler.DoLightningStrike()` indicate areas needing refactoring

## Debugging

Scene gizmos show:
- Red lines: ground/wall detection raycasts
- Yellow: player detection range (enemies)
- Blue: attack range
