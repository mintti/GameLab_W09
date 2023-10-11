using static Util;
public class UIResearch : IPlayerAction
{
    public void Selected()
    {
        
    }

    public void End()
    {
        GameManager.I.Next();
    }
}