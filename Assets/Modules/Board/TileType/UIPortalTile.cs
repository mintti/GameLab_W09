using System.Collections;
using UnityEngine;

public class UIPortalTile: BaseTile 
{
    public override IEnumerator OnEvent()
    {
        GameManager.I.Log("마법의 힘으로 지정한 타일로 이동이 가능하다.");

        GameManager.I.BoardManager.ClickTile = null;
        yield return new WaitUntil(()=> GameManager.I.BoardManager.ClickTile != null);

        int moveValue = GameManager.I.BoardManager.ClickTile.Index - GameManager.I.BoardManager.PlayerOnTile.Index;

        if (moveValue < 0)
            moveValue += GameManager.I.BoardManager.Tiles.Count;
        
        yield return GameManager.I.PlayerMove(moveValue);
    }

    public override void ExitEvent()
    {
        base.ExitEvent();
    }
} 