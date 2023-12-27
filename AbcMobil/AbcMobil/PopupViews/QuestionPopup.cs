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
    public class QuestionPopup:PopupPage
    {
        private TaskCompletionSource<bool> _taskCompletionSource;
        public Task<bool> TaskCompletionSource => _taskCompletionSource.Task;
        bool result=false;
        public QuestionPopup(string title = "", string message = "",string btnOk="Onayla",string btnCancel="İptal Et",string titleColorHex = "", string textColorHex = "")
        {
            Animation = new ScaleAnimation()
            {
                DurationIn =400,
                DurationOut = 300,
                EasingIn = Easing.SinOut,
                EasingOut = Easing.SinIn,
                HasBackgroundAnimation = true,
                PositionIn = MoveAnimationOptions.Bottom,
                PositionOut = MoveAnimationOptions.Bottom,
                
                //ScaleIn = 0.6,
                //ScaleOut = 0.6
            };
            Grid layout=new Grid
            {
                VerticalOptions=LayoutOptions.EndAndExpand,
                HorizontalOptions=LayoutOptions.FillAndExpand,
                RowDefinitions=
                {
                    new RowDefinition { Height = new GridLength(3, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(3, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                }
            };
            StackLayout inLayout = new StackLayout
            {
                HorizontalOptions=LayoutOptions.CenterAndExpand,
                VerticalOptions=LayoutOptions.CenterAndExpand,
            };
            StackLayout buttonLayout = new StackLayout();
            StackLayout buttonHorizontal = new StackLayout { Orientation = StackOrientation.Horizontal };
            Frame frm1 = new Frame
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 200,
                CornerRadius=20,
                Margin = new Thickness(0, 0, 0, -23),
                Content = inLayout
            };
            if(title=="" &&message=="")
                frm1.BackgroundColor= Color.Transparent;
            Frame frm2 = new Frame
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest=200,
                Content=buttonLayout
            };
            Label lbl1 = new Label
            {
                Text = title,FontSize = 20,
            };
            if (titleColorHex == "")
                lbl1.TextColor=Color.FromHex("1C658C");
            else
                lbl1.TextColor = Color.FromHex(titleColorHex);

            Label lbl2 = new Label
            {
                Text = message,FontSize=18
            };
            if (textColorHex == "")
                lbl2.TextColor = Color.FromHex("1C658C");
            else
                lbl2.TextColor = Color.FromHex(textColorHex);

            if (title != "")
                inLayout.Children.Add(lbl1);
            inLayout.Children.Add(lbl2);
            Button btn1 = new Button
            {
                Text = btnOk,
                BackgroundColor=Color.Green,
                HorizontalOptions = LayoutOptions.FillAndExpand,CornerRadius=10,
            };
            Button btn2 = new Button
            {
                Text = btnCancel,
                BackgroundColor = Color.Red,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                CornerRadius = 10,
            };
            buttonHorizontal.Children.Add(btn1);
            buttonHorizontal.Children.Add(btn2);
            buttonLayout.Children.Add(buttonHorizontal);
            btn1.Clicked += async (sender, e)=>{
                result = true;
                await PopupNavigation.Instance.PopAllAsync();
            };
            btn2.Clicked += async (sender, e) => {
                result = false;
                await PopupNavigation.Instance.PopAllAsync();
            };
            layout.Children.Add(frm1,0,2);
            layout.Children.Add(frm2,0,3);
            Content = layout;
        }
        protected override void OnDisappearing()
        {
            _taskCompletionSource.SetResult(result);
            base.OnDisappearing();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (_taskCompletionSource == null)
                _taskCompletionSource = new TaskCompletionSource<bool>();
        }
    }
}
