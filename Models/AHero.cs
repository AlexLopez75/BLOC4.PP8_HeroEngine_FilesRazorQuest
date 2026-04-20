namespace BLOC3.PP7_HeroEngine.models;

public class AHero : ACharacter
{
    protected AHero(string name) : base(name){}
    
    public override string Faction => "HERO";

    /// <summary>
    /// Upgrades MaxHp and Power values.
    /// </summary>
    public void LevelUp()
    {
        Level++;
        MaxHp = (int)(MaxHp + MaxHp * 0.2 * (Level - 1));
        Power = (int)(Power + Power * 0.1 * (Level - 1));
        Defense = (int)(Defense + Defense * 0.1 * (Level - 1));
        CurrentHp = MaxHp;
    }
}