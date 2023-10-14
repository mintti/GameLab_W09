using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : IUnit
{
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

    public BaseUnit(ClassType type, List<UnitType> hasType)
    {
        _classType = type;
        _hasType = hasType;
    }
    
    public void Init(UnitLevel level)
    {
        Level = level;
        UIUnit.ChangedLevel(_level);
    }

    public virtual void BattleAction()
    {
        
    }
}