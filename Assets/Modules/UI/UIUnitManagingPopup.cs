using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIUnitManagingPopup : MonoBehaviour
{
    enum State
    {
        Ignore,
        Empoly,
        Change
    }
    
    [Header("[Fix] Panel")] 
    [SerializeField] private GameObject _emptyTileObj;
    [SerializeField] private GameObject _onUnitTileObj;
    [SerializeField] private GameObject _selectClassContentTileObj;
    [SerializeField] private Button _upgradeBtn;
    

    [Header("Info")]
    [SerializeField] private State _popupState;
    [FormerlySerializedAs("_curTile")] [SerializeField] private UICommonTile curCommonTile;
    
    public void B_ActivePanel()
    {
        var commonTile = (GameManager.I.CurOnCommonTile as MonoBehaviour).GetComponent<UICommonTile>();
        curCommonTile = commonTile;
        gameObject.SetActive(true);
        
        if (commonTile.OnUnit == null)
        {
            _emptyTileObj.SetActive(true);
        }
        else
        {
            _onUnitTileObj.SetActive(true);
            _upgradeBtn.interactable = curCommonTile.OnUnit.Level < UnitLevel.Three;
        }

        _popupState = commonTile.OnUnit == null ? State.Empoly : State.Change;
    }

    public void B_SelectClass(int typeIdx)
    {
        bool result = false;
        var type = (ClassType)typeIdx;
        if (_popupState == State.Empoly)
        {
            if (GameManager.I.CheckCost(GamePassive.I.EmpolyCost))
            {
                curCommonTile.Employ(type);
                result = true;
            }
        }
        else if (_popupState == State.Change)
        {
            if (GameManager.I.CheckCost(GamePassive.I.ChangeCost))
            {
                curCommonTile.ChangeClass(type);
                result = true;
            }
        }

        if (result)
        {
            EndEvent();
        }
    }

    public void B_Upgrade()
    {
        if (GameManager.I.CheckCost(GamePassive.I.UpgradeCost))
        {
            curCommonTile.Upgrade();
            EndEvent();
        }
    }

    public void B_Destory()
    {
        curCommonTile.Destroy();
        EndEvent();
    }
    
    /// <summary>
    /// 플레이어의 행동을 완료 후 종료
    /// </summary>
    private void EndEvent()
    {
        B_Close();
        GameManager.I.Next();
    }

    /// <summary>
    /// 패널을 종료
    /// </summary>
    public void B_Close()
    {
        curCommonTile = null;
        _emptyTileObj.SetActive(false);
        _onUnitTileObj.SetActive(false);
        _selectClassContentTileObj.SetActive(false);
        gameObject.SetActive(false);
    }
}
