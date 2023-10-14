using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUnit : MonoBehaviour
{
    private IUnit _unit;
    [SerializeField] private MeshFilter _meshFilter;
    [SerializeField] private MeshRenderer _meshRenderer;
    
    public void Init(IUnit unit)
    {
        _unit = unit;
        ChangedLevel(unit.Level);
        ChangedClassType(unit.ClassType);
        
        // 설정
        unit.UIUnit = this;
    }

    public void ChangedLevel(UnitLevel level)
    {
        _meshFilter.mesh = ResourceManager.I.UnitMeshByLevel[(int)level];
    }

    public void ChangedClassType(ClassType type)
    {
        _meshRenderer.material = ResourceManager.I.ClassColorMaterial[(int)type];
    }
}
