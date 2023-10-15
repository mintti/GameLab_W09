using UnityEngine;

public class UIStartTile : BaseTile
{
    public override void PassEvent()
    {
        //재화 획득
        int getAnigma = GamePassive.I.StartPassGetAnigma;
        GameManager.I.Anigma += getAnigma;
        GameManager.I.Log($"[시작 지점 효과] {getAnigma} 아니그마를 얻었습니다.");
        
        // 새로운 손님 맞이
    }
}