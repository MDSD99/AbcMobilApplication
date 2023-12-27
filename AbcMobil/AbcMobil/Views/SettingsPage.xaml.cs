using AbcMobil.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AbcMobil.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        SettingsViewModel viewModel;
        public SettingsPage()
        {
            InitializeComponent();
            viewModel = new SettingsViewModel();
            BindingContext = viewModel;
        }
    }
}