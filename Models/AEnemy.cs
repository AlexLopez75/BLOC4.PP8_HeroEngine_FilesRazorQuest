namespace HeroEngine.Core.Models;

public abstract class AEnemy : ACharacter
{
    public override string Faction => "ENEMY";
    protected AEnemy(string name) : base(name){}
}