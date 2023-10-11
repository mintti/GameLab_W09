using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

public class BoardManager
{
    private ObservableCollection<UITile> Tiles { get; set; }
    public IEnumerable<IUnit> SortedOnUnitTiles => Tiles.Where(x=> x.OnUnit != null).OrderBy(x => x.Index).Select(x=> x.OnUnit);
    
    
    public void Init()
    {
        Tiles.CollectionChanged += (o, sender) =>
        {
            // 인덱스 재정의
            
            // 한 라인 내 연속으로 연결된 타입이 있는 경우, 시너지 효과에 추가 
        };
    }
    
    private void AddTile()
    {
        
    }


    private void TradeTile()
    {
        
    }
}