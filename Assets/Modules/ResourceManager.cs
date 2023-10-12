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


    public List<IEnv> Envs { get; private set; }

    public void Init()
    {
        
    }

    private void InitEnv()
    {
        Envs = new()
        {
            null,
            new SlimeForest(1, new () { EnemyType.Slime ,EnemyType.AnimalSlime,EnemyType.SteelSlime}, 2, BossType.SlimeKing ),
            new DevilCastle(2, new () { EnemyType.SmallDemon, EnemyType.MediumDemon, EnemyType.LargeDemon}, 2, BossType.DemonKing ),
        };
    }

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
    
    public IEnemy GetEnemy(EnemyType type)
    {
        return type switch
        {
            EnemyType.Slime => new Slime(nameof(Slime), 5, 2),
            EnemyType.AnimalSlime => new AnimalSlime(nameof(AnimalSlime), 5, 2),
            EnemyType.SteelSlime => new SteelSlime(nameof(SteelSlime), 5, 2),
            EnemyType.SmallDemon => new SmallDemon(nameof(SmallDemon), 5, 2),
            EnemyType.MediumDemon => new MediumDemon(nameof(MediumDemon), 5, 2),
            EnemyType.LargeDemon => new LargeDemon(nameof(LargeDemon), 5, 2),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    public IEnemy GetBoss(BossType type)
    {
        return type switch
        {
            BossType.SlimeKing => new SlimeKing(nameof(SlimeKing), 10, 3),
            BossType.DemonKing => new DemonKing(nameof(DemonKing), 10, 3),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}