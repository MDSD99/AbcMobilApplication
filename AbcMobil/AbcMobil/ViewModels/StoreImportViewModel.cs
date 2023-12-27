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
    public class StoreImportViewModel : BaseViewModel
    {
        private bool clearVisible = false;
        public bool ClearVisible
        {
            get { return clearVisible; }
            set
            {
                clearVisible = value;
                OnPropertyChanged(nameof(ClearVisible));
            }
        }
        public StoreImportViewModel()
        {
            SeriNoList = new ObservableCollection<StockUI>();
            Random randomm = new Random();
            SeriNoList.Add(new StockUI { ContentColor = Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256)), HeaderColor = Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256)), IsVisible = false, RafKodu = "", SeriNo = "202201013030", StokAdi = "Sol Ranza", StokKodu = "1031033" });
            SeriNoList.Add(new StockUI { ContentColor = Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256)), HeaderColor = Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256)), IsVisible = false, RafKodu = "", SeriNo = "202201013066", StokAdi = "Sağ Ranza", StokKodu = "1031055" });
            SeriNoList.Add(new StockUI { ContentColor = Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256)), HeaderColor = Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256)), IsVisible = false, RafKodu = "", SeriNo = "202201013055", StokAdi = "Alt Ranza", StokKodu = "1031033" });
            SeriNoList.Add(new StockUI { ContentColor = Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256)), HeaderColor = Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256)), IsVisible = false, RafKodu = "", SeriNo = "202201013012", StokAdi = "Üst Ranza", StokKodu = "1031044" });
            SeriNoList.Add(new StockUI { ContentColor = Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256)), HeaderColor = Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256)), IsVisible = false, RafKodu = "", SeriNo = "202201013044", StokAdi = "Üçlü Ranza", StokKodu = "1031077" });
            SeriNoList.Add(new StockUI { ContentColor = Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256)), HeaderColor = Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256)), IsVisible = false, RafKodu = "", SeriNo = "202201013012", StokAdi = "b Ranza", StokKodu = "1031011" });
            //SearchTagCommand = new Command(async (tag) => await OnSearchTag(tag));
            ReadCommand = new Command(async () => await OnRead());
            ClearCommand = new Command(OnClear);
            ClearDataCommand = new Command<StockUI>(OnClearData);
            MatchCommand = new Command(async () => await OnMatch());
        }
        private string readCard = "OKUTMAYA BAŞLA";
        public string ReadCard
        {
            get => readCard;
            set
            {
                readCard = value;
                OnPropertyChanged(nameof(ReadCard));
            }
        }
        public ObservableCollection<StockUI> SeriNoList { get; }
        public ICommand ReadCommand { get; }
        public ICommand ClearDataCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand MatchCommand { get; }
        public ICommand SearchTagCommand { get; }
        private async Task OnRead()
        {
            try
            {
                if (ReadCard == "OKUTMAYA BAŞLA" || ReadCard == "OKUTMAYA DEVAM ET")
                    ReadCard = "OKUTMAYA DURDUR";
                else
                    ReadCard = "OKUTMAYA DEVAM ET";
                while (ReadCard == "OKUTMAYA DURDUR")
                {
                    if (App.uhfService.CloseDown())
                    {
                        ReadCard = "OKUTMAYA DEVAM ET";
                        SeriNoList.Clear();
                        App.uhfService.Clear();
                        App.uhfService.CloseDownUpdate();
                    }
                    else
                    {
                        TerminalResult result = await App.uhfService.InventorySerialNumber(200, RfidSettings.Instance.MatchingReadPower, RfidSettings.Instance.MatchingReadPower);
                        //if (result.DeviceID != App.DeviceID) continue;
                        Random randomm = new Random();
                        Color color;
                        if (result.Result)
                        {
                            if (SeriNoList.Count != ((IList<TagInfo>)result.Data).Count)
                            {
                                IList<string> tags = ((IList<TagInfo>)result.Data).Where(s => s.MakeSync == false && s.Count < 2).Select(s => s.SeriNo).ToList();
                                foreach (string item in tags)
                                {
                                    App.audioService.playCensus(tags.Count - ((IList<TagInfo>)result.Data).Count);
                                    color = Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256));
                                    MobileResult mobileResult = await ApiService.GetStoreData(item.Substring(0, 8) + "-" + item.Substring(8, 4));
                                    if (mobileResult.Result)
                                    {
                                        Stock item2 = (Stock)mobileResult.Data;
                                        SeriNoList.Add(new StockUI
                                        {
                                            RafKodu = item2.RafKodu,
                                            IsVisible = false,
                                            SeriNo = item2.SeriNo,
                                            StokAdi = item2.StokAdi,
                                            StokKodu = item2.StokKodu,
                                            HeaderColor = color,
                                            ContentColor = Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256))
                                        });
                                        App.uhfService.Matching(item2);
                                    }
                                    if (mobileResult.ExceptionResult)
                                    {
                                        ReadCard = "OKUTMAYA DEVAM ET";
                                        await PopupNavigation.Instance.PushAsync(new MessagePopup("HATA", mobileResult.Message));
                                        return;
                                    }
                                    if (!mobileResult.Result)
                                    {
                                        await PopupNavigation.Instance.PushAsync(new MessagePopup("UYARI", mobileResult.Message));
                                    }

                                }
                                //App.audioService.playCensus();
                            }
                        }
                        else if (result.ExceptionResult)
                        {
                            ReadCard = "OKUTMAYA DEVAM ET";
                            await PopupNavigation.Instance.PushAsync(new MessagePopup("Hata", result.Message));
                            break;
                        }
                    }  
                }
            }
            catch 
            {
                ReadCard = "OKUTMAYA DEVAM ET";
                await PopupNavigation.Instance.PushAsync(new MessagePopup("Hata", "Lütfen bağlantı ayarlarınızı kontrol edin!"));
            }
        }
        private async Task OnMatch()
        {
            try
            {
                IList<Stock> data = new List<Stock>();
                foreach (StockUI stock in SeriNoList)
                {
                    data.Add(new Stock { RafKodu = stock.RafKodu, SeriNo = stock.SeriNo, StokAdi = stock.StokAdi, StokKodu = stock.StokKodu });
                }
                MobileResult mobileResult = await ApiService.PostStoreImport(data);
                if (mobileResult.Result)
                {
                    await PopupNavigation.Instance.PushAsync(new MessagePopup("Başarılı", "Depo yerleştirmeniz başarılı bir şekilde tamamlandı!"));
                    SeriNoList.Clear();
                    App.uhfService.Clear();
                }
                else
                {
                    await PopupNavigation.Instance.PushAsync(new MessagePopup("Uyarı", mobileResult.Message));
                    ClearVisible = true;
                }
            }
            catch 
            {
                await PopupNavigation.Instance.PushAsync(new MessagePopup("Hata", "Lütfen bağlantı ayarlarınızı kontrol edin!"));
            }
        }
        private void OnClear()
        {
            SeriNoList.Clear();
            App.uhfService.Clear();
            ClearVisible = false;
        }
        private void OnClearData(StockUI stock)
        {
            if (stock != null)
            {
                SeriNoList.Remove(stock);
                App.uhfService.RemoveEpc(stock.SeriNo);
            }
        }
    }
}
