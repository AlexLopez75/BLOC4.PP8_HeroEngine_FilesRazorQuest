using System;
using System.Collections.Generic;
using System.Text;

namespace HeroEngine.Core.Combat
{
    public class BattleResult
    {
        public DateTime Date { get; set; }
        public string ParticipatingHeroes { get; set; }
        public string Enemies { get; set; }
        public string Result { get; set; }
        public int TotalRounds { get; set; }
        public int TotalDamageDealt { get; set; }
        public string MostEffectiveHero { get; set; }
    }
}
