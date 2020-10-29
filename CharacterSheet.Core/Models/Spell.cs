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
        public string Range { get; set; }
        public string Duration { get; set; }
        public string SavingThrow { get; set; }
    }
}
