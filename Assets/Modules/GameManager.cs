using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Manager & Player")]
    private PlayerController _playerControllerController;
    private BoardManager _boardManager;
    private UnitManager _unitManager;
    private CrackManager _crackManager;
    
    [Header("Game Flow Variable")]
    private bool _gameEnd;
    private BaseEnemy _curBaseEnemy;

    #region Singleton
    public static GameManager I { get; private set; }
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

    #region Quick

    public string CurEnvName => ((object)_crackManager.ConnectedEnv).GetType().Name;
    

    #endregion
    public void ClickStart()
    {
        // Init Data
        _crackManager.Init(ResourceManager.I.Envs[(int) EnvType.SlimeForest]);

        // 
        StartCoroutine(GameFlow());
    }

    IEnumerator GameFlow()
    {
        _gameEnd = false;
        do
        {
            yield return RollDice();

            yield return BattleEvent();

            yield return CrackEvent();
            
        } while (_gameEnd);
    }

    IEnumerator RollDice()
    {
        // 주사위를 굴린다.
        
        yield return WaitNext(); // 값을 얻을 때까지 대기
        
        // 플레이어 이동
        // 보드를 이동할 땐, 한 칸 한 칸 이동한다.
        // 하나의 애니메이션을 반복한다.
        
        // 플레이어의 행동 대기
        yield return WaitNext();
    }
    
    IEnumerator BattleEvent()
    {
        if (_curBaseEnemy != null)
        {
            // 전투 용병 체크 후 공격
            var battleUnits = _boardManager.SortedOnUnitTiles.Where(x => x.HasType.Contains(UnitType.Battle));
            if (battleUnits.Count() > 0)
            {
                foreach (var unit in battleUnits)
                {
                    unit.BattleAction();
                }
            }
            
            // 모든 공격을 끝마치면....
            yield return WaitNext();

            // 적응 상태의 적 -> 성 공격
            if (_curBaseEnemy.HP > 0)
            {
                yield return _curBaseEnemy.Execute();
            }
            else
            {
                _curBaseEnemy.Destroy();
                _curBaseEnemy = null;
            }
        
            // 모든 공격을 끝마치면....
            yield return WaitNext();
        }
    }

    IEnumerator CrackEvent()
    {
        // 몬스터가 존재하지 않을 때, 크랙 이벤트 수행
        if (_curBaseEnemy == null)
        {
            yield return _crackManager.Execute();
        }
    }

    public void GameClear(string reason)
    {
        StopCoroutine(GameFlow());
        Debug.Log($"게임 클리어: {reason}");
    }

    #region Util
    private bool _next;
    public IEnumerable WaitNext()
    {
        _next = false;
        yield return new WaitUntil(() => _next);
    }

    public void Next()
    {
        _next = true;
    }

    public bool Record()
    {
        return false;
    }
    #endregion
}
