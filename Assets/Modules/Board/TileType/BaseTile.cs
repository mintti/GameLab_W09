using System.Collections;
using UnityEngine;
public abstract class BaseTile : MonoBehaviour
{
    public virtual int Index { get; set; }
    public virtual TileType Type { get; set; }
    public virtual IEnumerator OnEvent()
    {
        yield return null;
    }
    
    /// <summary>
    /// OnEvent 수행 수, 실행되는 이벤트
    /// </summary>
    public virtual void ExitEvent()
    {
        
    }

    public virtual void PassEvent()
    {
        
    }

    private void OnMouseUp()
    {
        GameManager.I.BoardManager.ClickTile = this;
    }
}