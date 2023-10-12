using System.Collections.Generic;

public class Wizard : IUnit
{
    public ClassType ClassType { get; set; }
    public UnitLevel Level { get; set; }
    public List<UnitType> HasType { get; set; }
    public int CycleExpense { get; set; }
    public void Init()
    {
        throw new System.NotImplementedException();
    }

    public void BattleAction()
    {
        throw new System.NotImplementedException();
    }
}