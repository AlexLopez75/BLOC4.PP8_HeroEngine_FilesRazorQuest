using BLOC4.PP8_HeroEngine_FilesRazorQuest.models;

namespace HeroEngine.Core.Models;

public class Ability
{
    public const string toStringMSG = "{0, -14} {1, -18} | Type: {2, -8} | Cost: {3, 2} mana";
    
    public RarityType Rarity { get; init; }
    public string Name { get; init; }
    public AbilityType Type { get; init; }
    public int Cost { get; init; }
    public int AbilityPower { get; set; }

    public Ability(RarityType rarity, string name, AbilityType type)
    {
        Rarity = rarity;
        Name = name;
        Type = type;
        (Cost, AbilityPower) = rarity switch
        {
            RarityType.COMMON => (5, 25),
            RarityType.RARE => (15, 40),
            RarityType.EPIC => (25, 65),
            RarityType.LEGENDARY => (40, 95),
            _ => (0, 0) //Default value
        };
    }
    
    public override string ToString() => String.Format(toStringMSG, $"[{Rarity}]", Name, Type, Cost);
}