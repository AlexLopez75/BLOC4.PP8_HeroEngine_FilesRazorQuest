using HeroEngine.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

            if (name == "Grog") Hero = new Warrior("Grog", "Hyaaa");
            else if (name == "Merlí") Hero = new Mage("Merlí");
            else if (name == "Shadow") Hero = new Rogue(name, 10);
        }
    }
}
