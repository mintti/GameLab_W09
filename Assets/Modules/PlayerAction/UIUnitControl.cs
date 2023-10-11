using static Util;
public class UIUnitControl : IPlayerAction
{
    #region IPlayerAction
    public void Selected()
    {
        
    }

    public void End()
    {
        GameManager.I.Next();
    }
    #endregion

    public void Hire(UnitType type)
    {
        
    }


    public void Upgrade()
    {
        
    }

    public void ChangeType(UnitType type)
    {
        
    }
}