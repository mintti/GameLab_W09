using System.Collections.Generic;

public class Wizard : BaseUnit
{
    public int Power;
    
    public Wizard(ClassType type, List<UnitType> hasType) : base(type, hasType)
    {
        UpdateStatus();
    }
    
    public override void UpdateStatus()
    {
        Power = ResourceManager.I.GetWizardStatus(Level);
    }

    public override void BattleAction(IEnemy enemy)
    {
        enemy.Hit(Power);
    }
}