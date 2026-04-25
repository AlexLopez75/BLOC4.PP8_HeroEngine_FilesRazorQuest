using Microsoft.AspNetCore.Mvc.RazorPages;
using HeroEngine.Core.Models;
using HeroEngine.Core.Data;

namespace HeroEngine.Web.Pages
{
    public class IndexModel : PageModel
    {
        public List<AHero> HeroList { get; set; }
        public int HeroCount { get; set; }

        public void OnGet()
        {
            HeroList = HeroRepository.GetAll();
            HeroCount = HeroList.Count;
        }
    }
}
