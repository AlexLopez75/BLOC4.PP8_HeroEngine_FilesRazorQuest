using BLOC4.PP8_HeroEngine_FilesRazorQuest.models;
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

        [BindProperty]
        public string? BattleCry { get; set; }

        [BindProperty]
        public int? NumDaggers { get; set; } = 1;

        public List<string> AvailableClasses { get; set; } = new List<string> { "Warrior", "Mage", "Rogue" };

        public IActionResult OnPost() 
        {
            if (!ModelState.IsValid) return Page();

            AHero newHero = null;
            
            switch (SelectedClass)
            {
                case "Warrior": newHero = new Warrior(Name, string.IsNullOrEmpty(BattleCry) ? "For the honor!" : BattleCry); 
                    break;
                case "Mage": Mage newMage = new Mage(Name);
                    Ability fireball = new Ability(RarityType.COMMON, "Fireball", AbilityType.Attack);
                    Ability magicShield = new Ability(RarityType.RARE, "Magic Shield", AbilityType.Defense);
                    Ability heal = new Ability(RarityType.COMMON, "Heal", AbilityType.Healing);

                    newMage.EquipAbility(fireball);
                    newMage.EquipAbility(magicShield);
                    newMage.EquipAbility(heal);
                    newHero = newMage;
                    break;
                case "Rogue": newHero = new Rogue(Name, NumDaggers ?? 1); 
                    break;
            }

            if (newHero != null)
            {
                HeroEngine.Core.Data.HeroRepository.Add(newHero);
            }

            return RedirectToPage("/Heroes/Index");
        }
    }
}
