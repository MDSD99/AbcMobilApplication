using AbcMobil.Models;
using Rg.Plugins.Popup.Animations;
using Rg.Plugins.Popup.Enums;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AbcMobil.PopupViews
{
    public class RfidWritePopup:PopupPage
    {
        private TaskCompletionSource<TerminalResult> taskCompletionSource;
        public Task<TerminalResult> CompSource => taskCompletionSource.Task;
        private bool Readed = false;
        public RfidWritePopup(string text,int type=0)
        {
            Animation = new ScaleAnimation()
            {
                DurationIn = 400,
                DurationOut = 300,
                EasingIn = Easing.SinOut,
                EasingOut = Easing.SinIn,
                HasBackgroundAnimation = true,
                PositionIn = MoveAnimationOptions.Left,
                PositionOut = MoveAnimationOptions.Right,
            };
            StackLayout layout = new StackLayout();
            Frame frm = new Frame
            {
                WidthRequest = 350,
                HeightRequest = 50,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = Color.FromHex("1C658C"),
                HasShadow = true,
                BorderColor = Color.DarkGray,
                Content = layout
            };
            Label labelTop = new Label
            {
                Text = "Rfid kartı yaklaştırın!",
                TextColor = Color.White,
                FontSize = 30,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            layout.Children.Add(labelTop);
            //ImageButton image = new ImageButton
            //{
            //    WidthRequest = 250,
            //    HeightRequest = 350,
            //    BorderColor = Color.DarkGray,
            //    IsEnabled = true,
            //};
            //image.Source = ImageSource.FromResource("AbcMobil.Assets.Icons.tagIcon.png");
            TerminalResult tResult = new TerminalResult { Result = false, Data = null, ExceptionResult = false, Message = "" };
            //layout.Children.Add(image);

            Device.StartTimer(TimeSpan.FromMilliseconds(500), () =>
            {
                //cts.Token
                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (!tResult.Result)
                    {
                        if(type==0)
                            tResult = App.uhfService.WriteSerialNumber(text, RfidSettings.Instance.TicketReadPower, RfidSettings.Instance.TicketWritePower );
                        else if(type==2)
                            tResult = App.uhfService.WriteTag(text, RfidSettings.Instance.TagReadPower, RfidSettings.Instance.TagWritePower );
                        if (tResult.Result)
                        {
                            Readed = true;
                            App.audioService.playCensus(1);
                            tResult.Result = true;
                            tResult.Data = tResult.Data;
                            await PopupNavigation.Instance.PopAsync();
                            taskCompletionSource.SetResult(tResult);
                        }
                    }
                });
                if (Readed)
                    return false;
                else
                    return true;
            });
            this.Content = frm;
        }
        protected override void OnDisappearing()
        {
            Readed = true;
            base.OnDisappearing();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (taskCompletionSource == null)
                taskCompletionSource = new TaskCompletionSource<TerminalResult>();
            Readed = false;
        }
    }
}
