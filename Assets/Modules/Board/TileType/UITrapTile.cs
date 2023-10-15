using System.Collections;
using UnityEngine;

public class UITrapTile: BaseTile
{
    public override IEnumerator OnEvent()
    {
        GameManager.I.PlayerOnCagel = true;
        GameManager.I.Log("덫에 빠져 아무것도 할 수 없습니다. (다음 턴 행동 불가)");
        yield return new WaitForSeconds(0.5f);
    }
}