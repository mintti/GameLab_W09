using UnityEngine;

public class UITrapTile: MonoBehaviour, ITile
{
    #region ITile
    public int Index { get; set; }
    public TileType Type { get; set; }
    public void OnEvent()
    {
        GameManager.I.Log("덫에 빠져 아무것도 할 수 없습니다.");
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