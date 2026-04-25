using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeroEngine.Web.Pages.Files
{
    public class FilesModel : PageModel
    {
        public bool LogExists { get; set; }
        public void OnGet()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "battleLog.txt");
            LogExists = System.IO.File.Exists(filePath);
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
    }
}
