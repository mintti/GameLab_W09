using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUnitManagingPopup : MonoBehaviour
{
    enum State
    {
        Ignore,
        Empoly,
        Change
    }
    
    [Header("Panel")] 
    [SerializeField] private GameObject EmptyTileObj;
    [SerializeField] private GameObject OnUnitTileObj;
    [SerializeField] private GameObject SelectClassContentTileObj;

    [Header("Info")]
    [SerializeField] private State _popupState;
    [SerializeField] private UITile _curTile;
    
    public void B_ActivePanel()
    {
        UITile tile = null; // [TODO] 현재 서 있는 위치 정보 로드 필요
        _curTile = tile;
        gameObject.SetActive(true);
        
        if (tile.OnUnit == null)
        {
            EmptyTileObj.SetActive(true);
        }
        else
        {
            OnUnitTileObj.SetActive(true);
        }
    }

    public void B_SelectClass(int typeIdx)
    {
        var type = (ClassType)typeIdx;
        if (_popupState == State.Empoly)
        {
            _curTile.Collocate(type);
        }
        else if (_popupState == State.Change)
        {
            _curTile.ChangeClass(type);
        }
    }

    public void B_Upgrade() => _curTile.Upgrade();
    public void B_Destory() => _curTile.Destroy();


    public void B_Close()
    {
        _curTile = null;
        EmptyTileObj.SetActive(false);
        OnUnitTileObj.SetActive(false);
        SelectClassContentTileObj.SetActive(false);
        gameObject.SetActive(false);
    }
}
