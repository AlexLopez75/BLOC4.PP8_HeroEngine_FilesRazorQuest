using BLOC4.PP8_HeroEngine_FilesRazorQuest.Combat;
using GestionFicheros.CSVParsing;
using HeroEngine.Core.Combat;
using HeroEngine.Core.Models;
using System.Globalization;
using System.IO;

namespace HeroEngine.Core.Data
{
    public static class CsvStatsWriter
    {
        public static void AppendCombatStats(BattleResult result)
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Data");

            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

            string filePath = Path.Combine(folderPath, "combat_stats.csv");

            CSVManager.Append(filePath, result);
        }

        public static List<BattleResult> LoadLastCombatsManually(string path)
        {
            var lastCombats = new List<BattleResult>();

            if (!System.IO.File.Exists(path)) return lastCombats;

            string[] lines = System.IO.File.ReadAllLines(path);

            var dataLines = lines.Skip(1).Where(l => !string.IsNullOrWhiteSpace(l)).ToList();

            var last10 = dataLines.TakeLast(10).Reverse().ToList();

            foreach (var line in last10)
            {
                string[] cols = line.Split(';');
                if (cols.Length >= 7)
                {
                    lastCombats.Add(new BattleResult
                    {
                        Date = DateTime.Parse(cols[0], CultureInfo.InvariantCulture),
                        ParticipatingHeroes = cols[1],
                        Enemies = cols[2],
                        Result = cols[3],
                        TotalRounds = int.Parse(cols[4]),
                        TotalDamageDealt = int.Parse(cols[5]),
                        MostEffectiveHero = cols[6]
                    });
                }
            }
            return lastCombats;
        }
    }
}