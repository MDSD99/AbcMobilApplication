using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AbcMobil.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShellPage : Shell
    {
        public ShellPage()
        {
            InitializeComponent();
        }

        private async void MenuItem_Clicked(object sender, System.EventArgs e)
        {
            bool result= await Shell.Current.DisplayAlert("Uyarı", "Çıkmak istediğinize emin misiniz?", "Ok", "Cancel");
            if (result)
                Application.Current.Quit();
        }
    }
}