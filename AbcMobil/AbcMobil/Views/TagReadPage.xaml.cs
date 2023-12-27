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
    public partial class TagReadPage : ContentPage
    {
        private readonly TagReadViewModel viewModel;
        public TagReadPage()
        {
            InitializeComponent();
            viewModel = new TagReadViewModel();
            BindingContext = viewModel; 
        }
    }
}