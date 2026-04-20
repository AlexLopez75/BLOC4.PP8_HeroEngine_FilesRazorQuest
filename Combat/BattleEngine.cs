using BLOC3.PP7_HeroEngine.models;
using BLOC4.PP8_HeroEngine_FilesRazorQuest.Data;

namespace BLOC4.PP8_HeroEngine_FilesRazorQuest.Combat;

/// <summary>
/// Manages the turn-based combat system between a group of heroes and enemies.
/// </summary>
public class BattleEngine
{
    private const string separator = "======================================================";
    private const string battleStartMSG = "                    BATTLE STARTS                     ";
    private const string roundMSG = "\n============== BATTLE LOG - Round {0} ===============";
    private const string heroWinMSG = "                   THE HEROES WIN!                    ";
    private const string enemyWinMSG = "                THE PRIMORDIAL BUG WINS!                 ";
    private const string expLevelUPMSG = "[SYSTEM] The experience of the battle makes the suvivors stronger!";
    private const string heroLevelUPMSG = "[SYSTEM] {0} levelled up to Level {1}!";
    
    private List<ACharacter> _heroes;
    private List<ACharacter> _enemies;
    private int _countRound;
    private BattleStatistics _stats;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="BattleEngine"/> class with the specified combatants.
    /// </summary>
    /// <param name="heroes">The list of heroes participating in the battle.</param>
    /// <param name="enemies">The list of enemies participating in the battle.</param>
    public BattleEngine(List<ACharacter> heroes, List<ACharacter> enemies)
    {
        _heroes = heroes;
        _enemies = enemies;
        _countRound = 1;
        _stats = new BattleStatistics();
    }

    /// <summary>
    /// Starts the main battle loop. The battle continues round by round until either all heroes or all enemies are defeated.
    /// </summary>
    public void StartBattle()
    {
        BattleLogger.Initialize();
        
        BattleLogger.Log(separator);
        BattleLogger.Log(battleStartMSG);
        BattleLogger.Log(separator);
        
        while (!IsBattleFinished())
        {
            PlayRound();
            _countRound++;
        }
        AnounceWinner();
    }

    /// <summary>
    /// Executes a single round of combat. Determines turn order based on initiative and processes each combatant's attack.
    /// </summary>
    private void PlayRound()
    {
        BattleLogger.Log(string.Format(roundMSG, _countRound));
        
        var allCombatants = new List<ACharacter>();
        allCombatants.AddRange(_heroes.Where(h => !h.IsDefeated));
        allCombatants.AddRange(_enemies.Where(e => !e.IsDefeated));

        var turnOrder = allCombatants.OrderByDescending(c => c.Initiative);

        foreach (var combatant in turnOrder)
        {
            if (IsBattleFinished()) break; //If combat ends in the middle of the round, exit.
            
            if (combatant.IsDefeated) continue; //Prevents combatants from attacking while defeated.
            
            ACharacter target = GetTarget(combatant);

            if (target != null)
            {
                int damage = combatant.Attack();
                int hpBefore = target.CurrentHp;
                target.TakeDamage(damage);
                int actualDamageDealt = hpBefore - target.CurrentHp;
                _stats.RecordDamage(combatant, actualDamageDealt);
                
                if (target.IsDefeated) _stats.RecordEnemyDefeat(target, _countRound);
            }
        }
    }

    /// <summary>
    /// Determines the target for the current attacking combatant.
    /// </summary>
    /// <param name="combatant">The character whose turn it is to attack.</param>
    /// <returns>The first available alive enemy if the combatant is a hero, or the first available alive hero if the combatant is an enemy. Returns <c>null</c> if no targets are valid.</returns>
    private ACharacter GetTarget(ACharacter combatant)
    {
        if (_heroes.Contains(combatant))
        {
            return _enemies.FirstOrDefault(e => !e.IsDefeated);
        }
        else
        {
            return _heroes.FirstOrDefault(h => !h.IsDefeated);
        }
    }  
    
    /// <summary>
    /// Checks whether the battle has reached a conclusion.
    /// </summary>
    /// <returns><c>true</c> if all heroes or all enemies are defeated; otherwise, <c>false</c>.</returns>
    public bool IsBattleFinished()
    {
        bool allHeroesDefeated = _heroes.All(h => h.IsDefeated);
        bool allEnemiesDefeated = _enemies.All(e => e.IsDefeated);

        return allHeroesDefeated || allEnemiesDefeated;
    }
    
    /// <summary>
    /// Logs the final result of the battle and applies experience or level-ups to surviving heroes if they win.
    /// </summary>
    private void AnounceWinner()
    {
        BattleLogger.Log("");
        BattleLogger.Log(separator);
        if (_enemies.All(e => e.IsDefeated))
        {
            BattleLogger.Log(heroWinMSG);
            BattleLogger.Log(separator);
            BattleLogger.Log(expLevelUPMSG);
            foreach (var hero in _heroes)
            {
                if (!hero.IsDefeated)
                {
                    if (hero is AHero survivingHero)
                    {
                        survivingHero.LevelUp();
                        BattleLogger.Log(String.Format(heroLevelUPMSG, survivingHero.Name, survivingHero.Level));
                    }
                }
            }
        }
        else
        {
            BattleLogger.Log(enemyWinMSG);
        }
        BattleLogger.Log(separator);
        
        _stats.PrintStatistics();
    }
}