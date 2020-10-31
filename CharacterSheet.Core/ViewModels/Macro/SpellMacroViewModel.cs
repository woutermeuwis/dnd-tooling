using CharacterSheet.Core.Enums;
using CharacterSheet.Core.Extensions;
using CharacterSheet.Core.Helpers;
using CharacterSheet.Core.Models;
using CharacterSheet.Core.Services.Interface;
using CharacterSheet.Core.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CharacterSheet.Core.ViewModels.Macro
{
    public class SpellMacroViewModel : BaseViewModel
    {
        private readonly ISpellMacroService _spellMacroService;

        public ObservableCollection<SpellRange> PossibleRanges { get; }

        public string SpellName { get; set; }
        public string School { get; set; }
        public string Components { get; set; }
        public string CastingTime { get; set; }
        public SpellRange Range { get; set; }
        public string CustomRange { get; set; }
        public string Duration { get; set; }
        public string SavingThrow { get; set; }

        public string Macro { get; set; }

        public bool ShouldShowCustomRange => Range == SpellRange.Custom;

        public ICommand GenerateMacroCommand { get; }

        public SpellMacroViewModel(ISpellMacroService spellMacroService)
        {
            _spellMacroService = spellMacroService;

            PossibleRanges = new ObservableCollection<SpellRange>();

            GenerateMacroCommand = new RelayCommand(GenerateMacro);
        }

        public override void Prepare()
        {
            base.Prepare();

            var ranges = Enum.GetValues(typeof(SpellRange)).Cast<SpellRange>().ToList();
            PossibleRanges.Update(ranges);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

        }

        private void GenerateMacro()
        {
            var range = Range == SpellRange.Custom ? CustomRange : Range.ToString();

            var spell = new Spell
            {
                SpellName = SpellName,
                School = School,
                Components = Components,
                CastingTime = CastingTime,
                Range = range,
                Duration = Duration,
                SavingThrow = SavingThrow
            };

            Macro = _spellMacroService.GenerateMacroFromSpell(spell);
            OnPropertyChanged(nameof(Macro));
        }

    }
}
