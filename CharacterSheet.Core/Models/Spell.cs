using CharacterSheet.Core.Enums;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterSheet.Core.Models
{
    [AddINotifyPropertyChangedInterface]
    public class Spell
    {
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
    }
}
