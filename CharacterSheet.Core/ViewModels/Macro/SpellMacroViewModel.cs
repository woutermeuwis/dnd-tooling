using CharacterSheet.Core.Enums;
using CharacterSheet.Core.Extensions;
using CharacterSheet.Core.Helpers;
using CharacterSheet.Core.Models;
using CharacterSheet.Core.Services.Interface;
using CharacterSheet.Core.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace CharacterSheet.Core.ViewModels.Macro
{
    public class SpellMacroViewModel : BaseViewModel
    {
        private readonly ISpellMacroService _spellMacroService;

        public ObservableCollection<SpellRange> PossibleRanges { get; }
        public ObservableCollection<SaveKind> PossibleSaveKinds { get; }
        public ObservableCollection<SpellResistance> PossibleResistances { get; }

        public string SpellName { get; set; }
        public string School { get; set; }
        public string Components { get; set; }
        public string CastingTime { get; set; }
        public SpellRange Range { get; set; }
        public string CustomRange { get; set; }
        public string Target { get; set; }
        public string Area { get; set; }
        public string Duration { get; set; }
        public SaveKind FortitudeSave { get; set; }
        public string CustomFortitudeSave { get; set; }
        public SaveKind ReflexSave { get; set; }
        public string CustomReflexSave { get; set; }
        public SaveKind WillSave { get; set; }
        public string CustomWillSave { get; set; }
        public SpellResistance SpellResistance { get; set; }
        public string Notes { get; set; }

        public string Macro { get; set; }

        public bool ShouldShowCustomRange => Range == SpellRange.Custom;
        public bool ShouldShowCustomFortitudeSave => FortitudeSave == SaveKind.Custom;
        public bool ShouldShowCustomReflexSave => ReflexSave == SaveKind.Custom;
        public bool ShouldShowCustomWillSave => WillSave == SaveKind.Custom;

        public ICommand GenerateMacroCommand { get; }

        public SpellMacroViewModel(ISpellMacroService spellMacroService)
        {
            _spellMacroService = spellMacroService;

            PossibleRanges = new ObservableCollection<SpellRange>();
            PossibleSaveKinds = new ObservableCollection<SaveKind>();
            PossibleResistances = new ObservableCollection<SpellResistance>();

            GenerateMacroCommand = new RelayCommand(GenerateMacro);
        }

        public override void Prepare()
        {
            base.Prepare();

            var ranges = Enum.GetValues(typeof(SpellRange)).Cast<SpellRange>();
            PossibleRanges.Update(ranges);

            var saveKinds = Enum.GetValues(typeof(SaveKind)).Cast<SaveKind>();
            PossibleSaveKinds.Update(saveKinds);

            var resistances = Enum.GetValues(typeof(SpellResistance)).Cast<SpellResistance>();
            PossibleResistances.Update(resistances);
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
                CustomRange = CustomRange,
                Target = Target,
                Area = Area,
                Duration = Duration,
                FortitudeSave = FortitudeSave,
                CustomFortitudeSave = CustomFortitudeSave,
                ReflexSave = ReflexSave,
                CustomReflexSave = CustomReflexSave,
                WillSave = WillSave,
                CustomWillSave = CustomWillSave,
                SpellResistance = SpellResistance,
                Notes = Notes
            };

            Macro = _spellMacroService.GenerateMacroFromSpell(spell);
            OnPropertyChanged(nameof(Macro));
        }
    }
}
