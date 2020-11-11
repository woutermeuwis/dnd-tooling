using Windows.UI.Xaml.Controls;
using CharacterSheet.Core.ViewModels.Macro;
using System.Threading.Tasks;
using AsyncAwaitBestPractices;

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
    }
}
