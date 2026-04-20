namespace BLOC3.PP7_HeroEngine.models;

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