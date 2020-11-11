using CharacterSheet.Core.Enums;
using Newtonsoft.Json;
using PropertyChanged;
using System;

namespace CharacterSheet.Core.Models
{
    [AddINotifyPropertyChangedInterface]
    public class Spell
    {
        public Guid Id { get; set; }
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

        public Spell()
        {
            Id = Guid.NewGuid();
        }

        [JsonConstructor]
        public Spell(Guid id, string spellName, string school, string components, string castingTime, SpellRange range, string customRange, string target, string area, string duration,
            SaveKind fortitudeSave, string customFortitudeSave, SaveKind reflexSave, string customReflexSave, SaveKind willSave, string customWillSave, SpellResistance spellResistance, string notes)
        {
            Id = id;
            SpellName = spellName;
            School = school;
            Components = components;
            CastingTime = castingTime;
            Range = range;
            CustomRange = customRange;
            Target = target;
            Area = area;
            Duration = duration;
            FortitudeSave = fortitudeSave;
            CustomFortitudeSave = customFortitudeSave;
            ReflexSave = reflexSave;
            CustomReflexSave = customReflexSave;
            WillSave = willSave;
            CustomWillSave = customWillSave;
            SpellResistance = spellResistance;
            Notes = notes;
        }
    }
}
