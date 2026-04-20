using BLOC4.PP8_HeroEngine_FilesRazorQuest.Data;

namespace BLOC3.PP7_HeroEngine.models;

public abstract class ACharacter
{
    public const string toStringMSG = "[{0}] {1} | Level: {2} | HP: {3}/{4} | Power: {5} | Defense: {6} ";
    public const string defAttackMSG = "{0} is defeated and can't attack!";
    public const string attackMSG = "{0} attacks! Deals {1} damage.";
    public const string defDamageMSG = "{0} is defeated and can't receive damage!";
    public const string damageMSG = "{0} receives {1} damage -> Defense: {2} -> Actual damage: {3} | {4}/{5}";
    
    public string Name { get; set; }
    public abstract string Faction { get; }
    public string DisplayName => $"[{Faction}] {Name}";
    public int Level { get; set; }
    public int CurrentHp { get; set; }
    public int MaxHp { get; set; }
    protected int Power { get; set; }
    protected int Defense { get; set; }
    public bool IsDefeated => CurrentHp <= 0;
    public virtual int Initiative => Power + MaxHp + Defense;
    
    protected ACharacter(string name)
    {
        Name = name;
        Level = 1;
    }

    public override string ToString() => String.Format(toStringMSG, GetType().Name, DisplayName, Level, CurrentHp, MaxHp, Power, Defense);

    /// <summary>
    /// Gets the hero's characteristic greeting or introduction phrase.
    /// </summary>
    /// <returns>A string containing the greeting.</returns>
    public string Greeting() => ToString();

    /// <summary>
    /// Calculates and executes the hero's attack based on their stats (Power).
    /// </summary>
    /// <returns>The total amount of damage the hero inflicts in this attack.</returns>
    public virtual int Attack()
    {
        if (IsDefeated)
        {
            BattleLogger.Log(string.Format(defAttackMSG, DisplayName));
            return 0;
        }
        BattleLogger.Log(string.Format(attackMSG, DisplayName, Power));
        return Power;   
    }

    /// <summary>
    /// Processes the amount of damage the hero receives and updates their current health points (<see cref="CurrentHp"/>).
    /// </summary>
    /// <param name="damage">The amount of incoming damage the hero takes.</param>
    /// <returns>The hero's remaining health points after the impact, or the effective damage taken (depending on the derived class implementation).</returns>
    public virtual int TakeDamage(int damage)
    {
        if (IsDefeated)
        {
            BattleLogger.Log(string.Format(defDamageMSG, DisplayName));
            return 0;
        }
        int actualDamage = Math.Max(0, damage - Defense);
        CurrentHp = Math.Max(0, CurrentHp - actualDamage);
        BattleLogger.Log(string.Format(damageMSG, DisplayName, damage, Defense, actualDamage, CurrentHp, MaxHp));
        return CurrentHp;
    }
}