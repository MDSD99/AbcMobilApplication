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
    public class TagWriteViewModel:BaseViewModel
    {
        public TagWriteViewModel()
        {
            WriteCommand = new Command(OnWrite);
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
        public ICommand WriteCommand { get; }
        private async void OnWrite()
        {
            try
            {
                TerminalResult terminal = App.uhfService.WriteTag(SerialNumber,RfidSettings.Instance.TagReadPower,RfidSettings.Instance.TagWritePower);
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
