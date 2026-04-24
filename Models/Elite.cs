namespace HeroEngine.Core.Models;

public class Elite : AEnemy
{
    public Elite(string name) : base(name)
    {
        MaxHp = 250;
        CurrentHp = MaxHp;
        Power = 50;
        Defense = 20;
    }
}