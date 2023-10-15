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
    [SerializeField] private BoardManager _boardManager;
    [SerializeField] private CrackManager _crackManager;
    [SerializeField] private UIDice _dice;
    
    [Header("Game Flow Variable")]
    private bool _gameEnd;
    private BaseEnemy _curEnemy;

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
    public BaseTile CurOnCommonTile => _boardManager.PlayerOnTile;

    public BoardManager BoardManager => _boardManager;

    #endregion

    #region Player
    public bool PlayerOnCagel { get; set; }
    
    private int _anigma;
    public int Anigma
    {
        get => _anigma;
        set
        {
            _anigma = value;
            UIManager.I.UpdateAnigmaTxt(_anigma);
        }
    }

    private int _castleMaxHP;
    private int _castleHp;
    public int CastleHp
    {
        get => _castleHp;
        set
        {
            _castleHp = Math.Max(0, Math.Min(value, _castleMaxHP));   
            UIManager.I.UpdateCastleHPTxt(_castleHp, _castleMaxHP);

            if (_castleHp == 0)
            {
                GameOver("성 체력이 0이 됨");
            }
        }
    }

    #endregion
    
    public void B_Start()
    {
        // Init Data
        _crackManager.Init(ResourceManager.I.Envs[(int) EnvType.SlimeForest]);
        _boardManager.Init();

        Anigma = GamePassive.I.StartAnigma;
        _castleMaxHP = GamePassive.I.StartCastleHP;
        CastleHp = _castleMaxHP;
        
        // 게임 시작
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
        if (!PlayerOnCagel)
        {
            // 주사위를 굴린다.
            _dice.Active();
            yield return WaitNext(); // 값을 얻을 때까지 대기

            yield return PlayerMove(_diceVal);
        }
        else
        {
            PlayerOnCagel = false;
            Log("덫에서 빠져나왔습니다. 이동할 힘이 없습니다.", 1.5f);
            yield return new WaitForSeconds(1.5f);
        }
    }

    public IEnumerator PlayerMove(int value)
    {
        // 플레이어 이동
        // 보드를 이동할 땐, 한 칸 한 칸 이동한다.
        // 하나의 애니메이션을 반복한다.
        for (int i = 0; i < value; i++)
        {
            var targetPos = (_boardManager.GetNextTile(true) as MonoBehaviour).transform.position;
            _playerController.Move(targetPos);
            yield return WaitNext();

            // 지나가는 타일에 대한 이벤트 수행 (도달하는 타일 포함)
            _boardManager.PlayerOnTile.PassEvent();
        }
        
        // 최종 이동한 타일에서 플레이어의 행동 대기
        yield return _boardManager.PlayerOnTile.OnEvent();
        _boardManager.PlayerOnTile.ExitEvent();
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
                Log("유닛들이 적을 공격합니다.");
                foreach (var unit in battleUnits)
                {
                    unit.BattleAction(_curEnemy);
                    
                    // 전투 종료
                    if (_curEnemy == null) break;
                }
            }
            
            // 모든 공격을 끝마치면....
            yield return new WaitForSeconds(1f);
            // yield return WaitNext();
            
            if (_curEnemy != null) 
            {
                // 적응 상태의 적 -> 성 공격
                if (_curEnemy?.HP > 0)
                {
                    yield return _curEnemy.Execute();
                }
            }
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
        Log($"게임 클리어: {reason}", 5);
    }
    
    private void GameOver(string reason)
    {
        StopCoroutine(GameFlow());
        Log($"게임 오버: {reason}", 5);
    }

    #region Other

    public void CalculateSynergy()
    {
        
    }

    public void KillMonster()
    {
        Log($"유닛들이 {_curEnemy.Name}을 처치했습니다.", 1.5f);
        _curEnemy = null;
        _crackManager.CheckPurgeEnv();
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

    public void Log(string log, float time = 1f)
    {
        UIManager.I.OutputInfo(log, time);
        Debug.Log(log);
    }
    
    public IEnumerator Timer(int time, Action endAction = null)
    {
        yield return new WaitForSeconds(time);
        endAction?.Invoke();
    }
    #endregion

    public bool CheckCost(int expense)
    {
        return expense <= Anigma;
    }
}
