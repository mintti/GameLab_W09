using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class UIDice : MonoBehaviour
{
    [SerializeField] private RectTransform _graduationRect;
    [SerializeField] private TextMeshProUGUI _resultTMP;
    [Header("Control")]
    [SerializeField] private RectTransform _arrowRect;
    [SerializeField] private float duration;

    private float _curParentWidth;
    
    public void Active()
    {
        gameObject.SetActive(true);
        ResetRoller();
    }

    public void B_RollingStart()
    {
        ResetRoller();
        _curParentWidth = _graduationRect.sizeDelta.x;
        float max = _curParentWidth / 2;

        _arrowRect.DOLocalMoveX(max, duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear);
    }

    /// <summary>
    /// 화살표 위치 맨 앞으로 초기화
    /// </summary>
    private void ResetRoller()
    {
        _resultTMP.text = string.Empty;
        
        var newPos = _arrowRect.localPosition;
        newPos.x = -(_graduationRect.sizeDelta.x / 2);
        _arrowRect.localPosition = newPos;
    }

    public void B_RollingEnd()
    {
        _arrowRect.transform.DOKill();
        var value = (int)(_arrowRect.localPosition.x + _curParentWidth / 2) / 50 + 1;
        _resultTMP.text = $"{value}";

        Action endAction = () =>
        {
            gameObject.SetActive(false);
            GameManager.I.Roll(value);
        };
        StartCoroutine(GameManager.I.Timer(1, endAction ));
    }
}