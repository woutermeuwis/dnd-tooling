using Windows.UI.Xaml.Controls;
using CharacterSheet.Core.ViewModels.Macro;
using System.Threading.Tasks;

namespace CharacterSheet.Platform.UWP.Pages.Macro
{
    public sealed partial class SpellMacroPage : Page
    {
        public SpellMacroViewModel ViewModel { get; }

        public SpellMacroPage()
        {
            InitializeComponent();
            ViewModel = Autofac.Resolve<SpellMacroViewModel>();
            ViewModel.Prepare();
        }      
    }
}
