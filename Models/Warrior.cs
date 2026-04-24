using BLOC4.PP8_HeroEngine_FilesRazorQuest.Data;
using System.Globalization;

namespace HeroEngine.Core.Models;

public class Warrior : AHero
{
    private const string toStringMSG = "| Armor: {0} | Battle Cry: {1}";
    private const string defDamageMSG = "{0} is defeated and can't receive damage!";
    private const string damageMSG = "{0} receives {1} damage. -> Defense: {2}, Armor absorbs {3} damage -> Total damage: {4} | HP: {5}/{6}";
    
    public int Armor { get; set; }
    public string BattleCry { get; set; }

    public Warrior(string name, string battleCry) : base(name)
    {
        BattleCry = battleCry;
        MaxHp = 150;
        CurrentHp = MaxHp;
        Power = 30;
        Defense = 15;
        Armor = 40;
    }
    
    public override string ToString() => base.ToString() + String.Format(toStringMSG, Armor, BattleCry);

    public override int TakeDamage(int damage)
    {
        if (CurrentHp <= 0)
        {
            BattleLogger.Log(string.Format(defDamageMSG, DisplayName));
            return 0;
        }
        int actualDamage = Math.Max(0, damage - Defense - Armor);
        CurrentHp = Math.Max(0, CurrentHp - actualDamage);
        BattleLogger.Log(string.Format(damageMSG, DisplayName, damage, Defense, Armor, actualDamage, CurrentHp, MaxHp));
        return CurrentHp;
    }
}