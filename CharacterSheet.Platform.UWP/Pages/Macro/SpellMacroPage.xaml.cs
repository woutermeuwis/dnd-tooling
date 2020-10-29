using Windows.UI.Xaml.Controls;
using CharacterSheet.Core.ViewModels.Macro;

namespace CharacterSheet.Platform.UWP.Pages.Macro
{
    public sealed partial class SpellMacroPage : Page
    {
        public SpellMacroViewModel ViewModel { get; }

        public SpellMacroPage()
        {
            InitializeComponent();
            ViewModel = Core.Autofac.Resolve<SpellMacroViewModel>();
        }
    }
}
