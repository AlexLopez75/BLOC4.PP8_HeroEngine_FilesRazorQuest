using HeroEngine.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace HeroEngine.Web.Pages.Heroes
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Name is obligatory")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Between 3 and 20 characters")]
        public string Name { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "You have to select a class")]
        public string SelectedClass { get; set; }

        public string BattleCry { get; set; }

        public List<string> AvailableClasses { get; set; } = new List<string> { "Warrior", "Mage", "Rogue" };

        public void OnGet() {}

        public IActionResult OnPost() 
        {
            if (!ModelState.IsValid) return Page();

            AHero newHero;
            switch (SelectedClass)
            {
                case "Warrior": newHero = new Warrior(Name, BattleCry); break;
                case "Mage": newHero = new Mage(Name); break;
                case "Rogue": newHero = new Rogue(Name, 10); break;
            }

            return RedirectToPage("/Heroes/Index");
        }
    }
}
