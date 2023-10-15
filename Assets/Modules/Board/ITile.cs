using System.Collections;

public interface ITile2
{
    int Index { get; set; }
    TileType Type { get; set; }
    
    IEnumerator OnEvent();
    void ExitEvent();
    void PassEvent();
}

public enum TileType : int
{
    Ignore,
    Common,
    Start,
    Trap,
    Reception,
    Portal
}