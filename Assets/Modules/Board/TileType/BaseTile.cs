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

    public virtual void ExitEvent()
    {
        
    }

    public virtual void PassEvent()
    {
        
    }
}