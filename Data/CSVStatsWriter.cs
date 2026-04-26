using GestionFicheros.CSVParsing;
using HeroEngine.Core.Combat;
using HeroEngine.Core.Models;
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
    }
}