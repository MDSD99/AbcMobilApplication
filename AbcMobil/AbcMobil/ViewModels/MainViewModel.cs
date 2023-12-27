using AbcMobil.Models;
using AbcMobil.PopupViews;
using AbcMobil.Views;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AbcMobil.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public readonly string Username = "Terminal1";
        public readonly string Password = "Terminal1789123";
        public static string jsonFileName = Path.Combine(App.PersonelFolderPath, "RfidSettings.json");
        public static string UserToken ="eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Ik1laG1ldCIsIkpvYiI6IkhhbmRoZWxkVGVybWluYWwiLCJQYXNzd29yZCI6IjE5MDUiLCJDb21wYW55IjoiUWVudFNvZnQiLCJSb2xlIjoiQWRtaW4iLCJuYmYiOjE2NDY0NzQ5MzksImV4cCI6MTY3ODAxMDkzOSwiaWF0IjoxNjQ2NDc0OTM5fQ.SfYKpQeTWPdq3kdpRdknd4avs2svhh6CRJ546l89imQ";
        public static DateTime Expiration = DateTime.Now;

        public MainViewModel()
        {

            if (File.Exists(jsonFileName))
            {
                string jsondata = File.ReadAllText(jsonFileName);
                RfidSettings setting = JsonConvert.DeserializeObject<RfidSettings>(jsondata);
                if (setting != null)
                {
                    RfidSettings.Instance.CensusReadPower = setting.CensusReadPower;
                    RfidSettings.Instance.TicketCensusReadPower = setting.TicketCensusReadPower;
                    RfidSettings.Instance.SearchReadPower = setting.SearchReadPower;
                    RfidSettings.Instance.MatchingReadPower = setting.MatchingReadPower;
                    RfidSettings.Instance.MatchingTagReadPower = setting.MatchingTagReadPower;
                    RfidSettings.Instance.MatchingPower = setting.MatchingPower;
                    RfidSettings.Instance.TagReadPower = setting.TagReadPower;
                    RfidSettings.Instance.TagWritePower = setting.TagWritePower;
                    RfidSettings.Instance.TicketWritePower = setting.TicketWritePower;
                    RfidSettings.Instance.TicketReadPower = setting.TicketReadPower;
                    RfidSettings.Instance.ExitPower = setting.ExitPower;
                }
            }
        }
        public IList<string> CountryList
        {
            get
            {
                return new List<string> { "USA", "Germany", "England" };
            }
        }
        public IList<string> slideList
        {
            get => new List<string> { "RFID ETİKETLEME", "Desktop/Mobil Uygulaması", "Otomasyon Uygulaması" };
        }

        private ICommand kutuphaneGetir;
        private ICommand pageOpen;
        public ICommand KutuphaneGetir => (kutuphaneGetir ?? (kutuphaneGetir = new Command(async () => await OnGetir())));
        public ICommand PageOpen => (pageOpen ?? (pageOpen = new Command<string>(async (page) => await OnPageOpen(page))));
        private string hexSayi;
        private string metin;
        public string HexSayi
        {
            get => hexSayi;
            set
            {
                hexSayi = value;
                OnPropertyChanged(nameof(HexSayi));
            }
        }
        public string Metin
        {
            get => metin;
            set
            {
                metin = value;
                OnPropertyChanged(nameof(Metin));
            }
        }
        private async Task OnPageOpen(string page)
        {
            //if ((UserToken == "") || Expiration <= DateTime.Now)
            //{
            //    Token fullToken = await ApiService.GetToken(Username, Password);
            //    if (fullToken == null) return;
            //    UserToken = fullToken.token;
            //    Expiration = fullToken.expiration;
            //}
           
            try
            {
                switch (page)
                {
                    case "Census":
                        await Shell.Current.GoToAsync($"//{nameof(CensusPage)}");
                        break;
                    case "TicketSearch":
                        await Shell.Current.GoToAsync($"//{nameof(TicketSearchPage)}");
                        break;
                    case "ShelfMatching":
                        await Shell.Current.GoToAsync($"//{nameof(TagMatchingPage)}");
                        break;
                    case "ShelfRead":
                        RfidPopup tagRead = new RfidPopup(2);
                        await PopupNavigation.Instance.PushAsync(tagRead);
                        break;
                    case "ShelfWrite":
                        BarcodePopup tagBarcode = new BarcodePopup(2);
                        await PopupNavigation.Instance.PushAsync(tagBarcode);
                        break;
                    case "TicketRead":
                        //await Shell.Current.GoToAsync($"//{nameof(TicketReadPage)}");
                        RfidPopup rfidPopup = new RfidPopup(0);
                        await PopupNavigation.Instance.PushAsync(rfidPopup);
                        break;
                    case "TicketWrite":
                        BarcodePopup popupPage = new BarcodePopup();
                        await PopupNavigation.Instance.PushAsync(popupPage);
                        //await Shell.Current.GoToAsync($"//{nameof(TicketWritePage)}");
                        break;
                    case "Settings":
                        await Shell.Current.GoToAsync($"//{nameof(SettingsPage)}");
                        break;
                    case "Rapor":
                        await Shell.Current.GoToAsync($"//{nameof(ReportSearchPage)}");
                        break;
                    case "TicketCensus":
                        await Shell.Current.GoToAsync($"//{nameof(TicketCensusPage)}");
                        break;
                    case "TagExit":
                        await Shell.Current.GoToAsync($"//{nameof(TagExitPage)}");
                        break;
                    case "StoreImport":
                        await Shell.Current.GoToAsync($"//{nameof(TagExitPage)}");
                        break;
                    case "TagProductList":
                        await Shell.Current.GoToAsync($"//{nameof(TagProductListPage)}");
                        break;
                    case "TempNumberTag":
                        await Shell.Current.GoToAsync($"//{nameof(TempTagNumber)}");
                        break;
                    default:
                        // await PopupNavigation.Instance.PushAsync(new DenemePopup(null,null));
                        break;
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Hata", "Hata messajı : " + ex.Message, "Ok", "Cancel");
            }
        }
        private async Task OnGetToken()
        {
            try
            {
                await ApiService.GetToken(Username, Password);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Hata", "Bağlantı Hatası : " + ex.Message, "Ok", "Cancel");
            }
        }
        private async Task OnGetir()
        {
            try
            {
                await Shell.Current.GoToAsync($"//{nameof(SettingsPage)}");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Hata", "Hata messajı : " + ex.Message, "Ok", "Cancel");
            }
        }
    }
}
