using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager I;
    public void Awake()
    {
        if (I == null)
        {
            I = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
    
    [Header("Common Panel")]
    [SerializeField] private GameObject _infoTxtBoxObj;
    #region Method
    public void ActiveInfoTxt(string infoTxt)
    {
        _infoTxtBoxObj.SetActive(true);
        _infoTxtBoxObj.GetComponentInChildren<TextMeshProUGUI>().text = infoTxt;
    }
    
    public void DisabledInfoTxt()
    {
        _infoTxtBoxObj.SetActive(false);
    }
    #endregion
    
    
    [Header("Player Info Panel")]
    [SerializeField] private TextMeshProUGUI _anigmaTMP;
    [SerializeField] private TextMeshProUGUI _castleTMP;
    #region Method
    public void UpdateAnigmaTxt(int value) => _anigmaTMP.text = $"Anigma: {value}";
    public void UpdateCastleHPTxt(int value, int maxVal) => _castleTMP.text = $"Castle HP: {value}/{maxVal}";
    #endregion

    [Header("Player Behavior")]
    [SerializeField] private GameObject _playerActionSelectorObj;

    #region Method
    public void ActivePlayerActionSelector() => _playerActionSelectorObj.SetActive(true);
    public void DisabledPlayerActionSelector() => _playerActionSelectorObj.SetActive(false);
    #endregion
    [Header("Crack/Monster Info")]
    [SerializeField] private TextMeshProUGUI _next;
    [SerializeField] private UIEnemyInfo _enemyInfo;
    public UIEnemyInfo EnemyInfo => _enemyInfo;
    #region Method

    

    #endregion
    
    [Header("Unit Info")]
    [SerializeField] private TextMeshProUGUI _synergyTxt;
    #region Method
    
    

    #endregion

    #region Gmae Info 

    public GameObject infoTextObj;
    public TextMeshProUGUI infoTMP;
    private Coroutine currentMessageCoroutine;

    /// <summary>
    /// 입력된 시간만큼 텍스트를 띄웁니다.
    /// </summary>
    public void OutputInfo(string text, float time = 1f)
    {
        if (currentMessageCoroutine != null)
        {
            StopCoroutine(currentMessageCoroutine); // Stop the previous coroutine
        }

        currentMessageCoroutine = StartCoroutine(Message(text, time - .1f));
    }

    IEnumerator Message(string text, float time)
    {
        var _timer = time;
        
        do
        {
            infoTextObj.SetActive(true);
            infoTMP.text = text;
            yield return new WaitForSeconds(0.1f);
            _timer -= .1f;
        } while (_timer > 0);
        
        
        infoTextObj.SetActive(false);
    }

    #endregion

}
