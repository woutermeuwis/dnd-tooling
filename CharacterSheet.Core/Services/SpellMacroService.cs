using CharacterSheet.Core.Enums;
using CharacterSheet.Core.Extensions;
using CharacterSheet.Core.Interfaces;
using CharacterSheet.Core.Models;
using CharacterSheet.Core.Services.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterSheet.Core.Services
{
	public class SpellMacroService : ISpellMacroService
	{
		private readonly IStorageService _storage;

		private readonly List<Spell> _spells;
		private bool _initialized;

		public SpellMacroService(IStorageService storageService)
		{
			_storage = storageService;
			_spells = new List<Spell>();
		}

		public async Task Initialize()
		{
			if (!_initialized)
			{
				await Load().ConfigureAwait(false);
				_initialized = true;
			}
		}

		public async Task<List<Spell>> GetSpells()
		{
			await Initialize().ConfigureAwait(false);
			return _spells
				.OrderBy(s => s.SpellName)
				.ToList();
		}

		public async Task SaveSpells(IEnumerable<Spell> spells)
		{
			_spells.Update(spells);
			await Save().ConfigureAwait(false);
		}

		public string GenerateMacroFromSpell(Spell spell)
		{
			if (spell == null)
				return string.Empty;

			var sb = new StringBuilder("&");

			sb.Append("{template:default}");

			if (spell.SpellName.IsNotNullOrEmptyOrWhitespace())
				sb.Append(WrapTag("name", spell.SpellName));

			if (spell.School.IsNotNullOrEmptyOrWhitespace())
				sb.Append(WrapTag("School", spell.School));

			if (spell.Components.IsNotNullOrEmptyOrWhitespace())
				sb.Append(WrapTag("Components", spell.Components));

			if (spell.CastingTime.IsNotNullOrEmptyOrWhitespace())
				sb.Append(WrapTag("Casting Time", spell.CastingTime));

			if (spell.Range == SpellRange.Custom)
				sb.Append(WrapTag("Range", spell.CustomRange));
			else if (spell.Range != SpellRange.Unknown)
				sb.Append(WrapTag("Range", spell.Range.ToString()));

			if (spell.Target.IsNotNullOrEmptyOrWhitespace())
				sb.Append(WrapTag("Target", spell.Target));

			if (spell.Area.IsNotNullOrEmptyOrWhitespace())
				sb.Append(WrapTag("Area", spell.Area));

			if (spell.Duration.IsNotNullOrEmptyOrWhitespace())
				sb.Append(WrapTag("Duration", spell.Duration));

			if (spell.FortitudeSave == SaveKind.None && spell.ReflexSave == SaveKind.None && spell.WillSave == SaveKind.None)
			{
				sb.Append(WrapTag("Saving Throw", "None"));
			}
			else
			{
				var saveBuilder = new StringBuilder();

				// fortitude
				if (spell.FortitudeSave == SaveKind.Custom && spell.CustomFortitudeSave.IsNotNullOrEmptyOrWhitespace())
				{
					saveBuilder
						.Append("Fortitude ")
						.Append(spell.CustomFortitudeSave)
						.Append(";");
				}
				else if (spell.FortitudeSave != SaveKind.None)
				{
					saveBuilder
						.Append("Fortitude ")
						.Append(spell.FortitudeSave)
						.Append(";");
				}

				// reflex
				if (spell.ReflexSave == SaveKind.Custom && spell.CustomReflexSave.IsNotNullOrEmptyOrWhitespace())
				{
					saveBuilder
						.Append("Reflex ")
						.Append(spell.CustomReflexSave)
						.Append(";");
				}
				else if (spell.ReflexSave != SaveKind.None)
				{
					saveBuilder
						.Append("Reflex ")
						.Append(spell.ReflexSave)
						.Append(";");
				}


				// will
				if (spell.WillSave == SaveKind.Custom && spell.CustomWillSave.IsNotNullOrEmptyOrWhitespace())
				{
					saveBuilder
						.Append("Will ")
						.Append(spell.CustomWillSave)
						.Append(";");
				}
				else if (spell.WillSave != SaveKind.None)
				{
					saveBuilder
						.Append("Will ")
						.Append(spell.WillSave)
						.Append(";");
				}

				sb.Append(WrapTag("Saving Throw", saveBuilder.ToString()));
			}

			switch (spell.SpellResistance)
			{
				case SpellResistance.Yes:
					sb.Append(WrapTag("Spell Resistance", "Yes"));
					break;
				case SpellResistance.Harmless:
					sb.Append(WrapTag("Spell Resistance", "Yes (Harmless)"));
					break;
			}

			if (spell.Notes.IsNotNullOrEmptyOrWhitespace())
			{
				sb.AppendLine();
				sb.Append(spell.Notes);
			}

			return sb.ToString();
		}


		#region private helpers

		private async Task Load()
		{
			var spellsJson = await _storage.Fetchdata(Constants.Files.Spells).ConfigureAwait(false);
			if (spellsJson.IsNotNullOrEmptyOrWhitespace())
			{
				var spells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Spell>>(spellsJson);
				_spells.Update(spells);
			}
		}

		private async Task Save()
		{
			var json = Newtonsoft.Json.JsonConvert.SerializeObject(_spells);
			await _storage.StoreData(Constants.Files.Spells, json);
		}

		private string WrapTag(string name, string value) => $" {{{{{name}={value}}}}}";

		#endregion
	}
}
