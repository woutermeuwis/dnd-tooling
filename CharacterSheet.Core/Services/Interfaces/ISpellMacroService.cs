using CharacterSheet.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CharacterSheet.Core.Services.Interface
{
    public interface ISpellMacroService
    {
        Task Initialize();
        Task<List<Spell>> GetSpells();
        string GenerateMacroFromSpell(Spell spell);
    }
}
