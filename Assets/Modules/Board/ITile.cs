public interface ITile
{
    int Index { get; set; }
    TileType Type { get; set; }
    
    void OnEvent();
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