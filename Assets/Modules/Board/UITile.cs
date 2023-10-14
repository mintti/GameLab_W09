using UnityEngine;

public class UITile : MonoBehaviour
{
    [SerializeField] private int _index; 
    /// <summary>
    /// 타일 인덱스
    /// </summary>
    public int Index { get => _index; set => _index = value; }
    
    public IUnit OnUnit { get; set; }

    public void Collocate(ClassType type)
    {
        
    }

    public void Upgrade()
    {
        
    }
        
    public void ChangeClass(ClassType type)
    {
        
    }

    public void Destroy()
    {
        
    }
}