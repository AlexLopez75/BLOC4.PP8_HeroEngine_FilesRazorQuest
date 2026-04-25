using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeroEngine.Web.Pages.Files
{
    public class FilesModel : PageModel
    {
        public bool LogExists { get; set; }
        public void OnGet()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "battleLog.txt");
            LogExists = System.IO.File.Exists(filePath);
        }

        public IActionResult OnPostDownloadLog()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "battleLog.txt");
            if (System.IO.File.Exists(filePath))
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
                
                return File(fileBytes, "text/plain", "BattleLog.txt");
            }

            return RedirectToPage();
        }
    }
}
