using AbcMobil.Models;
using AbcMobil.PopupViews;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AbcMobil.ViewModels
{
    public class TagReadViewModel:BaseViewModel
    {
        public TagReadViewModel()
        {
            ReadCommand = new Command(OnRead);
        }
        private string serialNumber;
        public string SerialNumber
        {
            get => serialNumber;
            set
            {
                serialNumber = value;
                OnPropertyChanged(nameof(SerialNumber));
            }
        }
        public ICommand ReadCommand { get; }
        private async void OnRead()
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new RfidPopup(2));
                
            }
            catch 
            {
                await PopupNavigation.Instance.PushAsync(new MessagePopup("Hata", "Lütfen bağlantı ayarlarınızı kontrol edin!"));
            }
        }
    }
}
