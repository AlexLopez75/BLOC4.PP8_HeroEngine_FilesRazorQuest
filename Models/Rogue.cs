using BLOC4.PP8_HeroEngine_FilesRazorQuest.Data;

namespace HeroEngine.Core.Models;

public class Rogue : AHero
{
    private const string toStringMSG = "| Damage multiplier: {0} | Daggers: {1}";
    private const string defAttackMSG = "{0} is defeated and can't attack!";
    private const string attackMSG = "{0} attacks! -> Base damage: {1}, Multiplier: {2} -> Deals {3} damage.";

    public int MultDamage { get; set; }
    public int NumDaggers { get; set; }

    public Rogue(string name, int numDaggers) : base(name)
    {
        MaxHp = 170;
        CurrentHp = MaxHp;
        Power = 15;
        Defense = 25;
        MultDamage = 3;
        NumDaggers = numDaggers;
    }

    public override string ToString() => base.ToString() + String.Format(toStringMSG, MultDamage, NumDaggers );
    
    public override int Attack()
    {
        if (CurrentHp <= 0)
        {
            BattleLogger.Log(string.Format(defAttackMSG, DisplayName));
            return 0;
        }
        BattleLogger.Log(string.Format(attackMSG, DisplayName, Power, MultDamage, Power * MultDamage));
        return Power * MultDamage;   
    }
}