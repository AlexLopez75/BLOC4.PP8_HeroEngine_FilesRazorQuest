using HeroEngine.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeroEngine.Web.Pages.Heroes
{
    public class IndexModel : PageModel
    {
        public List<AHero> HeroList { get; set; }
        public void OnGet()
        {
            HeroList = new List<AHero>
            {
                new Warrior("Grog", "Hyaaa"),
                new Mage("Merlí"),
                new Rogue("Shadow", 10)
            };
        }
    }
}
