using System;
using System.Collections;
using UnityEngine;

public abstract class BaseEnemy : IEnemy
{
    private SpriteRenderer _displaySpriteRenderer;
    public SpriteRenderer DisplaySpriteRenderer
    {
        private get => _displaySpriteRenderer;
        set
        {
            _displaySpriteRenderer = value;
            _displaySpriteRenderer.sprite = AdaptBeforeSprite;
        }
    }
    
    public BaseEnemy(string name, int hp, int adaptCnt)
    {
        Name = name;
        HP = hp;
        _adaptCount = adaptCnt;

        string path = $"{CommonConst.EnemySpritePath}/{GameManager.I.CurEnvName}/{name}";
        AdaptBeforeSprite = Resources.Load<Sprite>(path);
        AdaptAfterSprite = Resources.Load<Sprite>($"{path}_Adapted");
    }
    
    #region IEnemy
    public string Name { get; }
    public int HP
    {
        get => _hp;
        set => _hp = Math.Max(0, value);
    }
    
    public bool IsAdapt
    {
        get => _isAdapt;
        set
        {
            _isAdapt = value;
            if (_isAdapt)
            {
                DisplaySpriteRenderer.sprite = AdaptAfterSprite;
            }
        }
    }

    public Sprite AdaptBeforeSprite { get; set; }
    
    public Sprite AdaptAfterSprite { get; set; }
    
    public IEnumerator Execute()
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
    #endregion
    
    private int _hp;
    
    private bool _isAdapt;

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

    protected abstract void ExecuteBeforeAdapt();
    
    protected abstract void ExecuteAfterAdapt();

    public void Destroy()
    {
        DisplaySpriteRenderer.sprite = null;
        DisplaySpriteRenderer = null;
    }
}