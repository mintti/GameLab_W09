using System.Collections;
using UnityEngine;

public class UIRestTile : BaseTile
{
    public override IEnumerator OnEvent()
    {
        // [TODO] 손님 이벤트
        GameManager.I.Next();
        yield return null;
    }
    
    public override void PassEvent()
    {
        // [TODO] 손님의 인사   
    }
}