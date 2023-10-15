using System.Collections;
using UnityEngine;

public class UITrapTile: BaseTile
{
    public override IEnumerator OnEvent()
    {
        GameManager.I.PlayerOnCagel = true;
        GameManager.I.Log("덫에 빠져 아무것도 할 수 없습니다. (다음 턴 행동 불가)", 1.5f);
        yield return new WaitForSeconds(1.5f);
    }
}