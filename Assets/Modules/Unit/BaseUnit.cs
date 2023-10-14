using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class BaseUnit : IUnit
{
    public BaseUnit(ClassType type, List<UnitType> hasType)
    {
        _classType = type;
        _hasType = hasType;
    }
    
    public UIUnit UIUnit { get; set; }

    protected ClassType _classType;
    public ClassType ClassType 
    { 
        get => _classType;
        set
        {
            _classType = value;
            UIUnit.ChangedClassType(_classType);
            GameManager.I.CalculateSynergy();
        }
    }
    
    private UnitLevel _level;
    public UnitLevel Level
    {
        get => _level;
        set
        {
            if (UnitLevel.Ignore < value && value <= UnitLevel.Three)
            {
                _level = value;
                UIUnit.ChangedLevel(_level);
            }
        }
    }

    protected List<UnitType> _hasType;
    public List<UnitType> HasType 
    { 
        get => _hasType;
        set => _hasType = value;
    }
    
    public int CycleExpense { get; set; }
    
    public void Init(UnitLevel level)
    {
        Level = level;
        UIUnit.ChangedLevel(_level);
        UpdateStatus();
    }

    /// <summary>
    /// 호출 시 Where절로 인해, Battle 특성을 가진 유닛에서만 호출됨
    /// </summary>
    /// <param name="enemy">대상</param>
    public virtual void BattleAction(IEnemy enemy)
    {
        // nothing
    }

    public virtual void UpdateStatus()
    {
        
    }
}