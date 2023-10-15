public class Slime : BaseEnemy
{

    public Slime(string name, int hp, int adaptCnt) : base(name, hp, adaptCnt)
    {
    }
    
    protected override void ExecuteBeforeAdapt()
    {
        
    }

    protected override void ExecuteAfterAdapt()
    {
        Attack(5);
    }
}