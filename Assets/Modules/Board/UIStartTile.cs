using UnityEngine;

public class UIStartTile : MonoBehaviour, ITile
{
    #region ITile
    public int Index { get; set; }
    public TileType Type { get; set; }
    
    public void OnEvent()
    {
        GameManager.I.Next();
    }

    public void ExitEvent()
    {
        
    }

    public void PassEvent()
    {
        int getAnigma = GamePassive.I.StartPassGetAnigma;
        GameManager.I.Anigma += getAnigma;
        GameManager.I.Log($"{getAnigma} 아니그마를 얻었습니다.");
    }
    #endregion
}