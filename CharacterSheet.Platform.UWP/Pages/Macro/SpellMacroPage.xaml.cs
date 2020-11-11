using Windows.UI.Xaml.Controls;
using CharacterSheet.Core.ViewModels.Macro;
using System.Threading.Tasks;
using AsyncAwaitBestPractices;
using CharacterSheet.Core.Models;

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
            ViewModel.Initialize().SafeFireAndForget();

            var applicationView = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView();
        }

        private void DeleteSpell_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var btn = e.OriginalSource as Button;
            if (btn?.DataContext is Spell spell)
                ViewModel.DeleteCommand.Execute(spell);
        }
    }
}
