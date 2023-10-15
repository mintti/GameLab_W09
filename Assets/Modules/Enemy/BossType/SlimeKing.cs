public class SlimeKing : BaseEnemy
{
    public SlimeKing(string name, int hp, int adaptCnt) : base(name, hp, adaptCnt)
    {
    }
    
    protected override void ExecuteBeforeAdapt()
    {
        
    }

    protected override void ExecuteAfterAdapt()
    {
        Attack(20);
    }

}