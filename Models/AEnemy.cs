namespace BLOC3.PP7_HeroEngine.models;

public abstract class AEnemy : ACharacter
{
    public override string Faction => "ENEMY";
    protected AEnemy(string name) : base(name){}
}