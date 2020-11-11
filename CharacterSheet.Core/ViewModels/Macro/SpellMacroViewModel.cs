using CharacterSheet.Core.Enums;
using CharacterSheet.Core.Extensions;
using CharacterSheet.Core.Helpers;
using CharacterSheet.Core.Models;
using CharacterSheet.Core.Services.Interface;
using CharacterSheet.Core.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CharacterSheet.Core.ViewModels.Macro
{
	public class SpellMacroViewModel : BaseViewModel
	{
		private readonly ISpellMacroService _spellMacroService;

		public ObservableCollection<Spell> Spells { get; }
		public Spell SelectedSpell { get; set; }

		public ObservableCollection<SpellRange> PossibleRanges { get; }
		public ObservableCollection<SaveKind> PossibleSaveKinds { get; }
		public ObservableCollection<SpellResistance> PossibleResistances { get; }

		#region Spell Properties

		public string SpellName
		{
			get => SelectedSpell?.SpellName;
			set => SelectedSpell.SpellName = value;
		}

		public string School
		{
			get => SelectedSpell?.School;
			set => SelectedSpell.School = value;
		}

		public string Components
		{
			get => SelectedSpell?.Components;
			set => SelectedSpell.Components = value;
		}

		public string CastingTime
		{
			get => SelectedSpell?.CastingTime;
			set => SelectedSpell.CastingTime = value;
		}

		public SpellRange Range
		{
			get => SelectedSpell?.Range ?? SpellRange.Unknown;
			set => SelectedSpell.Range = value;
		}

		public string CustomRange
		{
			get => SelectedSpell?.CustomRange;
			set => SelectedSpell.CustomRange = value;
		}

		public string Target
		{
			get => SelectedSpell?.Target;
			set => SelectedSpell.Target = value;
		}

		public string Area
		{
			get => SelectedSpell?.Area;
			set => SelectedSpell.Area = value;
		}

		public string Duration
		{
			get => SelectedSpell?.Duration;
			set => SelectedSpell.Duration = value;
		}

		public SaveKind FortitudeSave
		{
			get => SelectedSpell?.FortitudeSave ?? SaveKind.None;
			set => SelectedSpell.FortitudeSave = value;
		}

		public string CustomFortitudeSave
		{
			get => SelectedSpell?.CustomFortitudeSave;
			set => SelectedSpell.CustomFortitudeSave = value;
		}

		public SaveKind ReflexSave
		{
			get => SelectedSpell?.ReflexSave ?? SaveKind.None;
			set => SelectedSpell.ReflexSave = value;
		}

		public string CustomReflexSave
		{
			get => SelectedSpell?.CustomReflexSave;
			set => SelectedSpell.CustomReflexSave = value;
		}

		public SaveKind WillSave
		{
			get => SelectedSpell?.WillSave ?? SaveKind.None;
			set => SelectedSpell.WillSave = value;
		}

		public string CustomWillSave
		{
			get => SelectedSpell?.CustomWillSave;
			set => SelectedSpell.CustomWillSave = value;
		}

		public SpellResistance SpellResistance
		{
			get => SelectedSpell?.SpellResistance ?? SpellResistance.No;
			set => SelectedSpell.SpellResistance = value;
		}

		public string Notes
		{
			get => SelectedSpell?.Notes;
			set => SelectedSpell.Notes = value;
		}

		#endregion

		public string Macro { get; set; }

		public bool ShouldShowCustomRange => Range == SpellRange.Custom;
		public bool ShouldShowCustomFortitudeSave => FortitudeSave == SaveKind.Custom;
		public bool ShouldShowCustomReflexSave => ReflexSave == SaveKind.Custom;
		public bool ShouldShowCustomWillSave => WillSave == SaveKind.Custom;
		public bool ShouldShowMacro => Macro.IsNotNullOrEmptyOrWhitespace();

		public ICommand GenerateMacroCommand { get; }
		public ICommand SaveCommand { get; }
		public ICommand AddCommand { get; }
		public ICommand ExportCommand { get; }
		public ICommand ImportCommand { get; }

		public SpellMacroViewModel(ISpellMacroService spellMacroService)
		{
			_spellMacroService = spellMacroService;

			Spells = new ObservableCollection<Spell>();
			PossibleRanges = new ObservableCollection<SpellRange>();
			PossibleSaveKinds = new ObservableCollection<SaveKind>();
			PossibleResistances = new ObservableCollection<SpellResistance>();

			GenerateMacroCommand = new RelayCommand(GenerateMacro);
			SaveCommand = new AsyncRelayCommand(SaveList);
			AddCommand = new RelayCommand(AddSpell);
			ExportCommand = new AsyncRelayCommand(ExportSpells);
			ImportCommand = new AsyncRelayCommand(ImportSpells);
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

		public override async Task Initialize()
		{
			await base.Initialize();
			await ReloadSpells();
		}

		private void GenerateMacro()
		{
			Macro = _spellMacroService.GenerateMacroFromSpell(SelectedSpell);
			OnPropertyChanged(nameof(Macro));
		}

		private async Task SaveList()
		{
			await _spellMacroService.SaveSpells(Spells);
		}

		private void AddSpell()
		{
			Spells.Add(new Spell() { SpellName = "New Spell" });
		}

		private async Task ExportSpells()
		{
			await SaveList();
			await _spellMacroService.Export();
		}

		private async Task ImportSpells()
		{
			await _spellMacroService.Import();
			await ReloadSpells();
		}

		private async Task ReloadSpells()
		{
			var spells = await _spellMacroService.GetSpells();
			Spells.Update(spells);
			SelectedSpell = Spells.FirstOrDefault() ?? new Spell();
		}
	}
}
