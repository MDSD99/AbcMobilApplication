using AbcMobil.Models;
using AbcMobil.PopupViews;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AbcMobil.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        public static string jsonFileName = Path.Combine(App.PersonelFolderPath, "RfidSettings.json");
        private int censusReadPower = 33;
        private int ticketCensusReadPower = 33;
        private int searchReadPower = 33;
        private int tagReadPower = 7;
        private int tagWritePower = 7;
        private int ticketReadPower = 7;
        private int ticketWritePower = 7;
        private int matchingReadPower = 7;
        private int matchingTagReadPower = 7;
        private int matchingPower = 7;
        private int exitPower = 7;
        public int ExitPower
        {
            get => exitPower;
            set
            {
                exitPower = value;
                OnPropertyChanged(nameof(ExitPower));
            }
        }
        public int CensusReadPower
        {
            get => censusReadPower; set
            {
                censusReadPower = value;
                OnPropertyChanged(nameof(CensusReadPower));
            }
        }
        public int TicketCensusReadPower
        {
            get => ticketCensusReadPower; set
            {
                ticketCensusReadPower = value;
                OnPropertyChanged(nameof(TicketCensusReadPower));
            }
        }
        public int SearchReadPower
        {
            get => searchReadPower; set
            {
                searchReadPower = value;
                OnPropertyChanged(nameof(SearchReadPower));
            }
        }
        public int TagReadPower
        {
            get => tagReadPower; set
            {
                tagReadPower = value;
                OnPropertyChanged(nameof(TagReadPower));
            }
        }
        public int TagWritePower
        {
            get => tagWritePower; set
            {
                tagWritePower = value;
                OnPropertyChanged(nameof(TagWritePower));
            }
        }
        public int TicketReadPower
        {
            get => ticketReadPower; set
            {
                ticketReadPower = value;
                OnPropertyChanged(nameof(TicketReadPower));
            }
        }
        public int TicketWritePower
        {
            get => ticketWritePower; set
            {
                ticketWritePower = value;
                OnPropertyChanged(nameof(TicketWritePower));
            }
        }
        public int MatchingReadPower
        {
            get => matchingReadPower; set
            {
                matchingReadPower = value;
                OnPropertyChanged(nameof(MatchingReadPower));
            }
        }
        public int MatchingTagReadPower
        {
            get => matchingTagReadPower; set
            {
                matchingTagReadPower = value;
                OnPropertyChanged(nameof(MatchingReadPower));
            }
        }
        public int MatchingPower
        {
            get => matchingPower; set
            {
                matchingPower = value;
                OnPropertyChanged(nameof(MatchingPower));
            }
        }
        public SettingsViewModel()
        {
            TicketWritePower = RfidSettings.Instance.TicketWritePower;
            TicketReadPower = RfidSettings.Instance.TicketReadPower;
            TicketCensusReadPower = RfidSettings.Instance.TicketCensusReadPower;
            TagReadPower = RfidSettings.Instance.TagReadPower;
            TagWritePower = RfidSettings.Instance.TagWritePower;
            CensusReadPower = RfidSettings.Instance.CensusReadPower;
            SearchReadPower = RfidSettings.Instance.SearchReadPower;
            MatchingReadPower = RfidSettings.Instance.MatchingReadPower;
            MatchingTagReadPower = RfidSettings.Instance.MatchingTagReadPower;
            MatchingPower = RfidSettings.Instance.MatchingPower;
            //PowerList = new ObservableCollection<string>();
            //PowerList.Clear();
            //RegionList = new ObservableCollection<string>{ "PRC","NA","NONE","KR","EU","EU2","EU3"};
            //RegionList.Clear();

            //for (int i = 0; i <= 33; i++)
            //    PowerList.Add(i.ToString());
            //PowerCommand = new Command(async () => await OnGucSec());
            //ItemTappedRegion = new Command<string>(OnItemTappedFrekans);
            //ItemTappedPower = new Command<string>(OnItemTappedGuc);
            //UnlockCommand = new Command(async () => await OnUnLock());
            //LockCommand = new Command(async () => await OnLock());
            SaveCommand = new Command(OnSave);
        }
        public ICommand SearchTagCommand { get; }
        public ICommand SaveCommand { get; }
        private bool Control()
        {
            if (
       ticketCensusReadPower < 5 || ticketCensusReadPower > 33 ||
       censusReadPower < 5 || censusReadPower > 33 ||
       searchReadPower < 5 || searchReadPower > 33 ||
       tagReadPower < 5 || tagReadPower > 33 ||
       tagWritePower < 5 || tagWritePower > 33 ||
       ticketReadPower < 5 || ticketReadPower > 33 ||
       ticketWritePower < 5 || ticketWritePower > 33 ||
       matchingReadPower < 5 || matchingReadPower > 33 ||
       matchingTagReadPower < 5 || matchingTagReadPower > 33 ||
       matchingPower < 5 || matchingPower > 33
                    )
            {
                return false;

            }
            else
                return true;
        }
        private async void OnSave()
        {
            try
            {
                if (Control())
                {
                    RfidSettings.Instance.CensusReadPower = CensusReadPower;
                    RfidSettings.Instance.TicketCensusReadPower = TicketCensusReadPower;
                    RfidSettings.Instance.SearchReadPower = SearchReadPower;
                    RfidSettings.Instance.TagReadPower = TagReadPower;
                    RfidSettings.Instance.TagWritePower = TagWritePower;
                    RfidSettings.Instance.TicketReadPower = TicketReadPower;
                    RfidSettings.Instance.TicketWritePower = TicketWritePower;
                    RfidSettings.Instance.MatchingReadPower = MatchingReadPower;
                    RfidSettings.Instance.MatchingTagReadPower = MatchingTagReadPower;
                    RfidSettings.Instance.MatchingPower = MatchingPower;
                    RfidSettings.Instance.ExitPower = ExitPower;
                    if (!File.Exists(jsonFileName))
                    {
                        FileStream file = File.Create(jsonFileName);
                        file.Close();
                    }
                    string jsondata = JsonConvert.SerializeObject(RfidSettings.Instance);
                    File.WriteAllText(jsonFileName, jsondata);
                    await PopupNavigation.Instance.PushAsync(new MessagePopup("Başarılı", "Güç ayarlarınız başarılı bir şekilde kaydedildi..."));
                }
                else
                {
                    await PopupNavigation.Instance.PushAsync(new MessagePopup("Uyarı", "Güç ayarlarınız 5-33 arasın da olmalı!"));
                }

            }
            catch 
            {
                await PopupNavigation.Instance.PushAsync(new MessagePopup("Hata","Lütfen bağlantı ayarlarınızı kontrol edin!"));
            }
        }
    }
}
