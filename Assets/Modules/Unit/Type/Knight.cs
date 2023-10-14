using System.Collections.Generic;

public class Knight : BaseUnit
{
    public int Power;
    public int Defense;
    
    public Knight(ClassType type, List<UnitType> hasType) : base(type, hasType)
    {
    }
    
    public override void UpdateStatus()
    {
        var status = ResourceManager.I.GetKnightStatus(Level);
        Power = status.power;
        Defense = status.defense;
    }

    public override void BattleAction(IEnemy enemy)
    {
        enemy.Hit(Power);
    }
}