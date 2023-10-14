using UnityEngine;

public class UIPortalTile: MonoBehaviour, ITile
{
    #region ITile
    public int Index { get; set; }
    public TileType Type { get; set; }
    public void OnEvent()
    {
        // [TODO] 타일을 지정하여 이동할 수 있음
        GameManager.I.Next();
    }

    public void ExitEvent()
    {
        
    }

    public void PassEvent()
    {
        
    }
    #endregion
}