using System.Net.Http.Headers;
using BLOC3.PP7_HeroEngine.models;
using BLOC4.PP8_HeroEngine_FilesRazorQuest.Combat;
using BLOC4.PP8_HeroEngine_FilesRazorQuest.Data;
using BLOC4.PP8_HeroEngine_FilesRazorQuest.models;

namespace BLOC3.PP7_HeroEngine
{
    public class Program
    {
        public static void Main()
        {// --- 1. PREPARACIÓN DE LOS HÉROES ---
            Warrior grog = new Warrior("Grog", "¡Por la horda!");
            Mage merlin = new Mage("Merlin");
            
            // Equipamos al mago con magia ofensiva y curativa
            merlin.EquipAbility(new Ability(RarityType.EPIC, "Bola de Fuego", AbilityType.Attack)); // Cost: 25
            merlin.EquipAbility(new Ability(RarityType.COMMON, "Cura Menor", AbilityType.Healing)); // Cost: 5
            merlin.EquipAbility(new Ability(RarityType.RARE, "Aura Arcana", AbilityType.Support) { Cost = 30, AbilityPower = 15 });
            
            BattleLogger.Log("");
            
            // --- 2. PREPARACIÓN DE LOS ENEMIGOS ---
            // Recordatorio de stats: Elite(120 HP, 25 Pwr, 15 Def) | Minion(50 HP, 15 Pwr, 5 Def)
            Elite elite = new Elite("Elite Segfault");
            Minion minion = new Minion("Minion Bug");

            // --- 3. CREACIÓN DE BANDO Y MOTOR ---
            List<ACharacter> heroes = new List<ACharacter> { grog, merlin };
            List<ACharacter> enemies = new List<ACharacter> { elite, minion };

            BattleEngine engine = new BattleEngine(heroes, enemies);

            // --- 4. ¡QUE COMIENCE LA BATALLA! ---
            engine.StartBattle();
        }
    }
}