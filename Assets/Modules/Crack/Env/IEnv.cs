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
    int Level { get; set; }
    
    /// <summary>
    /// 출현 몬스터 인덱스
    /// </summary>
    List<int> MonsterIdxList { get; set; }
    
    /// <summary>
    /// 몬스터 생성 간격
    /// </summary>
    int MonsterSpawnInterval { get; set; }
    
    /// <summary>
    /// 보스 인덱스
    /// </summary>
    int BossIdx { get; set; }

    /// <summary>
    /// 환경 효과
    /// </summary>
    void Effect();
}