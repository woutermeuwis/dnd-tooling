using CharacterSheet.Core.ViewModels.Macro;
using Windows.UI.Xaml.Controls;

namespace CharacterSheet.Platform.UWP.Pages.Macro
{
    public sealed partial class MacroSelectionPage : Page
    {
        public MacroSelectionViewModel ViewModel { get; }

        public MacroSelectionPage() : base()
        {
            InitializeComponent();
            ViewModel = Autofac.Resolve<MacroSelectionViewModel>();
        }

        private void GoToSpellMacroGenerator(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SpellMacroPage));
        }
    }
}
