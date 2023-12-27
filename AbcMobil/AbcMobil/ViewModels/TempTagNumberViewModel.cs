using AbcMobil.Models;
using AbcMobil.PopupViews;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
namespace AbcMobil.ViewModels
{
    public class TempTagNumberViewModel : BaseViewModel
    {
        private bool clearVisible = false;
       // private IList<string> tagInfos;
        public bool ClearVisible
        {
            get { return clearVisible; }
            set
            {
                clearVisible = value;
                OnPropertyChanged(nameof(ClearVisible));
            }
        }
        public TempTagNumberViewModel()
        {
            SeriNoList = new ObservableCollection<StockUI>();
            // SeriNoList2 = new ObservableCollection<StockUI>();
            //tagInfos = new ObservableCollection<string>();
            //Random randomm=new Random();
            //SeriNoList.Add(new StockUI { ContentColor= Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256)),HeaderColor= Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256)),IsVisible=false,RafKodu="",SeriNo="202201013030",StokAdi="Sol Ranza",StokKodu="1031033"});
            //SeriNoList.Add(new StockUI { ContentColor= Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256)),HeaderColor= Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256)),IsVisible=false,RafKodu="",SeriNo="202201013066",StokAdi="Sağ Ranza",StokKodu="1031055"});
            //SeriNoList.Add(new StockUI { ContentColor= Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256)),HeaderColor= Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256)),IsVisible=false,RafKodu="",SeriNo="202201013055",StokAdi="Alt Ranza",StokKodu="1031033"});
            //SeriNoList.Add(new StockUI { ContentColor= Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256)),HeaderColor= Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256)),IsVisible=false,RafKodu="",SeriNo="202201013012",StokAdi="Üst Ranza",StokKodu="1031044"});
            //SeriNoList.Add(new StockUI { ContentColor= Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256)),HeaderColor= Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256)),IsVisible=false,RafKodu="",SeriNo="202201013044",StokAdi="Üçlü Ranza",StokKodu="1031077"});
            //SeriNoList.Add(new StockUI { ContentColor= Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256)),HeaderColor= Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256)),IsVisible=false,RafKodu="",SeriNo="202201013012",StokAdi="b Ranza",StokKodu="1031011"});
            //SearchTagCommand = new Command(async (tag) => await OnSearchTag(tag));
            ReadCommand = new Command(async () => await OnRead());
            SearchTagCommand = new Command(OnTag);
            ClearCommand = new Command(OnClear);
            ClearDataCommand = new Command<StockUI>(OnClearData);
            MatchCommand = new Command(async () => await OnMatch());
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
        public ObservableCollection<StockUI> SeriNoList2 { get; }
        public ICommand ReadCommand { get; }
        public ICommand ClearDataCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand MatchCommand { get; }
        public ICommand SearchTagCommand { get; }
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
            try
            {

                if (ReadCard == "OKUTMAYA BAŞLA" || ReadCard == "OKUTMAYA DEVAM ET")
                    ReadCard = "OKUTMAYA DURDUR";
                else
                    ReadCard = "OKUTMAYA DEVAM ET";
                while (ReadCard == "OKUTMAYA DURDUR" || ReadCard == "OKUTMAYA BAŞLA")
                {
                    if (App.uhfService.CloseDown())
                    {
                        ReadCard = "OKUTMAYA DEVAM ET";
                        SeriNoList.Clear();
                        //SeriNoList2.Clear();
                        //tagInfos.Clear();
                        App.uhfService.Clear();
                        App.uhfService.CloseDownUpdate();
                    }
                    else
                    {
                        //Burası Tagların okunup getirildiği metod
                  
                        TerminalResult result = await App.uhfService.InventorySerialNumber(200, RfidSettings.Instance.MatchingReadPower, RfidSettings.Instance.MatchingReadPower);
                        
                        Random randomm = new Random();
                        Color color;
                        
                        if (result.Result)
                        {
                            //if (result.DeviceID != App.DeviceID) continue;
                            if (SeriNoList.Count != ((IList<TagInfo>)result.Data).Count)
                            {
                                //Okunan tagları liste haline getiriyor ve eşleşmemişleri getiriyor.
                                IList<string> tags = ((IList<TagInfo>)result.Data).Where(s => s.MakeSync == false && s.Count < 2).Select(s => s.SeriNo).ToList();

                                foreach (string item in tags)
                                {
                                    //tagInfos.Add(item);
                                    App.audioService.playCensus(tags.Count - ((IList<TagInfo>)result.Data).Count);
                                    color = Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256));
                                    if (item.Replace('*', ' ').Trim().Length == 12)
                                    {
                                        //MobileResult mobileResult = await ApiService.GetSimpleStoreData(item.Substring(0, 8) + "-" + item.Substring(8, 4));

                                        //Geçici tag için yeni method eklendi.
                                        //Burası Daha önce getirdiğimiz etiketlere ait dataları getiriyor.
                                        MobileResult mobileResult = await ApiService.GetSimpleStoreDataTempTag(item.Substring(0, 8) + "-" + item.Substring(8, 4));
                                        if (mobileResult.Result)
                                        {
                                            Stock item2 = (Stock)mobileResult.Data;
                                            var checkListItem = SeriNoList.Any(x => x.SeriNo == item2.SeriNo);
                                            if (!checkListItem)
                                            {
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
                                        }
                                        if (mobileResult.ExceptionResult)
                                        {
                                            ReadCard = "OKUTMAYA DEVAM ET";
                                            await PopupNavigation.Instance.PushAsync(new MessagePopup("Hata", mobileResult.Message));
                                            return;
                                        }
                                        if (!mobileResult.Result)
                                        {
                                            await PopupNavigation.Instance.PushAsync(new MessagePopup("Uyarı", mobileResult.Message));
                                        }

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
            catch (Exception ex)
            {
                ReadCard = "OKUTMAYA DEVAM ET";
                await PopupNavigation.Instance.PushAsync(new MessagePopup("Hata", "Lütfen bağlantı ayarlarınızı kontrol edin!"));
            }
            finally
            {
                //App.uhfService.Close();
            }
        }
        private async void OnTag()
        {
            try
            {
                RfidPopup popupPage = new RfidPopup(2);
                await PopupNavigation.Instance.PushAsync(popupPage);
                TerminalResult tResult = await popupPage.TaskCompletionSource;
                if (tResult.Result)
                {
                    TagNo = tResult.Data.ToString();
                }
                else if (tResult.ExceptionResult)
                {
                    await PopupNavigation.Instance.PushAsync(new MessagePopup("Hata", tResult.Message));
                }
            }
            catch
            {
                await PopupNavigation.Instance.PushAsync(new MessagePopup("Hata", "Lütfen bağlantı ayarlarınızı kontrol edin!"));
            }
            finally
            {
                // App.uhfService.Close();
            }
        }
        private async Task OnMatch()
        {
            try
            {
                if (SeriNoList.Count != 0 && tagNo != null && tagNo != "" && tagNo.Length == 5)
                {
                    QuestionPopup popupPage = new QuestionPopup("", "Bu etiketler belirlenen raflara yerleştirilsin mi?");
                    await PopupNavigation.Instance.PushAsync(popupPage);
                    if (await popupPage.TaskCompletionSource)
                    {

                        IList<Stock> data = new List<Stock>();
                        foreach (StockUI stock in SeriNoList)
                        {
                            data.Add(new Stock { RafKodu = stock.RafKodu, SeriNo = stock.SeriNo, StokAdi = stock.StokAdi, StokKodu = stock.StokKodu });
                        }
                        ////Store data .
                        //MobileResult mobileResult3 = await ApiService.GetCheckedStoreData(data).ConfigureAwait(false);
                        //if (mobileResult3.Result)
                        //{
                        //    Stock removedData = (Stock)mobileResult3.Data;
                        //    foreach (var item in removedData.)
                        //    {
                        //        data.Remove(data[0])
                        //    }

                        //}
                        MobileResult mobileResult = await ApiService.PostWarehouseLabels(data).ConfigureAwait(false);
                        if (mobileResult.Result)
                        {
                            //foreach (var item in tagInfos)
                            //{
                            //    MobileResult mobileResult2 = await ApiService.GetSimpleStoreDataTempTag4MamulCode(item.Trim().Substring(0, 8) + "-" + item.Trim().Substring(8, 4));
                            //    if (mobileResult.ExceptionResult == false)
                            //    {
                            //        if (mobileResult2.Result)
                            //        {
                            //            Stock item2 = (Stock)mobileResult2.Data;
                            //            var checkListItem = SeriNoList2.Any(x => x.SeriNo == item2.SeriNo);
                            //            if (!checkListItem)
                            //            {
                            //                SeriNoList2.Add(new StockUI
                            //                {
                            //                    RafKodu = item2.RafKodu,
                            //                    SeriNo = item2.SeriNo,
                            //                    StokAdi = item2.StokAdi,
                            //                    StokKodu = item2.StokKodu,
                            //                    MamulKodu = item2.MamulKodu,
                            //                    MamulStokAdi = item2.MamulStokAdi,
                            //                });
                            //                App.uhfService.Matching(item2);
                            //            }

                            //        }
                            //    }

                            //}
                            //if (SeriNoList2.Count() > 0)

                            //IList<Stock> tempData = new List<Stock>();
                            //foreach (StockUI stock in SeriNoList2)
                            //{
                            //    tempData.Add(new Stock { RafKodu = stock.RafKodu, SeriNo = stock.SeriNo, StokAdi = stock.StokAdi, StokKodu = stock.StokKodu, MamulKodu = stock.MamulKodu, MamulStokAdi = stock.MamulStokAdi });
                            //}
                   
                            var r = await ApiService.PostCellListTempMatching(data, TagNo).ConfigureAwait(false);
                            if (r.ExceptionResult == false)
                            {
                                await PopupNavigation.Instance.PushAsync(new MessagePopup("Başarılı", "Raf yerleştirmeniz başarılı bir şekilde tamamlandı!"));
                                SeriNoList.Clear();
                                //SeriNoList2.Clear();
                                //tagInfos.Clear();
                                App.uhfService.Clear();
                                TagNo = "";
                            }
                            else
                            {
                                await PopupNavigation.Instance.PushAsync(new MessagePopup("Uyarı", mobileResult.Message));
                                ClearVisible = true;
                            }



                        }
                        else
                        {
                            await PopupNavigation.Instance.PushAsync(new MessagePopup("Uyarı", mobileResult.Message));
                            ClearVisible = true;
                        }
                    }
                }
                else
                {
                    if (SeriNoList.Count != 0)
                        await PopupNavigation.Instance.PushAsync(new MessagePopup("Uyarı", "Etiketlerinizi okutmadınız!"));
                    else
                        await PopupNavigation.Instance.PushAsync(new MessagePopup("Uyarı", "Taglarınızı okutmadınız!"));
                }
            }
            catch (Exception ex)
            {
                await PopupNavigation.Instance.PushAsync(new MessagePopup("Hata", "Lütfen bağlantı ayarlarınızı kontrol edin!"));
            }
            finally
            {
                // App.uhfService.Close();
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
