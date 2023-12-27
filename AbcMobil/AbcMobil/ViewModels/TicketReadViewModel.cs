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
    public class TicketReadViewModel:BaseViewModel
    {
        public TicketReadViewModel()
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
                TerminalResult terminal = App.uhfService.ReadSerialNumber(RfidSettings.Instance.TicketReadPower,RfidSettings.Instance.TicketWritePower);
                if (terminal.Result)
                {
                    SerialNumber = terminal.Data.ToString();
                }
                else if (terminal.ExceptionResult)
                    await PopupNavigation.Instance.PushAsync(new MessagePopup("Hata", terminal.Message));
                else
                    await PopupNavigation.Instance.PushAsync(new MessagePopup("Uyarı", terminal.Message));
            }
            catch
            {
                await PopupNavigation.Instance.PushAsync(new MessagePopup("Hata","Lütfen bağlantı ayarlarınızı kontrol edin!"));
            }
            finally
            {
               // App.uhfService.Close();
            }
        }
    }
}
