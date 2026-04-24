using HeroEngine.Core.Models;

namespace BLOC4.PP8_HeroEngine_FilesRazorQuest.models;

/// <summary>
/// Defines the contract for characters that can equip, list, and cast abilities.
/// </summary>
public interface IAbility
{
    /// <summary>
    /// Casts the specified ability, applying its effects and consuming any required resources.
    /// </summary>
    /// <param name="ability">The <see cref="Ability"/> to cast.</param>
    public int CastAbility(Ability ability);
    
    /// <summary>
    /// Generates a formatted string of the provided abilities.
    /// </summary>
    /// <param name="listAbilities">The list of <see cref="Ability"/> objects to display.</param>
    /// <returns>A formatted string listing the abilities.</returns>
    public string ListAbilities(List<Ability> listAbilities);
    
    /// <summary>
    /// Equips a new ability to the character.
    /// </summary>
    /// <param name="ability">The <see cref="Ability"/> to equip.</param>
    public void EquipAbility(Ability ability);
}