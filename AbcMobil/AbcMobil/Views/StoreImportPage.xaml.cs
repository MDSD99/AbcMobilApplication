using AbcMobil.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AbcMobil.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoreImportPage : ContentPage
    {
        StoreImportViewModel viewModel;
        public StoreImportPage()
        {
            InitializeComponent();
            viewModel = new StoreImportViewModel();
            BindingContext = viewModel;
        }
    }
}