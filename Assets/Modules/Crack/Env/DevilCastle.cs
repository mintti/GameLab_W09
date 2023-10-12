using System.Collections.Generic;

public class DevilCastle : IEnv
{
    public int Level { get; set; }
    public List<int> MonsterIdxList { get; set; }
    public int MonsterSpawnInterval { get; set; }
    public int BossIdx { get; set; }
    public void Effect()
    {
        
    }
}