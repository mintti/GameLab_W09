using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class CrackManager : MonoBehaviour
{
    public float BlockadeGauge => GamePassive.I.BlockadeGauge;
    
    public IEnv ConnectedEnv { get; private set; }

    private int _enemyIdx;
    private int _emptyCount;

    [Header("Enemy")]
    [SerializeField] private SpriteRenderer enemySpriteRenderer;
    
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
    public IEnemy Execute()
    {
        IEnemy enemy = null;
        // [TODO] 보스 몹 잡은 이후, 봉쇄하도록 설정 필요
        _emptyCount++;

        if (_emptyCount == ConnectedEnv.EnemySpawnInterval)
        {
            enemy = SpawnEnemy();
            _emptyCount = 0;
        }

        return enemy;
    }
    
    private IEnemy SpawnEnemy()
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
            
            Debug.Log($"{enemy.Name} 출현! ");
            enemy.DisplaySpriteRenderer = enemySpriteRenderer; // 이미지 출력 연결
        }
        else
        {
            // [TODO] 다른 곳에서 해당 함수 실행하도록 이동필요
            PurgeCompletedEnv();
        }

        return enemy;
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