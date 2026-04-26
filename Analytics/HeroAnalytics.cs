using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using BLOC4.PP8_HeroEngine_FilesRazorQuest.models;
using HeroEngine.Core.Models;

namespace HeroEngine.Core.Analytics
{
    public class HeroAnalytics
    {
        private readonly IEnumerable<AHero> _heroes;

        public HeroAnalytics(IEnumerable<AHero> heroes)
        {
            _heroes = heroes;
        }

        public IEnumerable<AHero> GetTopHeroesByLevel(int level)
        {
            return _heroes.OrderByDescending(h => h.Level).Take(level);
        }

        public IEnumerable<Ability> GetAbilitiesByRarity(RarityType rarity)
        {
            return _heroes.OfType<Mage>().SelectMany(m => m.AbilityDictionary.Values).Where(a => a.Rarity == rarity);
        }

        public IEnumerable<AHero> GetHeroesWithAbilityCount(int min)
        {
            return _heroes.OfType<Mage>().Where(h => h.AbilityDictionary.Count >= min);
        }

        public Dictionary<string, double> GetHeroDamagePerClass()
        {
            return _heroes.GroupBy(h => h.GetType().Name).ToDictionary(
                g => g.Key,
                g => g.Average(h => h.Power)
            );
        }

        public IEnumerable<AHero> SearchHeroesByName(string pattern)
        {
            return _heroes.Where(h => Regex.IsMatch(h.Name, pattern, RegexOptions.IgnoreCase));
        }
    }
}
