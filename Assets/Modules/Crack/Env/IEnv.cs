using System.Collections.Generic;

public enum EnvType : int
{
    Ignore,
    SlimeForest,
    DevilCastle
}

public interface IEnv
{
    /// <summary>
    /// 난이도
    /// </summary>
    int Level { get;  }
    
    /// <summary>
    /// 출현 몬스터 인덱스
    /// </summary>
    List<EnemyType> EnemyTypeList { get; }
    
    /// <summary>
    /// 몬스터 생성 간격
    /// </summary>
    int EnemySpawnInterval { get; }
    
    /// <summary>
    /// 보스 인덱스
    /// </summary>
    BossType BossType { get; }

    /// <summary>
    /// 환경 효과
    /// </summary>
    void Effect();

    void PurgeCompleted();
}