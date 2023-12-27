using AbcMobil.ViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AbcMobil.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WriterPage : ContentPage
    {
        WriterViewModel viewModel;
        public WriterPage()
        {
            InitializeComponent();
            viewModel = new WriterViewModel();
            BindingContext = viewModel;
        }
    }
}