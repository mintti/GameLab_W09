using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UICommonTile : BaseTile
{
    #region Base Tile
    [SerializeField] private int _index;

    /// <summary>
    /// 타일 인덱스
    /// </summary>
    public override int Index
    {
        get => _index; 
        set => _index = value;
    }
    
    public override IEnumerator OnEvent()
    {
        UIManager.I.ActivePlayerActionSelector();
        yield return GameManager.I.WaitNext();
    }
    
    public override void ExitEvent()
    {
        UIManager.I.DisabledPlayerActionSelector();
    }
    #endregion
    
    public void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private MeshRenderer _meshRenderer;
    
    public IUnit OnUnit { get; set; }

    public void Employ(ClassType type)
    {
        var obj = Instantiate(ResourceManager.I.UnitPrefab, transform);
        var unit = ResourceManager.I.GetUnit(type);
        obj.GetComponent<UIUnit>().Init(unit);

        OnUnit = unit;
        unit.Init(UnitLevel.One);
        
        UpdateMaterial();
    }

    public void Upgrade()
    {
        OnUnit.Level = (UnitLevel)((int)OnUnit.Level + 1);
        UpdateMaterial();
    }
        
    public void ChangeClass(ClassType type)
    {
        OnUnit.ClassType = type;
        UpdateMaterial();
    }

    public void Destroy()
    {
        Destroy(OnUnit.UIUnit.gameObject);
        OnUnit = null;
        UpdateMaterial();
        
        GameManager.I.CalculateSynergy();
    }

    /// <summary>
    /// ON Unit 타입에 따른 타일 색상 변경
    /// </summary>
    void UpdateMaterial()
    {
        Material changeMaterial = null;
        
        if (OnUnit != null)
        {
            var type = OnUnit.ClassType;
            var colorMaterial = ResourceManager.I.ClassColorMaterial[(int)type];
            changeMaterial = colorMaterial;
        }
        else
        {
            changeMaterial = ResourceManager.I.DefaultTileColorMaterial;
        }

        _meshRenderer.material = changeMaterial;
    }
}