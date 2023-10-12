using System.Collections;
using UnityEngine;

public interface IEnemy
{
    string Name { get; }
    
    /// <summary>
    /// Enemy HP
    /// </summary>
    int HP { get; set; }
    
    /// <summary>
    /// 세계에 적응 여부
    /// </summary>
    bool IsAdapt { get; set; }
    
    Sprite AdaptBeforeSprite { get; set; }
    
    Sprite AdaptAfterSprite { get; set; }
    
    /// <summary>
    /// 액션
    /// </summary>
    IEnumerable Execute();
}