using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using HeroEngine.Core.Data;
using HeroEngine.Core.Models;
using BLOC4.PP8_HeroEngine_FilesRazorQuest.Combat;
using HeroEngine.Core.Combat;

namespace HeroEngine.Web.Pages.Combat
{
    public class CombatModel : PageModel
    {
        public List<AHero> AvailableHeroes { get; set; } = new List<AHero>();
        public List<ACharacter> EnemyTeam { get; set; } = new List<ACharacter>();
        public List<string> BattleLogs { get; set; } = new List<string>();

        public void OnGet()
        {
            AvailableHeroes = HeroRepository.GetAll();
        }

        public IActionResult OnPost()
        {
            AvailableHeroes = HeroRepository.GetAll();

            if (AvailableHeroes == null || AvailableHeroes.Count == 0)
            {
                ModelState.AddModelError(string.Empty, "No heroes available for combat.");
                return Page();
            }

            foreach (var hero in AvailableHeroes)
            {
                hero.CurrentHp = hero.MaxHp;

                if (hero is Mage mage)
                {
                    mage.CurrentMana = mage.MaxMana;
                }

            }

            List<ACharacter> heroTeam = AvailableHeroes.Cast<ACharacter>().ToList();

            Random rnd = new Random();
            int amountOfEnemies = rnd.Next(1, 6);

            for (int i = 1; i <= amountOfEnemies; i++)
            {
                int enemyType = rnd.Next(1, 4);
                ACharacter newEnemy;

                switch (enemyType)
                {
                    case 1:
                        newEnemy = new Minion($"Minion {i}");
                        break;
                    case 2:
                        newEnemy = new Elite($"Elite {i}");
                        break;
                    default:
                        newEnemy = new Boss($"Boss {i}");
                        break;
                }

                newEnemy.InitiativeModifier = rnd.Next(1, 20);
                EnemyTeam.Add((AEnemy)newEnemy);
            }
            BattleLogger.Initialize();

            BattleEngine engine = new BattleEngine(heroTeam, EnemyTeam);
            engine.StartBattle();

            bool heroesWon = heroTeam.Any(h => !h.IsDefeated);

            BattleResult result = engine.Stats.GenerateCombatResult(heroTeam, EnemyTeam, heroesWon, engine.CurrentRound);
            CsvStatsWriter.AppendCombatStats(result);

            BattleLogs = BattleLogger.CurrentBattleLogs.ToList();
            string dataFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Data");

            if (!Directory.Exists(dataFolderPath))
            {
                Directory.CreateDirectory(dataFolderPath);
            }

            string logFilePath = Path.Combine(dataFolderPath, "battleLog.txt");
            System.IO.File.WriteAllLines(logFilePath, BattleLogs);

            return Page();
        }
    }
}
