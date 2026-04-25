using BLOC4.PP8_HeroEngine_FilesRazorQuest.models;
using HeroEngine.Core.Models;

namespace HeroEngine.Core.Data
{
    public class HeroRepository
    {
        private static readonly string _path = Path.Combine(Directory.GetCurrentDirectory(), "Data", "heroes.json");
        private static List<AHero> _heroesInMemory = new List<AHero>();

        public static void LoadAll()
        {
            var dtos = JsonManager.Read<HeroDTO>(_path);
            _heroesInMemory = dtos.Select(dto => MapToEntity(dto)).ToList();
        }

        public static void SaveAll(IEnumerable<AHero> heroes)
        {
            var dtos = heroes.Select(h => MapToDTO(h)).ToList();
            JsonManager.Write(_path, dtos);
            _heroesInMemory = heroes.ToList();
        }

        public static void Add(AHero hero)
        {
            _heroesInMemory.Add(hero);
            JsonManager.Append(_path, MapToDTO(hero));
        }

        public static void Delete(string name)
        {
            _heroesInMemory.RemoveAll(h => h.Name == name);
            SaveAll(_heroesInMemory);
        }

        public static List<AHero> GetAll() => _heroesInMemory;

        private static HeroDTO MapToDTO(AHero hero)
        {
            var dto = new HeroDTO
            {
                Name = hero.Name,
                Type = hero.GetType().Name,
                Level = hero.Level,
                MaxHp = hero.MaxHp,
                Power = hero.Power,
                Defense = hero.Defense,
            };

            if (hero is Warrior w)
            {
                dto.Armor = w.Armor;
                dto.BattleCry = w.BattleCry;

            }
            else if (hero is Mage m)
            {
                dto.MaxMana = m.MaxMana;
                dto.ArchLevel = m.ArchLevel;
                dto.Abilities = m.AbilityDictionary.Values.Select(a => new AbilityDTO
                {
                    Name = a.Name,
                    Type = a.Type.ToString(),
                    Rarity = a.Rarity.ToString(),
                    Cost = a.Cost
                }).ToList();
            }
            else if (hero is Rogue r)
            {
                dto.MultDamage = r.MultDamage;
                dto.NumDaggers = r.NumDaggers;
            }

            return dto;
        }

        public static AHero MapToEntity(HeroDTO dto)
        {
            AHero hero;

            switch (dto.Type)
            {
                case "Warrior":
                    hero = new Warrior(dto.Name, dto.BattleCry ?? "Hyaaa");
                    ((Warrior)hero).Armor = dto.Armor;
                    break;
                case "Mage":
                    hero = new Mage(dto.Name);
                    ((Mage)hero).MaxMana = dto.MaxMana;
                    ((Mage)hero).ArchLevel = dto.ArchLevel;
                    
                    if (dto.Abilities != null)
                    {
                        foreach (var abilityDto in dto.Abilities)
                        {
                            Ability ability = new Ability(
                                Enum.Parse<RarityType>(abilityDto.Rarity),
                                abilityDto.Name,
                                Enum.Parse<AbilityType>(abilityDto.Type)
                                
                            );

                            abilityDto.Cost = ability.Cost;
                            abilityDto.AbilityPower = ability.AbilityPower;
                            ((Mage)hero).EquipAbility(ability);
                        }
                    }
                    break;
                case "Rogue":
                    hero = new Rogue(dto.Name, dto.NumDaggers);
                    ((Rogue)hero).MultDamage = dto.MultDamage;
                    break;

                default:
                    hero = new Warrior(dto.Name, "¡Error!"); // If type is unknown, create a default warrior with an error battle cry
                    break;
            }

            hero.Level = dto.Level;
            hero.MaxHp = dto.MaxHp;
            hero.CurrentHp = dto.MaxHp;

            return hero;
        }
    }
}
