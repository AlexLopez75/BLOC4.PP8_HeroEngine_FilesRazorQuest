using BLOC3.PP7_HeroEngine.models;
using BLOC4.PP8_HeroEngine_FilesRazorQuest.Data;

namespace BLOC4.PP8_HeroEngine_FilesRazorQuest.Combat;

/// <summary>
/// Manages the register of statistics of the battle and the printing of these stats.
/// </summary>
public class BattleStatistics
{
    private const string separator = "======================================================";
    private const string statisticsMSG = "                  BATTLE STATISTICS                   ";
    private const string totalDamageMSG = "[SYSTEM] Total damage dealt by the heroes: {0}";
    private const string effectiveHeroMSG = "[SYSTEM] Most effective hero (most damage dealt): {0} ({1} damage)";
    private const string fastestDefeatMSG = "[SYSTEM] Fastest enemy defeated: {0} (defeated in round {1})";
    
    public int TotalDamageInflicted { get; private set; } = 0;
    private Dictionary<string, int> HeroDamage = new Dictionary<string, int>();
    private Dictionary<string, int> EnemyDefeatRounds = new Dictionary<string, int>();

    /// <summary>
    /// Registers the true damage after every attack.
    /// </summary>
    public void RecordDamage(ACharacter attacker, int actualDamage)
    {
        if (attacker is AHero)
        {
            TotalDamageInflicted += actualDamage;
            if (!HeroDamage.ContainsKey(attacker.Name))
            {
                HeroDamage[attacker.Name] = 0;
            }
            HeroDamage[attacker.Name] += actualDamage;
        }
    }

    /// <summary>
    /// Registers the round which an enemy is defeated.
    /// </summary>
    public void RecordEnemyDefeat(ACharacter enemy, int round)
    {
        if (enemy is AEnemy && !EnemyDefeatRounds.ContainsKey(enemy.Name))
        {
            EnemyDefeatRounds.Add(enemy.Name, round);
        }
    }
    
    /// <summary>
    /// Calculates and prints the final statistics.
    /// </summary>
    public void PrintStatistics()
    {
        BattleLogger.Log("");
        BattleLogger.Log(separator);
        BattleLogger.Log(statisticsMSG);
        BattleLogger.Log(separator);
        BattleLogger.Log(String.Format(totalDamageMSG, TotalDamageInflicted));

        if (HeroDamage.Count > 0)
        {
            var bestHero = HeroDamage.OrderByDescending(x => x.Value).First();
            BattleLogger.Log(String.Format(effectiveHeroMSG, bestHero.Key, bestHero.Value));
        }

        if (EnemyDefeatRounds.Count > 0)
        {
            var fastestDefeat = EnemyDefeatRounds.OrderBy(x => x.Value).First();
            BattleLogger.Log(String.Format(fastestDefeatMSG, fastestDefeat.Key, fastestDefeat.Value));
        }
    }
}