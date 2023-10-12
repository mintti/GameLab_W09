using System.Collections.Generic;

public abstract class BaseEnv : IEnv
{
    public BaseEnv(int level, List<EnemyType> enemyTypes, int interval, BossType bossType)
    {
        Level = level;
        EnemyTypeList = enemyTypes;
        EnemySpawnInterval = interval;
        BossType = bossType;
    }

    public int Level { get; }
    public List<EnemyType> EnemyTypeList { get;  }
    public int EnemySpawnInterval { get; }
    
    public BossType BossType { get; }
    
    public abstract void Effect();

    public void PurgeCompleted()
    {
        
    }
}