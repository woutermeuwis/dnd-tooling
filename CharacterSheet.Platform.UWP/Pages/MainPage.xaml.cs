using Windows.UI.Xaml.Controls;
using CharacterSheet.Core.ViewModels;
using CharacterSheet.Platform.UWP.Pages.Macro;

namespace CharacterSheet.Platform.UWP
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; }

        public MainPage()
        {
            InitializeComponent();
            ViewModel = Core.Autofac.Resolve<MainViewModel>();
        }

        private void GoToMacroGenerator(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MacroSelectionPage));
        }
    }
}
