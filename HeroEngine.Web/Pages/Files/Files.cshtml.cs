using HeroEngine.Core.Combat;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;

namespace HeroEngine.Web.Pages.Files
{
    public class FilesModel : PageModel
    {
        public bool LogExists { get; set; }
        public bool CsvExists { get; set; }
        public List<BattleResult> LastCombats { get; set; } = new List<BattleResult>();

        public void OnGet()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "battleLog.txt");
            LogExists = System.IO.File.Exists(filePath);

            string csvPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "combat_stats.csv");
            CsvExists = System.IO.File.Exists(csvPath);

            if (CsvExists)
            {
                LoadLastCombatsManually(csvPath);
            }
        }

        private void LoadLastCombatsManually(string path)
        {
            string[] lines = System.IO.File.ReadAllLines(path);

            var dataLines = lines.Skip(1).Where(l => !string.IsNullOrWhiteSpace(l)).ToList();

            var last10 = dataLines.TakeLast(10).Reverse().ToList();

            foreach (var line in last10)
            {
                string[] cols = line.Split(';');
                if (cols.Length >= 7)
                {
                    LastCombats.Add(new BattleResult
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
        }

        public IActionResult OnGetDownloadJson()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "heroes.json");

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("The JSON file was not found.");
            }
            return PhysicalFile(filePath, "application/json", "heroes.json");
        }

        public IActionResult OnPostDownloadLog()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "battleLog.txt");
            if (System.IO.File.Exists(filePath))
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
                
                return File(fileBytes, "text/plain", "BattleLog.txt");
            }

            return RedirectToPage();
        }

        public IActionResult OnPostDownloadCsv()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "combat_stats.csv");
            if (System.IO.File.Exists(filePath))
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
                return File(fileBytes, "text/csv", "combat_stats.csv");
            }
            return RedirectToPage();
        }
    }
}
