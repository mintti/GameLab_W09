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
                GameManager.I.BlockadeCrack();
            }
        }
    }

    private IEnv _connectedEnv;

    private int _monsterIdx;
    private int _emptyCount;

    /// <summary>
    /// 균열에 재연결 
    /// </summary>
    /// <param name="env"></param>
    public void Init(IEnv env)
    {
        // 환경 설정 애니메이션?
        
        _monsterIdx = 0;
        _connectedEnv = env;
    }
    
    /// <summary>
    /// 현재 몬스터가 생성되지 않은 상태이면 게임 매니저에서 호출된다
    /// </summary>
    public IEnumerable Execute()
    {
        _emptyCount++;

        if (_emptyCount == _connectedEnv.MonsterSpawnInterval)
        {
            SpawnMonster();
            _emptyCount = 0;
        }

        yield return null;
    }
    
    private void SpawnMonster()
    {
        if (_monsterIdx <= _connectedEnv.MonsterIdxList.Count)
        {
            if (_monsterIdx < _connectedEnv.MonsterIdxList.Count)
            {
                int monsterIdx = _connectedEnv.MonsterIdxList[_monsterIdx];
                // monsterIdx에 해당하는 몬스터 설정
            }
            else
            {
                // 보스 설정
                // 보스 이펙트?
            }
            
            // 소환
        }
        else
        {
            ClearEnv();
        }
    }

    /// <summary>
    /// 현재 지정된 환경에서 모든 전투를 끝마침
    /// </summary>
    private void ClearEnv()
    {
        // 한 레벨 높은 지역 정보 랜덤으로 읽음
        IEnv nextEnv = null;
        
        // 현재 지역으로 지정
        Init(nextEnv);
    }
}