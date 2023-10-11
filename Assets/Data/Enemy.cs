using System;
using System.Collections;
using System.Collections.Generic;

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

public abstract class Enemy : IEnemy
{
    private int _hp;
    public int HP
    {
        get => _hp;
        set => _hp = Math.Max(0, value);
    }
        
    private bool _isAdapt;
    public bool IsAdapt
    {
        get => _isAdapt;
        set => _isAdapt = value;
    }

    private int _adaptCount;
    private int _turnCount;

    private int TurnCount
    {
        get => _turnCount;
        set
        {
            _turnCount = value;
            if (_turnCount == _adaptCount)
            {
                IsAdapt = true;
                _turnCount = 0;
            }
        }
    }
    
    public IEnumerable Execute()
    {
        if (!_isAdapt)
        {
            ExecuteBeforeAdapt();
            TurnCount++;
        }
        else
        {
            ExecuteAfterAdapt();
        }

        yield return null;
    }

    protected abstract void ExecuteBeforeAdapt();
    
    protected abstract void ExecuteAfterAdapt();

    public void Destroy()
    {
        
    }
}