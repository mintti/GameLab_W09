using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIEnemyInfo : MonoBehaviour
{
    [Header("Enemy Info")]
    [SerializeField] private TextMeshProUGUI _nameTMP;
    [SerializeField] private TextMeshProUGUI _hpTMP;
    [SerializeField] private RectTransform _maxHPRect;
    [SerializeField] private RectTransform _curHPRect;
    
    public void Active(string name, int hp)
    {
        gameObject.SetActive(true);
        _nameTMP.text = name;
        UpdateHP(hp, hp);
    }

    public void UpdateHP(int hp, int maxHp)
    {
        _hpTMP.text = $"{hp}/{maxHp}";
        
        _maxHPRect.sizeDelta = new Vector2(10 * maxHp, _maxHPRect.sizeDelta.y);
        _curHPRect.sizeDelta = new Vector2((10 * hp) - 10, _curHPRect.sizeDelta.y);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
