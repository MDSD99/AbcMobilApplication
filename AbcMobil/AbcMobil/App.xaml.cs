using AbcMobil.Services;
using AbcMobil.Views;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AbcMobil
{
    public partial class App : Application
    {
        public static IUHFService uhfService { get; private set; }
        //public static IBarcodeService barcodeService { get; private set; }
        public static IMAudioService audioService { get; private set; }
        public static IDevice device { get; private set; }
        public static string FolderPath { get; private set; }
        public static string PersonelFolderPath { get; private set; }
        public static string DeviceID { get; private set; }
        public App(IUHFService _rfidService,IMAudioService _audioService , IDevice _device)
        {
            InitializeComponent();

            FolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            PersonelFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
            DependencyService.Register<DataService>();
            uhfService = _rfidService;
            device = _device;
            //barcodeService = _barcodeService;
            audioService = _audioService;
            MainPage = new Views.SplashScreenPage();

            //DependencyService.Register<MockDataStore>();
        }
       
        protected override void OnStart()
        {
            DeviceID = device.GetIdentifier();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
