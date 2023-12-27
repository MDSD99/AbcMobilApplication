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
    public class TicketSearchViewModel : BaseViewModel
    {
        private int searchType = 0;
        private string searchText="";
        public string TextTitle
        {
            get => searchType == 0 ? "Stok Adı" : "Stok Kodu";
        }
        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                OnPropertyChanged(nameof(SearchText));
                OnTextChanged();
            }
        }
        public int SearchType
        {
            get => searchType;
            set
            {
                searchType = value;
                OnPropertyChanged(nameof(SearchType));
                OnPropertyChanged(nameof(TextTitle));
            }
        }
        public void OnTextChanged()
        {
            if (SearchText == null || SearchText == "")
            {
                DataList.Clear();
                foreach (ShelfInfo item in DatabaseData)
                {
                    DataList.Add(item);
                }
            }
            else
            {
                if(SearchType == 0)
                {
                    DataList.Clear();
                    foreach (ShelfInfo item in DatabaseData.Where(s => s.Title1.ToLower().Contains(SearchText.ToLower())).ToList())
                    {
                        DataList.Add(item);
                    }
                }
                else
                {
                    DataList.Clear();
                    foreach (ShelfInfo item in DatabaseData.Where(s => s.Title2.ToLower().Contains(SearchText.ToLower())).ToList())
                    {
                        DataList.Add(item);
                    }
                }
            }
        }
        public ObservableCollection<ShelfInfo> DataList { get;  }
        public IList<ShelfInfo> DatabaseData;
        public ICommand LoadCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand SearchTextCommand { get; }
        public TicketSearchViewModel()
        {
            DataList = new ObservableCollection<ShelfInfo>();
            if (DatabaseData == null)
                DatabaseData = new List<ShelfInfo>();
            LoadCommand = new Command(async () => await OnLoad());
            SearchCommand = new Command<ShelfInfo>(OnSearch);
            SearchTextCommand = new Command(OnTextSearch);
        }
        private async void OnTextSearch()
        {
            DataListPopup popupPage = new DataListPopup(null, 0, "");
            await PopupNavigation.Instance.PushAsync(popupPage);
            MobileResult terminal = await popupPage.TaskCompletionSource;
            if (terminal.Result)
            {
                SearchType = (int)terminal.Data;
            }
        }
        private async void OnSearch(ShelfInfo sender)
        {
            await PopupNavigation.Instance.PushAsync(new DataListPopup(sender,0,sender.Title1));
        }
        private async Task OnLoad()
        {
            IsBusy = true;
            try
            {
                Color color;
                Random randomm = new Random();
                ShelfInfo infos;
                MobileResult mobileResult = await ApiService.GetFullStore();
                if (mobileResult.Result)
                {
                    DataList.Clear();
                    DatabaseData.Clear();
                    int Id = -1;
                    foreach (Stock item in (IList<Stock>)mobileResult.Data)
                    {
                        Id = DatabaseData.Where(s => s.Title1 == item.StokAdi).Select(s => s.Id).FirstOrDefault();
                        color = Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256));
                        if (Id == 0)
                        {
                            infos = new ShelfInfo(item.StokAdi,item.StokKodu, 1, DatabaseData.Count + 1, color);
                            infos.Add(new StockUI
                            {
                                HeaderColor = color,
                                ContentColor = Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256)),
                                IsVisible = true,
                                RafKodu = item.RafKodu,
                                SeriNo = item.SeriNo,
                                StokAdi = item.StokAdi,
                                StokKodu = item.StokKodu
                            });
                            //DataList.Add(infos);
                            DatabaseData.Add(infos);
                        }
                        else
                        {
                            //DataList[Id - 1].Amount += 1;
                            //DataList[Id - 1].Add(new StockUI
                            //{
                            //    HeaderColor = DataList[Id - 1].HeaderColor,
                            //    ContentColor = Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256)),
                            //    IsVisible = true,
                            //    RafKodu = item.RafKodu,
                            //    SeriNo = item.SeriNo,
                            //    StokAdi = item.StokAdi,
                            //    StokKodu = item.StokKodu
                            //});
                            DatabaseData[Id - 1].Amount += 1;
                            DatabaseData[Id - 1].Add(new StockUI
                            {
                                HeaderColor = DatabaseData[Id - 1].HeaderColor,
                                ContentColor = Color.FromRgb(randomm.Next(0, 256), randomm.Next(0, 256), randomm.Next(0, 256)),
                                IsVisible = true,
                                RafKodu = item.RafKodu,
                                SeriNo = item.SeriNo,
                                StokAdi = item.StokAdi,
                                StokKodu = item.StokKodu
                            });
                        }
                    }
                    OnTextChanged();
                }
                else if (mobileResult.ExceptionResult)
                {
                    await PopupNavigation.Instance.PushAsync(new MessagePopup("Hata", mobileResult.Message));
                }
                else
                {

                }
            }
            catch 
            {
                await PopupNavigation.Instance.PushAsync(new MessagePopup("Hata", "Lütfen bağlantı ayarlarınızı kontrol edin!"));
            }
            finally
            {
                IsBusy = false;
                //App.uhfService.Close();
            }
        }
    }
}
