using UnityEngine;

public class UIRestTile : MonoBehaviour, ITile
{
    #region ITile
    public int Index { get; set; }
    public TileType Type { get; set; }
    public void OnEvent()
    {
        // [TODO] 손님 이벤트
        GameManager.I.Next();
    }

    public void ExitEvent()
    {
        
    }

    public void PassEvent()
    {
        // [TODO] 손님의 인사   
    }
    #endregion
}