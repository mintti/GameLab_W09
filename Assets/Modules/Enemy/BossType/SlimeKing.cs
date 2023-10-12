public class SlimeKing : BaseEnemy
{
    public SlimeKing(string name, int hp, int adaptCnt) : base(name, hp, adaptCnt)
    {
    }
    
    protected override void ExecuteBeforeAdapt()
    {
        throw new System.NotImplementedException();
    }

    protected override void ExecuteAfterAdapt()
    {
        throw new System.NotImplementedException();
    }

}