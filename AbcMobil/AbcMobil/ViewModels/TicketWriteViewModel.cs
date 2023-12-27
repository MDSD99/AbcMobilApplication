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
    public class TicketWriteViewModel : BaseViewModel
    {
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
        public ICommand WriteCommand { get;}
        public TicketWriteViewModel()
        {
            WriteCommand = new Command(OnWrite);
        }
        private async void OnWrite()
        {
            try
            {
                int dell = SerialNumber.IndexOf('-');
                if(dell != -1)
                    SerialNumber = SerialNumber.Substring(0,8)+SerialNumber.Substring(9,4);
                dell = SerialNumber.IndexOf('*');
                if (dell != -1)
                    SerialNumber = SerialNumber.Remove(dell).Trim();
                TerminalResult terminal=App.uhfService.WriteSerialNumber(SerialNumber.Trim(),RfidSettings.Instance.TicketReadPower,RfidSettings.Instance.TicketWritePower);
                if (terminal.Result)
                {
                    await PopupNavigation.Instance.PushAsync(new MessagePopup("Başarılı", terminal.Data.ToString()));
                }
                else if (terminal.ExceptionResult)
                    await PopupNavigation.Instance.PushAsync(new MessagePopup("Hata", terminal.Message));
                else
                    await PopupNavigation.Instance.PushAsync(new MessagePopup("Uyarı", terminal.Message));
            }
            catch 
            {
                await PopupNavigation.Instance.PushAsync(new MessagePopup("Hata", "Lütfen bağlantı ayarlarınızı kontrol edin!"));
            }
        }
    }
}
