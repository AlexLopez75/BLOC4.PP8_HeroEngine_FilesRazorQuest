using HeroEngine.Core.Models;
using HeroEngine.Core.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeroEngine.Web.Pages.Heroes
{
    public class IndexModel : PageModel
    {
        public List<AHero> HeroList { get; set; }
        public void OnGet()
        {
            HeroList = HeroRepository.GetAll();
        }

        public IActionResult OnPostDelete(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                HeroRepository.Delete(name);
            }

            return RedirectToPage();
        }
    }
}
