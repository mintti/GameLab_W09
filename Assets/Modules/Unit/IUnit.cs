using System.Collections.Generic;

public enum Class
{
    Ignore,
    Knight,
    Wizard,
    Manager
}

public enum UnitType
{
    Ignore,
    Battle,
    Enigma,
    Research
}

public enum UnitLevel : int
{
    Ignore = 0,
    One,
    Two,
    Three
}

public interface IUnit
{
    Class Class { get; set; }
    UnitLevel Level { get; set; }
    List<UnitType> HasType { get; set; }

    /// <summary>
    /// 일정 주기마다 소모되는 비용
    /// </summary>
    int CycleExpense { get; set; }
        
    /// <summary>
    /// 초기화 시 설정
    /// </summary>
    void Init();

    void BattleAction();
}

