using HeroEngine.Core.Combat;
using HeroEngine.Core.Data;
using HeroEngine.Core.Models;
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
        
        [BindProperty]
        public GameConfig CurrentConfig { get; set; }

        public void OnGet()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "battleLog.txt");
            LogExists = System.IO.File.Exists(filePath);

            string csvPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "combat_stats.csv");
            CsvExists = System.IO.File.Exists(csvPath);

            if (CsvExists)
            {
                LastCombats = CsvStatsWriter.LoadLastCombatsManually(csvPath);
            }

            CurrentConfig = GameConfigManager.LoadConfig();
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
        public IActionResult OnPostSaveConfig()
        {
            GameConfigManager.SaveConfig(CurrentConfig);
            return RedirectToPage();
        }
    }
}
