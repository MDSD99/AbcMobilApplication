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
    public class RfidReadPopup : PopupPage
    {
        private TaskCompletionSource<bool> _taskCompletionSource;
        public Task<bool> taskCompletionSource=>_taskCompletionSource.Task;
       // bool Readed = false;
        public RfidReadPopup(string text = "")
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
                WidthRequest = 250,
                HeightRequest = 50,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
                BackgroundColor = Color.FromHex("1C658C"),
                HasShadow = true,
                BorderColor = Color.DarkGray,
                Content = layout
            };
            //try
            //{

            //}
            //catch (Exception ex)
            //{

            //}
            Label labelTop = new Label
            {
                Text = text,
                TextColor = Color.White,
                FontSize = 30,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            //Device.StartTimer(TimeSpan.FromMilliseconds(5000), () =>
            //{
            //    if (!Readed)
            //    {
            //        Device.BeginInvokeOnMainThread(async () =>
            //        {
            //            Readed = true;
            //            await PopupNavigation.Instance.PopAsync();
            //        });
            //    }
            //    if(!Readed)
            //        return true;
            //    else 
            //        return false;
            //});
            layout.Children.Add(labelTop);
            this.Content = frm; 
        }
        protected override void OnDisappearing()
        {
            //Readed = true;
            base.OnDisappearing();
            _taskCompletionSource.SetResult(true);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if(_taskCompletionSource==null)
                _taskCompletionSource = new TaskCompletionSource<bool>();
        }
    }
}
