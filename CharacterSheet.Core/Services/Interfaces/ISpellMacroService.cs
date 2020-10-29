using CharacterSheet.Core.Models;

namespace CharacterSheet.Core.Services.Interface
{
    public interface ISpellMacroService
    {
        string GenerateMacroFromSpell(Spell spell);
    }
}
