using System.Collections.ObjectModel;
using DG.Tweening;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int _enigma;

    public int Enigma
    {
        get => _enigma;
        set => _enigma = value;
    }
    
    private int _castleHp;
    public int CastleHp
    {
        get => _castleHp;
        set => _castleHp = value;
    }

    [SerializeField] private Ease _moveEase;
    
    public ObservableCollection<IUnit> UnitCollection { get; set; }


    public void Init()
    {
        // Collection Update Event
        UnitCollection.CollectionChanged += (sender, e)=>
        {
        };
    }


    public void Move(Vector3 targetPos)
    {
        transform.DOMove(targetPos, 1)
            .SetEase(_moveEase)
            .OnComplete(() => GameManager.I.Next());
    }
}