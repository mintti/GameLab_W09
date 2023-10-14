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
    [SerializeField] private UITile _curTile;
    
    public void B_ActivePanel()
    {
        UITile tile = GameManager.I.CurOnTile; // [TODO] 현재 서 있는 위치 정보 로드 필요
        _curTile = tile;
        gameObject.SetActive(true);
        
        if (tile.OnUnit == null)
        {
            _emptyTileObj.SetActive(true);
        }
        else
        {
            _onUnitTileObj.SetActive(true);
            _upgradeBtn.interactable = _curTile.OnUnit.Level < UnitLevel.Three;
        }

        _popupState = tile.OnUnit == null ? State.Empoly : State.Change;
    }

    public void B_SelectClass(int typeIdx)
    {
        bool result = false;
        var type = (ClassType)typeIdx;
        if (_popupState == State.Empoly)
        {
            if (GameManager.I.CheckCost(GamePassive.I.EmpolyCost))
            {
                _curTile.Employ(type);
                result = true;
            }
        }
        else if (_popupState == State.Change)
        {
            if (GameManager.I.CheckCost(GamePassive.I.ChangeCost))
            {
                _curTile.ChangeClass(type);
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
            _curTile.Upgrade();
            EndEvent();
        }
    }

    public void B_Destory()
    {
        _curTile.Destroy();
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
        _curTile = null;
        _emptyTileObj.SetActive(false);
        _onUnitTileObj.SetActive(false);
        _selectClassContentTileObj.SetActive(false);
        gameObject.SetActive(false);
    }
}
