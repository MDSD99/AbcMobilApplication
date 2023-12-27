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
    public class TicketCensusViewModel : BaseViewModel
    {
        private string censusStart = "SAYIMA BAŞLA";
        private int ticketCount = 0;
        public int TicketCount
        {
            get => ticketCount;
            set
            {
                ticketCount = value;
                OnPropertyChanged(nameof(TicketCount));
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
        public ICommand DetailCensust { get; }
        public Command TitleCommand { get; }
        public TicketCensusViewModel()
        {
            CensusStartCommand = new Command(async () => await OnStart());
            CensusFinishCommand = new Command(OnFinish);
            DetailCensust = new Command(OnDetail);
            TitleCommand = new Command((title) => OnSelectedTitle(title));
            if (SerialList == null)
                SerialList = new ObservableCollection<StockUI>();
        }
        public ObservableCollection<StockUI> SerialList { get; }
        public void OnSelectedTitle(object Id)
        {
            //if (SerialList[(int)Id - 1].IsVisible)
            //{
            //    if (SerialList[(int)Id - 1].Count != 0)
            //        for (int i = 0; i < CensusList[(int)Id - 1].Count; i++)
            //        {
            //            SerialList[(int)Id - 1][i].IsVisible = false;
            //        }
            //    SerialList[(int)Id - 1].IsVisible = false;
            //}
            //else
            //{
            //    if (SerialList[(int)Id - 1].Count != 0)
            //        for (int i = 0; i < CensusList[(int)Id - 1].Count; i++)
            //        {
            //            CensusList[(int)Id - 1][i].IsVisible = true;
            //        }
            //    CensusList[(int)Id - 1].IsVisible = true;
            //}
        }
        public async Task OnStart()
        {
            List<string> notFoundSerialNumber = new List<string>();
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
                        SerialList.Clear();
                        App.uhfService.Clear();
                        TicketCount = 0;
                        CensusStart = "SAYIMA DEVAM ET";
                        App.uhfService.CloseDownUpdate();
                    }
                    else
                    {

                        try
                        {

                            TerminalResult result = await App.uhfService.InventorySerialNumber(200, RfidSettings.Instance.TicketCensusReadPower, RfidSettings.Instance.TicketCensusReadPower);
                            //if (result.DeviceID != App.DeviceID) continue;
                            Random randomm = new Random();
                            if (result.Result)
                            {
                                if (SerialList.Count != ((IList<TagInfo>)result.Data).Count)
                                {

                                    IList<string> seri = ((IList<TagInfo>)result.Data).Where(s => s.MakeSync == false && s.Count < 2).Select(s => s.SeriNo).ToList();
                                    foreach (string item in seri)
                                    {
                                        App.audioService.playCensus(1);
                                        MobileResult mobileResult = await ApiService.GetSimpleStoreData(item.Substring(0, 8) + "-" + item.Substring(8, 4));
                                        if (mobileResult.Result)
                                        {
                                            Stock item2 = (Stock)mobileResult.Data;
                                            var checkListItem = SerialList.Any(x => x.SeriNo == item2.SeriNo);
                                            if (!checkListItem)
                                            {
                                                SerialList.Add(new StockUI
                                                {
                                                    RafKodu = item2.RafKodu,
                                                    IsVisible = false,
                                                    SeriNo = item2.SeriNo,
                                                    StokAdi = item2.StokAdi,
                                                    StokKodu = item2.StokKodu,
                                                    HeaderColor = Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256)),
                                                    ContentColor = Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256))
                                                });
                                            }
                                            TicketCount += 1;
                                            App.uhfService.Matching(item2);
                                        }
                                        if (mobileResult.ExceptionResult)
                                        {
                                            CensusStart = "SAYIMA DEVAM ET";
                                            App.uhfService.CensusSerialNumberDelete(item);
                                            await PopupNavigation.Instance.PushAsync(new MessagePopup("Hata", mobileResult.Message));
                                            return;
                                        }
                                        if (!mobileResult.Result)
                                        {
                                            notFoundSerialNumber.Add(item.Substring(0, 8) + "-" + item.Substring(8, 4));

                                        }
                                    }
                                }
                            }
                            else if (result.ExceptionResult)
                            {
                                CensusStart = "SAYIMA DEVAM ET";
                                await PopupNavigation.Instance.PushAsync(new MessagePopup("Hata", result.Message));
                                break;
                            }
                        }
                        catch (Exception ex)
                        {
                            CensusStart = "SAYIMA DEVAM ET";
                            await PopupNavigation.Instance.PushAsync(new MessagePopup("Hata", $"Lütfen bağlantı ayarlarınızı kontrol ediniz!: {ex.Message}"));
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
                if (notFoundSerialNumber != null && notFoundSerialNumber.Count() > 0)
                    await PopupNavigation.Instance.PushAsync(new MessagePopup("Uyarı", $"Bu ürünler depoda bulunmamaktadır! \n=>{string.Join("\n=>", notFoundSerialNumber.Distinct().ToArray())}"));
                //  App.uhfService.Close();
            }
        }
        public async void OnFinish()
        {
            try
            {
                QuestionPopup popupPage = new QuestionPopup("", "Sayımı bitirmek istediğinize emin misiniz?");
                await PopupNavigation.Instance.PushAsync(popupPage);
                if (await popupPage.TaskCompletionSource)
                {
                    IList<Stock> TagSayimData = new List<Stock>();

                    foreach (StockUI stock in SerialList)
                    {
                        TagSayimData.Add(new Stock { RafKodu = stock.RafKodu, SeriNo = stock.SeriNo, StokAdi = stock.StokAdi, StokKodu = stock.StokKodu, MamulKodu = stock.MamulKodu, MamulStokAdi = stock.MamulStokAdi });
                    }

                    MobileResult mobileResult = await ApiService.PostCensus(TicketCount);
                    if (mobileResult.Result)
                    {
                        //smgRfSayimTicket tablosuna verileri at
                        MobileResult mobileResult1 = await ApiService.PostCensusTicketList(TagSayimData);
                        if (mobileResult1.Result)
                        {
                            await PopupNavigation.Instance.PushAsync(new MessagePopup(mobileResult.Message));
                            SerialList.Clear();
                            App.uhfService.Clear();
                            TicketCount = 0;
                        }

                    }
                    else if (mobileResult.ExceptionResult)
                    {
                        await PopupNavigation.Instance.PushAsync(new MessagePopup("Uyarı", mobileResult.Message));
                    }
                }
            }
            catch
            {
                await PopupNavigation.Instance.PushAsync(new MessagePopup("Hata", "Lütfen bağlantı ayarlarınızı ve ürünün depoda olup olmadığını kontrol edin!"));
            }
        }
        public void OnDetail()
        {

        }
    }
}
