using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using Vector3 = UnityEngine.Vector3;

public class BoardManager : MonoBehaviour
{
    [Header("Board Setting")]
    [SerializeField] private Transform _pivotTr; // 아래 오브젝트 상위에 존재하는 중심점 설정용 오브젝트
    [SerializeField] private Transform _crackTr; // 균열 (보라색) 오브젝트
    [SerializeField] private Transform _cameraTargetTr;  // 카메라 대상이 되는 객체의 Transform

    [Header("Tile Related")]
    [SerializeField] private Transform _tilesTr;
    [SerializeField] private GameObject _tilePrefab;
    public ObservableCollection<BaseTile> Tiles { get; private set; }

    public IEnumerable<IUnit> SortedOnUnitTiles =>
        Tiles.Where(x=> x is UICommonTile)
            .Select(x=> x.GetComponent<UICommonTile>())
            .Where(x=> x.OnUnit != null)
            .OrderBy(x => x.Index)
            .Select(x=> x.OnUnit);
    
    
    
    /// <summary>
    /// 현재 보드에서 한 라인 내 타일 갯수
    /// </summary>
    private int _boardLineCount = 3;
    
    public BaseTile PlayerOnTile { get; private set; }
    private int PlayerOnTileIdx => PlayerOnTile.Index;
    public BaseTile ClickTile { get; set; }

    [SerializeField] private GameObject _enemyObject;
    public Vector3 EnemyPosition => _enemyObject.transform.position;
    
    public void Init()
    {
        // 타일 컬렉션 변경 이벤트 설정
        Tiles = new();
        Tiles.CollectionChanged += (o, sender) =>
        {
            // 인덱스 재정의
            
            // 한 라인 내 연속으로 연결된 타입이 있는 경우, 시너지 효과에 추가 
        };
        
        // 타일 정보 초기화
        // [TODO] 타일을 생성해내어야함
        for (int i = 0, cnt = _tilesTr.childCount; i < cnt; i++)
        {
            var tr = _tilesTr.GetChild(i);
            tr.gameObject.AddComponent(GetTileType(i));
            var tile = _tilesTr.GetChild(i).GetComponent<BaseTile>();
            tile.Index = i;
            Tiles.Add(tile);
        }

        PlayerOnTile = Tiles[0];
    }

    private Type GetTileType(int index)
    {
        Type result;
        if (index % _boardLineCount == 0)
        {
            result = (index / _boardLineCount) switch
            {
                0 => typeof(UIStartTile),
                1 => typeof(UITrapTile),
                2 => typeof(UIRestTile),
                3 => typeof(UIPortalTile),
            };
        }
        else
        {
            result = typeof(UICommonTile);
        }

        return result;
    }

    public void InitTile(int tileCntByLine)
    {
        for (int i = 0; i < tileCntByLine * 4; i++)
        {
            var obj = Instantiate(_tilePrefab, _tilesTr);
            
            // 위치 지정
            
            // 중심점 지정
            
            //
        }
    }
    
    private void AddTile()
    {
        // 각 라인에 타일 추가
        for (int i = 0; i < 4; i++)
        {
            int insertIdx = (_boardLineCount * i) + i;
        }
        
        // 외 옵션 설정
        _boardLineCount++;
        int crackScale = _boardLineCount - 1;
        _crackTr.localScale = new(crackScale, .1f, crackScale);
        
        // [TODO] 추가 타일 위치 지정 필요
        
    } 


    private void TradeTile()
    {
        
    }

    /// <summary>
    /// 현재 위치에서 한칸 이동된 타일 정보를 출력합니다.
    /// </summary>
    /// <param name="changeCurTile">플레이어가 존재하는 타일 정보를 업데이트 합니다</param>
    /// <returns></returns>
    public BaseTile GetNextTile(bool changeCurTile)
    {
        int index = (PlayerOnTileIdx + 1) % Tiles.Count;
        var tile = Tiles[index];

        if (changeCurTile)
        {
            // 대상 설정
            PlayerOnTile = tile;
            
            // 카메라 대상 오브젝트 회전
            if (index % _boardLineCount == 0)
            {
                int lineIdx = index / _boardLineCount; 
                SetAngle(lineIdx);
                GameManager.I.PlayerController.Rotate(lineIdx);
            }
        }
        
        return tile;
    }

    void SetAngle(int lineIdx)
    { 
        // 라인별 지정된 각도로 지정
        var YAngle = lineIdx switch
        {
            0 => -45,
            1 =>  45,
            2 =>  135,
            3 =>  225,
            _ => 0
        };

        // 지정 방식
        var angle = _cameraTargetTr.localEulerAngles;
        angle.y = YAngle;
        _cameraTargetTr.DOLocalRotate(angle, .5f) .SetEase(Ease.Linear);

        // // 가산 방식
        // var angle = _cameraTargetTr.localEulerAngles;
        // angle.y = YAngle;
        // _cameraTargetTr.DORotate( new Vector3(0, 90, 0), .5f, RotateMode.WorldAxisAdd)
        //     .OnComplete(() =>
        //     {
        //         var angle = _cameraTargetTr.rotation;
        //         angle.y %= 360;
        //         _cameraTargetTr.rotation = angle;
        //     });
    }
}