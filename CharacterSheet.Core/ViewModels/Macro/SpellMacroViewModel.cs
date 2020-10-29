using CharacterSheet.Core.Helpers;
using CharacterSheet.Core.Models;
using CharacterSheet.Core.Services.Interface;
using CharacterSheet.Core.ViewModels.Base;
using System.Windows.Input;

namespace CharacterSheet.Core.ViewModels.Macro
{
    public class SpellMacroViewModel : BaseViewModel
    {
        private readonly ISpellMacroService _spellMacroService;

        public string SpellName { get; set; }
        public string School { get; set; }
        public string Components { get; set; }
        public string CastingTime { get; set; }
        public string Range { get; set; }
        public string Duration { get; set; }
        public string SavingThrow { get; set; }

        public string Macro { get; set; }

        public ICommand GenerateMacroCommand { get; }

        public SpellMacroViewModel(ISpellMacroService spellMacroService)
        {
            _spellMacroService = spellMacroService;

            GenerateMacroCommand = new RelayCommand(GenerateMacro);
        }

        private void GenerateMacro()
        {
            var spell = new Spell
            {
                SpellName = SpellName,
                School = School,
                Components = Components,
                CastingTime = CastingTime,
                Range = Range,
                Duration = Duration,
                SavingThrow = SavingThrow
            };

            Macro = _spellMacroService.GenerateMacroFromSpell(spell);
            OnPropertyChanged(nameof(Macro));
        }

    }
}
