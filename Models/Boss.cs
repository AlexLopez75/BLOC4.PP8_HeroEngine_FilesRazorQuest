namespace HeroEngine.Core.Models;

public class Boss : AEnemy
{
    public Boss(string name) : base(name)
    {
        MaxHp = 600;
        CurrentHp = MaxHp;
        Power = 80;
        Defense = 40;
    }
}