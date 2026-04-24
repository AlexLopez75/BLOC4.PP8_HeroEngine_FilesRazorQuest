namespace HeroEngine.Core.Models;

public class Minion : AEnemy
{
    public Minion(string name) : base(name)
    {
        MaxHp = 100;
        CurrentHp = MaxHp;
        Power = 20;
        Defense = 10;
    }
}