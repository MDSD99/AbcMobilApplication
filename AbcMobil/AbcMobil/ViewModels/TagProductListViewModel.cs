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
    public class TagProductListViewModel : BaseViewModel
    {
        private string censusStart = "Tag Okut";
        private int tagCount = 0;
        private int shelfCount = 0;
        public int TagCount
        {
            get => tagCount;
            set
            {
                tagCount = value;
                OnPropertyChanged(nameof(TagCount));
            }
        }
        public int ShelfCount
        {
            get => shelfCount;
            set
            {
                shelfCount = value;
                OnPropertyChanged(nameof(ShelfCount));
            }
        }
        public string CensusStart
        {
            get => censusStart;
            set
            {
                censusStart = value;
                OnPropertyChanged(nameof(CensusStart));
            }
        }
        public ICommand CensusStartCommand { get; }
        public ICommand CensusFinishCommand { get; }
        public Command TitleCommand { get; }
        public TagProductListViewModel()
        {
            CensusStartCommand = new Command(async () => await OnStart());
            CensusFinishCommand = new Command(OnFinish);
            TitleCommand = new Command((title) => OnSelectedTitle(title));
            if (CensusList == null)
                CensusList = new ObservableCollection<ShelfInfo>();
        }
        public ObservableCollection<ShelfInfo> CensusList { get; }
        public void OnSelectedTitle(object Id)
        {
            if (CensusList[(int)Id - 1].IsVisible)
            {
                if (CensusList[(int)Id - 1].Count != 0)
                    for (int i = 0; i < CensusList[(int)Id - 1].Count; i++)
                    {
                        CensusList[(int)Id - 1][i].IsVisible = false;
                    }
                CensusList[(int)Id - 1].IsVisible = false;
            }
            else
            {
                if (CensusList[(int)Id - 1].Count != 0)
                    for (int i = 0; i < CensusList[(int)Id - 1].Count; i++)
                    {
                        CensusList[(int)Id - 1][i].IsVisible = true;
                    }
                CensusList[(int)Id - 1].IsVisible = true;
            }
        }
        public async Task OnStart()
        {
            try
            {
                RfidSettings.Instance.TagReadPower = 1;
                if (CensusStart == "SAYIMA BAŞLA" || CensusStart == "SAYIMA DEVAM ET")
                    CensusStart = "SAYIMI DURDUR";
                else
                    CensusStart = "SAYIMA DEVAM ET";
                while (CensusStart == "SAYIMI DURDUR")
                {
                    if (App.uhfService.CloseDown())
                    {
                        CensusStart = "SAYIMA DEVAM ET";
                        CensusList.Clear();
                        App.uhfService.Clear();
                        App.uhfService.CloseDownUpdate();
                    }
                    else
                    {

                        try
                        {
                            TerminalResult result = await App.uhfService.TagInventory(20, 1, 33);
                            ShelfInfo stocks;
                            Random randomm = new Random();
                            Color color;
                            if (result.Result)
                            {
                                if (CensusList.Count < 1 )
                                {
                                    string tag = ((IList<TagInfo>)result.Data).Where(s => s.MakeSync == false).Select(s => s.RafKodu).FirstOrDefault();

                                    App.audioService.playCensus(1 - ((IList<TagInfo>)result.Data).Count);
                                    color = Color.Black;
                                    MobileResult mobileResult = await ApiService.GetCell(tag);

                                    if (mobileResult.Result)
                                    {
                                        stocks = new ShelfInfo(tag, "", ((IList<Stock>)mobileResult.Data).Count, CensusList.Count + 1, color);

                                        foreach (Stock item2 in (IList<Stock>)mobileResult.Data)
                                        {
                                                stocks.Title2 = item2.StokKodu;
                                                stocks.Add(new StockUI
                                                {
                                                    RafKodu = item2.RafKodu,
                                                    KumasDetayi=item2.KumasDetayi,
                                                    //Bakiye=item2.Bakiye,
                                                    MamulStokAdi=item2.MamulStokAdi,
                                                    MamulKodu=item2.MamulKodu,
                                                    IsVisible = false,
                                                    SeriNo = item2.SeriNo,
                                                    StokAdi = item2.StokAdi,
                                                    StokKodu = item2.StokKodu,
                                                    HeaderColor = color,
                                                    ContentColor = Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256))
                                                });
                                        }

                                        TagCount = 1;
                                        CensusList.Add(stocks);
                                        App.uhfService.TicketMatching((IList<Stock>)mobileResult.Data, tag);
                                    }
                                    else if (mobileResult.ExceptionResult)
                                    {
                                        CensusStart = "SAYIMA DEVAM ET";
                                        await PopupNavigation.Instance.PushAsync(new MessagePopup("Hata", mobileResult.Message));
                                        return;
                                    }

                                    //ShelfCount = ((IList<TagInfo>)result.Data).Count;
                                    //IList<string> tags = ((IList<TagInfo>)result.Data).Where(s => s.MakeSync == false).Select(s => s.RafKodu).ToList();
                                    //foreach (string item in tags)
                                    //{
                                    //    App.audioService.playCensus(tags.Count - ((IList<TagInfo>)result.Data).Count);
                                    //    color = Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256));
                                    //    MobileResult mobileResult = await ApiService.GetCell(item);
                                    //    if (mobileResult.Result)
                                    //    {
                                    //        stocks = new ShelfInfo(item, "", ((IList<Stock>)mobileResult.Data).Count, CensusList.Count + 1, color);

                                    //        int i = 0;
                                    //        foreach (Stock item2 in (IList<Stock>)mobileResult.Data)
                                    //        {
                                    //            i++;
                                    //            if (i == 1)
                                    //            {
                                    //                stocks.Title2 = item2.StokKodu;
                                    //                stocks.Add(new StockUI
                                    //                {
                                    //                    RafKodu = item2.RafKodu,
                                    //                    IsVisible = false,
                                    //                    SeriNo = item2.SeriNo,
                                    //                    StokAdi = item2.StokAdi,
                                    //                    StokKodu = item2.StokKodu,
                                    //                    HeaderColor = color,
                                    //                    ContentColor = Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256))
                                    //                });
                                    //            }
                                    //        }

                                    //        TagCount += ((IList<Stock>)mobileResult.Data).Count;
                                    //        CensusList.Add(stocks);
                                    //        App.uhfService.TicketMatching((IList<Stock>)mobileResult.Data, item);
                                    //    }
                                    //    else if (mobileResult.ExceptionResult)
                                    //    {
                                    //        CensusStart = "SAYIMA DEVAM ET";
                                    //        await PopupNavigation.Instance.PushAsync(new MessagePopup("Hata", mobileResult.Message));
                                    //        return;
                                    //    }

                                    //}
                                    //App.audioService.playCensus();
                                }
                            }
                            else if (result.ExceptionResult)
                            {
                                CensusStart = "SAYIMA DEVAM ET";
                                await PopupNavigation.Instance.PushAsync(new MessagePopup("Hata", result.Message));
                                break;
                            }
                        }
                        catch
                        {
                            CensusStart = "SAYIMA DEVAM ET";
                            await PopupNavigation.Instance.PushAsync(new MessagePopup("Hata", "Lütfen bağlantı ayarlarınızı kontrol edin!"));
                            break;
                        }
                    }
                }
            }
            catch
            {
                CensusStart = "SAYIMA DEVAM ET";
                await PopupNavigation.Instance.PushAsync(new MessagePopup("Hata", "Lütfen bağlantı ayarlarınızı kontrol edin!"));
            }
            finally
            {
                RfidSettings.Instance.TagReadPower = 5;
                // App.uhfService.Close();
            }
        }
        public async void OnFinish()
        {
            try
            {
                if (CensusStart != "SAYIMA BAŞLA")
                {
                    QuestionPopup popupPage = new QuestionPopup("", "Sayımı bitirmek istediğinize emin misiniz?", "EVET", "HAYIR");
                    await PopupNavigation.Instance.PushAsync(popupPage);
                    if (await popupPage.TaskCompletionSource)
                    {
                        MobileResult mobileResult = await ApiService.PostCensusTag(shelfCount, tagCount);
                        if (mobileResult.Result)
                        {
                            await PopupNavigation.Instance.PushAsync(new MessagePopup(mobileResult.Message));
                            CensusList.Clear();
                            App.uhfService.Clear();
                            TagCount = 0;
                            ShelfCount = 0;
                            CensusStart = "SAYIMA BAŞLA";
                        }
                        else if (mobileResult.ExceptionResult)
                        {
                            await PopupNavigation.Instance.PushAsync(new MessagePopup("Uyarı", mobileResult.Message));
                        }
                    }
                }
            }
            catch
            {
                await PopupNavigation.Instance.PushAsync(new MessagePopup("Hata", "Lütfen bağlantı ayarlarınızı kontrol ediniz!"));
            }
        }


        //private string serialNumber;
        //public string SerialNumber
        //{
        //    get => serialNumber;
        //    set
        //    {
        //        serialNumber = value;
        //        OnPropertyChanged(nameof(SerialNumber));
        //    }
        //}        
        //private List<StockUI> _returnList;

        //public TagProductListViewModel()
        //{
        //    ReadCommand = new Command(OnRead);
        //    _returnList =new List<StockUI>();
        //}

        //public ICommand ReadCommand { get; }

        //private async void OnRead(object obj)
        //{
        //    try
        //    {
        //        TerminalResult terminal =await App.uhfService.ReadTag(RfidSettings.Instance.TicketReadPower, RfidSettings.Instance.TicketWritePower);
        //        if (terminal.Result)
        //        {
        //            SerialNumber = terminal.Data.ToString();
        //        }
        //        else if (terminal.ExceptionResult)
        //            await PopupNavigation.Instance.PushAsync(new MessagePopup("Hata", terminal.Message));
        //        else
        //            await PopupNavigation.Instance.PushAsync(new MessagePopup("Uyarı", terminal.Message));
        //    }
        //    catch
        //    {
        //        await PopupNavigation.Instance.PushAsync(new MessagePopup("Hata", "Lütfen bağlantı ayarlarınızı kontrol edin!"));
        //    }
        //    finally
        //    {
        //        // App.uhfService.Close();
        //    }
        //}
    }
}
