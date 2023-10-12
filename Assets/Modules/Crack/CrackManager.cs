using System;
using System.Collections;
using System.Linq;

public class CrackManager
{
    private float _blockadeGauge;
    public float BlockadeGauge
    {
        get => _blockadeGauge;
        set
        {
            _blockadeGauge = Math.Min(value, 100f);

            if (_blockadeGauge == 100)
            {
                GameManager.I.GameClear("연구를 통한 균열 봉쇄");
            }
        }
    }

    public IEnv ConnectedEnv { get; private set; }

    private int _enemyIdx;
    private int _emptyCount;

    /// <summary>
    /// 균열에 재연결 
    /// </summary>
    /// <param name="env"></param>
    public void Init(IEnv env)
    {
        // 환경 설정 애니메이션?
        
        _enemyIdx = 0;
        ConnectedEnv = env;
    }
    
    /// <summary>
    /// 현재 몬스터가 생성되지 않은 상태이면 게임 매니저에서 호출된다
    /// </summary>
    public IEnumerable Execute()
    {
        _emptyCount++;

        if (_emptyCount == ConnectedEnv.EnemySpawnInterval)
        {
            SpawnEnemy();
            _emptyCount = 0;
        }

        yield return null;
    }
    
    private void SpawnEnemy()
    {
        IEnemy enemy = null;
        if (_enemyIdx <= ConnectedEnv.EnemyTypeList.Count)
        {
            if (_enemyIdx < ConnectedEnv.EnemyTypeList.Count)
            {
                var enemyType = ConnectedEnv.EnemyTypeList[_enemyIdx];
                enemy = ResourceManager.I.GetEnemy(enemyType);
            }
            else
            {
                enemy = ResourceManager.I.GetBoss(ConnectedEnv.BossType);
                // 보스 이펙트?
            }
            
            // [TODO] 소환
        }
        else
        {
            PurgeCompletedEnv();
        }
    }

    /// <summary>
    /// 현재 지정된 환경에서 모든 전투를 끝마침
    /// </summary>
    private void PurgeCompletedEnv()
    {
        if (ConnectedEnv.BossType == BossType.DemonKing)
        {
            GameManager.I.GameClear("보스를 잡아 균열 봉쇄");
        }
        else
        {
            int nextLevel = ConnectedEnv.Level + 1;
        
            // 한 레벨 높은 지역 정보 랜덤으로 지정
            var envsByLevel = ResourceManager.I.Envs.Where(x => x.Level == nextLevel).ToList();
            IEnv nextEnv = envsByLevel[UnityEngine.Random.Range(0, envsByLevel.Count)];
        
            // 현재 지역으로 지정
            Init(nextEnv);
        }
    }
}