using GestionFicheros.CSVParsing;
using HeroEngine.Core.Analytics;
using HeroEngine.Core.Combat;
using HeroEngine.Core.Data;
using HeroEngine.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;

namespace HeroEngine.Web.Pages.Stats
{
    public class StatsModel : PageModel
    {
        private readonly HeroAnalytics _analytics;

        public List<AHero> TopHeroes { get; set; }
        public Dictionary<string, double> ClassDistribution { get; set; }
        public Dictionary<string, int> AbilityTypeCounts { get; set; }
        public List<BattleResult> BattleHistory { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ResultFilter { get; set; }

        public StatsModel()
        {
            var allHeroes = HeroRepository.GetAll();
            _analytics = new HeroAnalytics(allHeroes);
        }

        public void OnGet()
        {
            var allHeroes = HeroRepository.GetAll();

            TopHeroes = _analytics.GetTopHeroesByLevel(3).ToList();
            
            int total = allHeroes.Count;
            ClassDistribution = allHeroes
                .GroupBy(h => h.GetType().Name)
                .ToDictionary(g => g.Key, g => (double)g.Count() / total * 100);

            AbilityTypeCounts = allHeroes.OfType<Mage>()
                .SelectMany(m => m.AbilityDictionary.Values)
                .GroupBy(a => a.Type.ToString())
                .ToDictionary(g => g.Key, g => g.Count());

            string path = Path.Combine(Directory.GetCurrentDirectory(), "Data", "combat_stats.csv");
            BattleHistory = CsvStatsWriter.LoadLastCombatsManually(path);

            if (!string.IsNullOrEmpty(ResultFilter))
            {
                BattleHistory = BattleHistory
                    .Where(b => b.Result.Contains(ResultFilter, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
        }
    }
}
