using System.Collections;

public interface IEnemy
{
    /// <summary>
    /// Enemy HP
    /// </summary>
    int HP { get; set; }
    
    /// <summary>
    /// 세계에 적응 여부
    /// </summary>
    bool IsAdapt { get; set; }

    /// <summary>
    /// 액션
    /// </summary>
    IEnumerable Execute();
}