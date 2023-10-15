using System.Collections;
using UnityEngine;

public class UIRestTile : BaseTile
{
    public override IEnumerator OnEvent()
    {
        GameManager.I.Log("(접객실은 비어있다.)");
        GameManager.I.Next();
        yield return new WaitForSeconds(.5f);
    }
    
    public override void PassEvent()
    {
        // [TODO] 손님의 인사   
    }
}