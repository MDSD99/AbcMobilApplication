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
    public class TagExitViewModel : BaseViewModel
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
        public TagExitViewModel()
        {
            SeriNoList = new ObservableCollection<StockUI>();
            //Random randomm=new Random();
            //SeriNoList.Add(new StockUI { ContentColor= Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256)),HeaderColor= Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256)),IsVisible=false,RafKodu="",SeriNo="202201013030",StokAdi="Sol Ranza",StokKodu="1031033"});
            //SeriNoList.Add(new StockUI { ContentColor= Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256)),HeaderColor= Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256)),IsVisible=false,RafKodu="",SeriNo="202201013066",StokAdi="Sağ Ranza",StokKodu="1031055"});
            //SeriNoList.Add(new StockUI { ContentColor= Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256)),HeaderColor= Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256)),IsVisible=false,RafKodu="",SeriNo="202201013055",StokAdi="Alt Ranza",StokKodu="1031033"});
            //SeriNoList.Add(new StockUI { ContentColor= Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256)),HeaderColor= Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256)),IsVisible=false,RafKodu="",SeriNo="202201013012",StokAdi="Üst Ranza",StokKodu="1031044"});
            //SeriNoList.Add(new StockUI { ContentColor= Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256)),HeaderColor= Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256)),IsVisible=false,RafKodu="",SeriNo="202201013044",StokAdi="Üçlü Ranza",StokKodu="1031077"});
            //SeriNoList.Add(new StockUI { ContentColor= Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256)),HeaderColor= Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256)),IsVisible=false,RafKodu="",SeriNo="202201013012",StokAdi="b Ranza",StokKodu="1031011"});
            //SearchTagCommand = new Command(async (tag) => await OnSearchTag(tag));
            ReadCommand = new Command(async () => await OnRead());
            CellExitCommand = new Command(OnTagExit);
            ClearCommand = new Command(OnClear);
            ClearDataCommand = new Command<StockUI>(OnClearData);
        }
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
        public ICommand CellExitCommand { get; }
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
            List<string> notFoundSerialNumber = new List<string>();
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
                        TerminalResult result = await App.uhfService.InventorySerialNumber(200, RfidSettings.Instance.ExitPower, RfidSettings.Instance.ExitPower);
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
                                    MobileResult mobileResult = await ApiService.GetSimpleStoreData(item.Substring(0, 8) + "-" + item.Substring(8, 4));
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
                                        await PopupNavigation.Instance.PushAsync(new MessagePopup("Hata", mobileResult.Message));
                                        return;
                                    }
                                    if (!mobileResult.Result)
                                    {
                                        notFoundSerialNumber.Add(item.Substring(0, 8) + "-" + item.Substring(8, 4));

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
            finally
            {
                if (notFoundSerialNumber != null && notFoundSerialNumber.Count() > 0)
                    await PopupNavigation.Instance.PushAsync(new MessagePopup("Uyarı", $"Bu ürünler depoda bulunmamaktadır! \n=>{string.Join("\n=>", notFoundSerialNumber.Distinct().ToArray())}"));
            }
        }
        private async void OnTagExit()
        {
            try
            {
                IList<Stock> data = new List<Stock>();
                foreach (StockUI item in SeriNoList)
                {
                    data.Add(new Stock { SeriNo = item.SeriNo, StokAdi = item.StokAdi, StokKodu = item.StokKodu, RafKodu = item.RafKodu });
                }
                MobileResult mobileResult = await ApiService.PostCellExit(data);
                if (mobileResult.Result)
                {
                    await PopupNavigation.Instance.PushAsync(new MessagePopup("Başarılı", mobileResult.Message));
                    SeriNoList.Clear();
                    App.uhfService.Clear();
                }

                if (mobileResult.ExceptionResult)
                {
                    await PopupNavigation.Instance.PushAsync(new MessagePopup("Uyarı", mobileResult.Message));
                    SeriNoList.Clear();
                    App.uhfService.Clear();
                }
                if (!mobileResult.Result)
                {
                    await PopupNavigation.Instance.PushAsync(new MessagePopup("Uyarı", mobileResult.Message));
                }
            }
            catch
            {
                await PopupNavigation.Instance.PushAsync(new MessagePopup("Hata", "Lütfen bağlantı ayarlarınızı kontrol edin!"));
            }
            finally
            {
                //App.uhfService.Close();
            }
        }
        private void OnClear()
        {
            SeriNoList.Clear();
            TagNo = "";
            App.uhfService.Clear();
            ClearVisible = false;
        }
        private void OnClearData(StockUI stock)
        {
            if (stock != null)
            {
                SeriNoList.Remove(stock);
                App.uhfService.CensusSerialNumberDelete(stock.SeriNo);
            }
        }
    }
}
