using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using AbcMobil.Droid.Services;
using Xamarin.Forms;
using AbcMobil.PopupViews;
using Android.Content;
using Android.Media;
using System.Collections.Generic;
using Rg.Plugins.Popup.Services;
using AbcMobil.Models;
using Java.Util;
using System;
//using AbcMobil.Droid.Models;

namespace AbcMobil.Droid
{
    [Activity(Label = "Qent-Soft", Icon = "@mipmap/qenticon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        //private IScanner _scanner = null;
        public static Context context;
       // public static string barcode="";
        // public static BarcodeReaderReceiver barcodeReaderReceiver;
        public override async void OnBackPressed()
        {
            try
            {
                
                switch (Shell.Current.CurrentState.Location.ToString())
                {
                    case "//MainPage":
                        //service.playAnkaraBaglari();
                        QuestionPopup popupPage = new QuestionPopup("", "Çıkış yapmak istediğinize emin misiniz?\nÇıkış ile birlikte sayımlar sıfırlanır!", "EVET", "HAYIR");
                        await PopupNavigation.Instance.PushAsync(popupPage);
                        if (await popupPage.TaskCompletionSource)
                        {
                            UHFService.down = true;
                            base.OnBackPressed();
                        }
                        break;
                    default:
                        QuestionPopup popupPagee = new QuestionPopup("", "Okutulan listeler silinsin mi?", "EVET", "HAYIR");
                        await PopupNavigation.Instance.PushAsync(popupPagee);
                        if (await popupPagee.TaskCompletionSource)
                        {
                            UHFService.down = true;
                            await Shell.Current.GoToAsync("//MainPage");
                            
                        }
                        //await Shell.Current.GoToAsync("..//");
                        break;
                }

            }
            catch
            {
            }
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            //MyBroadcastReceiver receiver = new MyBroadcastReceiver();
            context = this;
            //IntentFilter filter = new IntentFilter();
            //filter.AddAction("com.rfid.SCAN");
            //registerReceiver(receiver, filter);
            base.OnCreate(savedInstanceState);
            Rg.Plugins.Popup.Popup.Init(this);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            //IntentFilter filter = new IntentFilter();
            //filter.AddAction("com.rfid.SCAN");    
            //if(barcodeReaderReceiver == null)
            //    barcodeReaderReceiver = new BarcodeReaderReceiver();
            //RegisterReceiver(barcodeReaderReceiver, filter);
            LoadApplication(new App(new UHFService(),new MAudioService() , new Services.Device()));
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        } 
    }
}