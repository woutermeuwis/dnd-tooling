using CharacterSheet.Core.Models;
using CharacterSheet.Core.Services.Interface;
using System.Text;

namespace CharacterSheet.Core.Services
{
    public class SpellMacroService : ISpellMacroService
    {
        public string GenerateMacroFromSpell(Spell spell)
        {
            var sb = new StringBuilder("&");

            sb.Append("{template:default}");
            sb.Append(WrapTag("name", spell.SpellName));
            sb.Append(WrapTag("School", spell.School));
            sb.Append(WrapTag("Components", spell.Components));
            sb.Append(WrapTag("Casting Time", spell.CastingTime));
            sb.Append(WrapTag("Range", spell.Range));
            sb.Append(WrapTag("Duration", spell.Duration));
            sb.Append(WrapTag("Saving Throw", spell.SavingThrow));

            return sb.ToString();
        }

        private string WrapTag(string name, string value) => $" {{{{{name}={value}}}}}";
    }
}
