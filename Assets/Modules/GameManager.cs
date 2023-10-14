using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Manager & Player")]
    [SerializeField] private PlayerController _playerController;
    [SerializeField]private BoardManager _boardManager;
    [SerializeField]private CrackManager _crackManager;
    
    [Header("Game Flow Variable")]
    private bool _gameEnd;
    private IEnemy _curEnemy;

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

    public string CurEnvName => _crackManager.ConnectedEnv.GetType().Name;
    public PlayerController PlayerController => _playerController;
    public UITile CurOnTile => _boardManager.PlayerOnTile;

    public BoardManager BoardManager => _boardManager;

    #endregion
    
    public void B_Start()
    {
        // Init Data
        _crackManager.Init(ResourceManager.I.Envs[(int) EnvType.SlimeForest]);
        _boardManager.Init();
        
        // 
        StartCoroutine(GameFlow());
    }

    IEnumerator GameFlow()
    {
        Debug.Log("게임 시작");
        
        _gameEnd = false;
        do
        {
            yield return RollDice();

            yield return BattleEvent();

            yield return CrackEvent();
            
        } while (!_gameEnd);
    }

    #region Dice Sample
    [Header("Dice")] 
    [SerializeField] private TextMeshProUGUI _diceValTMP;
    private int _diceVal;
    public void B_Roll()
    {
        var text = _diceValTMP.text.Trim();
        if (int.TryParse(text, 
                NumberStyles.Number, 
                CultureInfo.CurrentCulture.NumberFormat,
                out int value))
        {
            Roll(value);
        }
        else
        {
            Roll(1);
        }
    }

    public void Roll(int value)
    {
        _diceVal = value;
        Next();
    }
    #endregion
    IEnumerator RollDice()
    {
        Debug.Log("주사위 눈금을 입력해주세요.");
        // 주사위를 굴린다.
        
        yield return WaitNext(); // 값을 얻을 때까지 대기

        // 플레이어 이동
        // 보드를 이동할 땐, 한 칸 한 칸 이동한다.
        // 하나의 애니메이션을 반복한다.
        for (int i = 0; i < _diceVal; i++)
        {
            var targetPos = _boardManager.GetNextTile(true).transform.position;
            _playerController.Move(targetPos);
            yield return WaitNext();
        }
        
        // 최종 이동한 타일에서 플레이어의 행동 대기
        UIManager.I.ActivePlayerActionSelector();
        yield return WaitNext();
        
        UIManager.I.DisabledPlayerActionSelector();
    }

    private void MovePlayer()
    {
        
    }
    
    IEnumerator BattleEvent()
    {
        if (_curEnemy != null)
        {
            Debug.Log("적이 존재하여 전투 시작");
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
            if (_curEnemy.HP > 0)
            {
                yield return _curEnemy.Execute();
            }
            else
            {
                _curEnemy.Destroy();
                _curEnemy = null;
            }
        
            // 모든 공격을 끝마치면....
            yield return WaitNext();
        }
    }

    IEnumerator CrackEvent()
    {
        // 몬스터가 존재하지 않을 때, 크랙 이벤트 수행
        if (_curEnemy == null)
        {
            Debug.Log("균열 내 몬스터가 미존해야하여 균열 행동");
            _curEnemy = _crackManager.Execute();
        }

        yield return null;
    }

    public void GameClear(string reason)
    {
        StopCoroutine(GameFlow());
        Debug.Log($"게임 클리어: {reason}");
    }

    #region Other

    public void CalculateSynergy()
    {
        
    }
    

    #endregion
    #region Util
    private bool _next;
    public IEnumerator WaitNext()
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

    public bool CheckCost(int expense)
    {
        return expense <= _playerController.Enigma;
    }
}
