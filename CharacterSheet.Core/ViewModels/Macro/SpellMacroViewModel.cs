using CharacterSheet.Core.Helpers;
using CharacterSheet.Core.ViewModels.Base;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CharacterSheet.Core.ViewModels.Macro
{
    public class SpellMacroViewModel : BaseViewModel
    {
        public string SpellName { get; set; }
        public string School { get; set; }
        public string Components { get; set; }
        public string CastingTime { get; set; }
        public string Range { get; set; }
        public string Duration { get; set; }
        public string SavingThrow { get; set; }

        public string Macro { get; set; }

        public ICommand GenerateMacroCommand { get; }

        public SpellMacroViewModel()
        {
            GenerateMacroCommand = new RelayCommand(GenerateMacro);
        }

        private void GenerateMacro()
        {
            var sb = new StringBuilder();
            sb.AppendLine(SpellName)
                .AppendLine(School)
                .AppendLine(Components)
                .AppendLine(CastingTime)
                .AppendLine(Range)
                .AppendLine(Duration)
                .AppendLine(SavingThrow);

            Macro = sb.ToString();
        }

    }
}
