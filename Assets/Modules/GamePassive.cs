using System;
using UnityEngine;

public class GamePassive :  MonoBehaviour
{
    public static GamePassive I;
    public void Awake()
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

    // Research
    [Header("Crack")] 
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

    private int _crackMonsterWeakeningLevel;

    public int CrackMonsterWeakeningLevel
    {
        get => _crackMonsterWeakeningLevel;
        set => _crackMonsterWeakeningLevel = value;
    }
    
    [Header("Castle")]
    private int _dice;
    
    [Header("Unit")]
    private int _knightSkillLvl;
    private int _wizardSkillLvl;
    private int _managerSkillLvl;
    private int _treasureHunterSkillLvl;
    private int _bardSkillLvl;

    // Unit Count Synergy
    private int _knightCnt;
    private int _wizardCnt;
    private int _managerCnt;
    private int _treasureHunterCnt;
    private int _bardCnt;
    
}