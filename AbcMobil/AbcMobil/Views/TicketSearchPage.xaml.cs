using AbcMobil.Models;
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
    public partial class TicketSearchPage : ContentPage
    {
        private readonly TicketSearchViewModel viewModel;
        public TicketSearchPage()
        {
            InitializeComponent();
            viewModel = new TicketSearchViewModel();
            BindingContext = viewModel;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.IsBusy = true;
        }
    }
}