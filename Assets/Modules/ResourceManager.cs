using System;
using System.Collections.Generic;
using UnityEngine;
public class ResourceManager : MonoBehaviour
{
    #region Singleton
    public static ResourceManager I;

    private void Awake()
    {
        if (I == null)
        {
            I = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion


    public IUnit GetUnit(ClassType type)
    {
        return type switch
        {
            ClassType.Knight => new Knight(),
            ClassType.Wizard => new Wizard(),
            ClassType.Manager => new Manager(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
    
    public IEnv GetEnv(EnvType type)
    {
        return type switch
        {
            EnvType.SlimeForest => new SlimeForest(),
            EnvType.DevilCastle => new DevilCastle(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
    public IEnemy GetEnemy(EnemyType type)
    {
        return type switch
        {
            EnemyType.Slime => new Slime(),
            EnemyType.AnimalSlime => new AnimalSlime(),
            EnemyType.SteelSlime => new SteelSlime(),
            EnemyType.SmallDemon => new SmallDemon(),
            EnemyType.MediumDemon => new MediumDemon(),
            EnemyType.LargeDemon => new LargeDemon(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    public IEnemy GetBoss(BossType type)
    {
        return type switch
        {
            BossType.SlimeKing => new SlimeKing(),
            BossType.DemonKing => new DemonKing(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}