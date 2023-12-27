using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbcMobil.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AbcMobil.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TicketReadPage : ContentPage
    {
        private TicketReadViewModel viewModel;
        public TicketReadPage()
        {
            InitializeComponent();
            viewModel = new TicketReadViewModel();
            BindingContext= viewModel;
        }
    }
}