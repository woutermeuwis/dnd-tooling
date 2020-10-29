using CharacterSheet.Core.Models;
using CharacterSheet.Core.Services.Interface;

namespace CharacterSheet.Core.Services
{
    public class SpellMacroService : ISpellMacroService
    {
        public string GenerateMacroFromSpell(Spell spell)
        {
            return "Yay DI works";
        }
    }
}
