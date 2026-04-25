using HeroEngine.Core.Models;
using HeroEngine.Core.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace HeroEngine.Web.Pages.Heroes
{
    public class DetailModel : PageModel
    {
        public AHero Hero { get; set; }
        public void OnGet(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                Response.Redirect("/Heroes/Index");
                return;
            }

            Hero = HeroRepository.GetAll().FirstOrDefault(h => h.Name == name);

            if (Hero == null)
            {
                Response.Redirect("/Heroes/Index");
                return;
            }
        }
    }
}
