using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using HeroEngine.Web.Data;
using HeroEngine.Core.Models;
using BLOC4.PP8_HeroEngine_FilesRazorQuest.Combat;

namespace HeroEngine.Web.Pages.Combat
{
    public class CombatModel : PageModel
    {
        public List<AHero> AvailableHeroes { get; set; }
        public List<string> BattleLogs { get; set; } = new List<string>();

        public void OnGet()
        {
            AvailableHeroes = FakeDatabase.Heroes;
        }

        public IActionResult OnPost()
        {
            AvailableHeroes = FakeDatabase.Heroes;

            if (AvailableHeroes.Count == 0)
            {
                ModelState.AddModelError(string.Empty, "There are no available heroes.");
                return Page();
            }

            foreach (var hero in AvailableHeroes)
            {
                hero.CurrentHp = hero.MaxHp;
            }

            List<ACharacter> heroTeam = AvailableHeroes.Cast<ACharacter>().ToList();

            ACharacter enemy = new Minion("Minion");
            List<ACharacter> enemiesTeam = new List<ACharacter> { enemy };

            if (!Directory.Exists("Files"))
            {
                Directory.CreateDirectory("Files");
            }

            TextWriter originalConsole = Console.Out;
            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            try
            {
                BattleEngine engine = new BattleEngine(heroTeam, enemiesTeam);
                engine.StartBattle();
            }
            finally
            {
                Console.SetOut(originalConsole);
            }

            string rawOutput = consoleOutput.ToString();
            BattleLogs = rawOutput.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).ToList();

            return Page();
        }
    }
}
