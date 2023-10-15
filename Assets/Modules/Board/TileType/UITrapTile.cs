using System.Collections;
using UnityEngine;

public class UITrapTile: BaseTile
{
    public override IEnumerator OnEvent()
    {
        GameManager.I.Log("덫에 빠져 아무것도 할 수 없습니다.");
        yield return new WaitForSeconds(0.5f);
    }
}