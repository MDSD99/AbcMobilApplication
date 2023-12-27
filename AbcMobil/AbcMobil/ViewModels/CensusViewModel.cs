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
    public class CensusViewModel : BaseViewModel
    {
        private string censusStart = "SAYIMA BAŞLA";
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
        public CensusViewModel()
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
            string testTag = "Test";
            try
            {
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
                            TerminalResult result = await App.uhfService.TagInventory(200, RfidSettings.Instance.CensusReadPower, RfidSettings.Instance.CensusReadPower);
                            ShelfInfo stocks;
                            Random randomm = new Random();
                            Color color;
                            
                            if (result.Result) {
                           
								if (CensusList.Count != ((IList<TagInfo>)result.Data).Count)
                                {
                                    ShelfCount = ((IList<TagInfo>)result.Data).Count;
                                    IList<string> tags = ((IList<TagInfo>)result.Data).Where(s => s.MakeSync == false).Select(s => s.RafKodu).ToList();
                               
									foreach (string item in tags)
                                    {
                                        testTag = item;
                                        App.audioService.playCensus(tags.Count - ((IList<TagInfo>)result.Data).Count);
                                        color = Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256));
                                        
                                        MobileResult mobileResult = await ApiService.GetCell(item);
                                        if (mobileResult.Result)
                                        {
                                            stocks = new ShelfInfo(item, "", ((IList<Stock>)mobileResult.Data).Count, CensusList.Count + 1, color);
                                            foreach (Stock item2 in (IList<Stock>)mobileResult.Data)
                                            {
                                                stocks.Title2 = item2.StokKodu;
                                                stocks.Add(new StockUI
                                                {
                                                    RafKodu = item2.RafKodu,
                                                    IsVisible = false,
                                                    SeriNo = item2.SeriNo,
                                                    StokAdi = item2.StokAdi,
                                                    StokKodu = item2.StokKodu,
                                                    MamulKodu = item2.MamulKodu,
                                                    MamulStokAdi=item2.MamulStokAdi,
                                                    HeaderColor = color,
                                                    ContentColor = Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256))
                                                });
                                            }
                                            TagCount += ((IList<Stock>)mobileResult.Data).Count;
                                            CensusList.Add(stocks);
                                            App.uhfService.TicketMatching((IList<Stock>)mobileResult.Data, item);
                                        }
                                        else if (mobileResult.ExceptionResult)
                                        {
                                            CensusStart = "SAYIMA DEVAM ET";
                                            await PopupNavigation.Instance.PushAsync(new MessagePopup("Hata 1", mobileResult.Message));
                                            return;
                                        }

                                    }
                                    //App.audioService.playCensus();
                                }
                            }
                            else if (result.ExceptionResult)
                            {
                                CensusStart = "SAYIMA DEVAM ET";
                                await PopupNavigation.Instance.PushAsync(new MessagePopup($"Hata 2{testTag}", result.Message));
                                break;
                            }

                        }
                        catch (Exception ex)
                        {
                            CensusStart = "SAYIMA DEVAM ET";
                            await PopupNavigation.Instance.PushAsync(new MessagePopup($"Hata 3{testTag}", ex.Message));
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CensusStart = "SAYIMA DEVAM ET";
                await PopupNavigation.Instance.PushAsync(new MessagePopup($"Hata 4{testTag}", ex.Message));
               
            }
            finally
            {
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
                    if (await popupPage.TaskCompletionSource )
                    {
                        if (CensusList.Count != 0)
                        {
                            IList<Stock> TagSayimData = new List<Stock>();

                            foreach (ShelfInfo stock in CensusList)
                            {
                                for (int i = 0; i < stock.Count(); i++)
                                {
                                    TagSayimData.Add(new Stock { RafKodu = stock[i].RafKodu, SeriNo = stock[i].SeriNo, StokAdi = stock[i].StokAdi, StokKodu = stock[i].StokKodu, MamulKodu = stock[i].MamulKodu, MamulStokAdi = stock[i].MamulStokAdi });
                                }

                            }

                            MobileResult mobileResult = await ApiService.PostCensusTag(shelfCount, tagCount);

                            if (mobileResult.Result)
                            {
                                //smgRfSayim tablosuna verileri at
                                MobileResult mobileResult1 = await ApiService.PostCensusTagList(TagSayimData);
                                if (mobileResult1.Result)
                                {
                                    await PopupNavigation.Instance.PushAsync(new MessagePopup(mobileResult1.Message));

                                    CensusList.Clear();
                                    App.uhfService.Clear();
                                    TagSayimData.Clear();
                                    TagCount = 0;
                                    ShelfCount = 0;
                                    CensusStart = "SAYIMA BAŞLA";
                                }
                                else if (mobileResult.ExceptionResult)
                                {
                                    await PopupNavigation.Instance.PushAsync(new MessagePopup("Uyarı", mobileResult1.Message));
                                }

                            }
                            else if (mobileResult.ExceptionResult)
                            {
                                await PopupNavigation.Instance.PushAsync(new MessagePopup("Uyarı", mobileResult.Message));
                            }
                        }else
                            await PopupNavigation.Instance.PushAsync(new MessagePopup("Uyarı", "Tag listesi Boş!"));

                    }
                }
            }
            catch (Exception ex)
            {
                await PopupNavigation.Instance.PushAsync(new MessagePopup("Hata 6", ex.Message));
            }
        }
    }
}
