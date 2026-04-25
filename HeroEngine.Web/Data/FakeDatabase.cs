using HeroEngine.Core.Models;

namespace HeroEngine.Web.Data
{
    public class FakeDatabase
    {


        public static List<AHero> Heroes { get; set; } = new List<AHero>();
         static FakeDatabase()
        {
            Heroes.Add(new Warrior("Warrior", "Hyaaa"));
            Heroes.Add(new Mage("Mage"));
            Heroes.Add(new Rogue("Rogue", 10));
        }
    }
}
