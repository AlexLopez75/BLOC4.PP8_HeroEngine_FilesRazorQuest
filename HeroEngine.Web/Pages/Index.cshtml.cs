using Microsoft.AspNetCore.Mvc.RazorPages;
using HeroEngine.Core.Models;

namespace HeroEngine.Web.Pages
{
    public class IndexModel : PageModel
    {
        public int HeroCount { get; set; }
        public List<string> HeroList { get; set; }

        public void OnGet()
        {
            // Simulamos unos datos por ahora (más adelante los leerás de tu carpeta Data)
            HeroCount = 3;
            HeroList = new List<string>
            {
                "Grog",
                "Merlí",
                "Shadow"

            };
        }
    }
}
