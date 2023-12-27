using AbcMobil.Helper;
using AbcMobil.Models;
using AbcMobil.PopupViews;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AbcMobil.ViewModels
{
    public class WriterViewModel : BaseViewModel
    {
        private string tagNo;
        public string TagNo
        {
            get => tagNo == null ? "" : tagNo.Trim();
            set
            {
                tagNo = value;
                OnPropertyChanged(nameof(TagNo));
            }
        }
        private string readCard = "Kart Okut";
        public string ReadCard
        {
            get => readCard;
            set
            {
                readCard = value;
                OnPropertyChanged(nameof(ReadCard));
            }
        }
        public ObservableCollection<TagInfo> SeriNoList { get; }
        public ICommand ReadCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand MatchCommand { get; }
        public ICommand SearchTagCommand { get; }
        public WriterViewModel()
        {
            SeriNoList = new ObservableCollection<TagInfo>();
            //SearchTagCommand = new Command(async (tag) => await OnSearchTag(tag));
            ReadCommand = new Command(async () => await OnRead());
            ClearCommand = new Command(OnClear);
            MatchCommand = new Command(async () => await OnMatch());
        }
        //private async Task OnSearchTag(object tag)
        //{
        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        private async Task OnRead()
        {
            if (ReadCard == "Kart Okut")
                ReadCard = "Okutucuyu Durdur";
            else
                ReadCard = "Kart Okut";
            while(ReadCard!="Kart Okut")
            {
                try
                {
                    TerminalResult result = await App.uhfService.InventorySerialNumber();
                    //if (result.DeviceID != App.DeviceID) continue;
                    if (result.Result)
                    {
                        SeriNoList.Clear();
                        foreach (TagInfo item in (IList<TagInfo>)result.Data)
                        {
                            SeriNoList.Add(item);
                        }
                    }
                    else if (result.ExceptionResult)
                    {
                        await PopupNavigation.Instance.PushAsync(new MessagePopup("Hata", result.Message));
                        ReadCard = "Kart Okut";
                    }
                }
                catch 
                {
                    await PopupNavigation.Instance.PushAsync(new MessagePopup("Hata", "Lütfen bağlantı ayarlarınızı kontrol edin!"));
                    ReadCard = "Kart Okut";
                }
            }
        }
        private async Task OnWrite()
        {
            string hexm = TagNo.Trim();
            int dell = TagNo.Trim().IndexOf("-");
            if (dell != -1)
                hexm = hexm.Remove(dell, 1);
            dell = TagNo.Trim().IndexOf("*");
            if (dell != -1)
                hexm = hexm.Remove(dell, 1);
            hexm = hexm.Trim();
            try
            {
                Convert.ToInt64(hexm);
                if (hexm.Length == 12)
                {
                    TerminalResult result = App.uhfService.WriteSerialNumber(TagNo.Trim(),RfidSettings.Instance.TicketReadPower,RfidSettings.Instance.TicketWritePower);
                    if (result.Result)
                        await PopupNavigation.Instance.PushAsync(new MessagePopup("Başarılı", result.Message));
                    else
                        await PopupNavigation.Instance.PushAsync(new MessagePopup("Hata", result.Message));
                }
                else
                    await PopupNavigation.Instance.PushAsync(new MessagePopup("Uyarı", "Lütfen seri numarası giriniz!"));
            }
            catch
            {
                await PopupNavigation.Instance.PushAsync(new MessagePopup("Uyarı", "Lütfen seri numarası giriniz!"));
            }
        }
        private void OnClear()
        {
            TagNo = "";
            SeriNoList.Clear();
            App.uhfService.Clear();
        }
        private async Task OnMatch()
        {
            try
            {
                string hexm = TagNo.Trim();
                int dell = TagNo.Trim().IndexOf("-");
                if (dell != -1)
                    hexm = hexm.Remove(dell, 1);
                dell = TagNo.Trim().IndexOf("*");
                if (dell != -1)
                    hexm = hexm.Remove(dell, 1);
                try
                {
                    Convert.ToInt64(hexm);
                    if (hexm.Length == 12)
                    {
                        byte[] data = ConvertHelper.TextToBytes(TagNo.Trim());
                        byte[] password = new byte[4];
                        password[0] = 0x00;
                        password[1] = 0x00;
                        password[2] = 0x00;
                        password[3] = 0x00;
                        TerminalResult result =  App.uhfService.WriteSerialNumber(TagNo.Trim(),RfidSettings.Instance.TicketReadPower,RfidSettings.Instance.TicketWritePower);
                        //TerminalResult result = await App.uhfService.WriteTagEPC( data, password, 1000);
                        if (result.Result)
                        {
                            await PopupNavigation.Instance.PushAsync(new MessagePopup("Başarılı", ConvertHelper.ByteToTexts(data)));
                        }
                        else
                        {
                            await PopupNavigation.Instance.PushAsync(new MessagePopup("Hata", result.Message));
                        }
                    }
                    else
                    {
                        await PopupNavigation.Instance.PushAsync(new MessagePopup("Uyarı", "Lütfen seri numarası giriniz!"));
                    }
                }
                catch
                {
                    await PopupNavigation.Instance.PushAsync(new MessagePopup("Uyarı", "Lütfen seri numarası giriniz!"));
                }
            }
            catch 
            {
                await PopupNavigation.Instance.PushAsync(new MessagePopup("Uyarı", "Lütfen bağlantı ayarlarınızı kontrol edin!"));
            }
        }
    }
}
