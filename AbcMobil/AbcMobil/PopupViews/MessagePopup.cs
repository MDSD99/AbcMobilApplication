using Rg.Plugins.Popup.Animations;
using Rg.Plugins.Popup.Enums;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace AbcMobil.PopupViews
{
    public class MessagePopup : PopupPage
    {
        public MessagePopup(string title = "", string message = "", string textColorHex = "", string btnCancel = "Onayla")
        {
            Animation = new ScaleAnimation()
            {
                DurationIn = 400,
                DurationOut = 300,
                EasingIn = Easing.SinOut,
                EasingOut = Easing.SinIn,
                HasBackgroundAnimation = true,
                PositionIn = MoveAnimationOptions.Right,
                PositionOut = MoveAnimationOptions.Left
            };
            Grid layout;
            if (title == "")
                layout = new Grid
                {
                    VerticalOptions = LayoutOptions.StartAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(1.5, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(3, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(3, GridUnitType.Star) }
                }
                };
            else
                layout = new Grid
                {
                    VerticalOptions = LayoutOptions.StartAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(3, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(3, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(3, GridUnitType.Star) }
                }
                };
            StackLayout 
                stackLayout = new StackLayout
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Spacing = 10
                };
            Frame frm1;
            if(title!="")
            frm1 = new Frame
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 200,
                Content = stackLayout
            };
            else
                frm1 = new Frame
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    HeightRequest = 100,
                    Content = stackLayout
                };
            if (title == "" && message == "")
                frm1.BackgroundColor = Color.Transparent;
            Label lbl1 = new Label
            {
                Text = title,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 20,
            };
            lbl1.TextColor = Color.FromHex("1C658C");
            Label lbl2 = new Label
            {
                Text = message,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 18
            };
            lbl2.HorizontalOptions = LayoutOptions.CenterAndExpand;
            if (textColorHex == "")
                lbl2.TextColor = Color.FromHex("1C658C");
            else
                lbl2.TextColor = Color.FromHex(textColorHex);

            stackLayout.Children.Add(lbl1);
            stackLayout.Children.Add(lbl2);
            Grid myBtnLayout = new Grid
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                ColumnDefinitions =
                {
                    new ColumnDefinition{ Width =new GridLength(2, GridUnitType.Star) },
                    new ColumnDefinition{ Width =new GridLength(8, GridUnitType.Star) }
                }
            };
            Frame myBtn = new Frame
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                BorderColor = Color.FromHex("1C658C"),
                BackgroundColor = Color.White,
                CornerRadius = 20,
                Margin = 0,
                Padding = 5,
                Content = myBtnLayout
            };
            Image myBtnImage = new Image
            {
                Source = ImageSource.FromResource("AbcMobil.Assets.Icons.okeyIcon.png"),
            };
            Label myBtnLabel = new Label
            {
                FontSize = 18,
                Text = "Tamam",
                TextColor = Color.FromHex("1C658C")
            };
            myBtnLayout.Children.Add(myBtnImage, 0, 0);
            myBtnLayout.Children.Add(myBtnLabel, 1, 0);
            switch (title)
            {
                case "Uyarı":
                    lbl1.TextColor = Color.FromHex("FFDBA91D");
                    lbl2.TextColor = Color.FromHex("FFDBA91D");
                    myBtn.BorderColor = Color.FromHex("FFDBA91D");
                    myBtnImage.Source = ImageSource.FromResource("AbcMobil.Assets.Icons.yellowOkeyIcon.png");
                    myBtnLabel.TextColor = Color.FromHex("FFDBA91D");
                    break;
                case "Hata":
                    lbl1.TextColor = Color.FromHex("FFDB201D");
                    lbl2.TextColor = Color.FromHex("FFDB201D");
                    myBtn.BorderColor = Color.FromHex("FFDB201D");
                    myBtnImage.Source = ImageSource.FromResource("AbcMobil.Assets.Icons.redOkeyIcon.png");
                    myBtnLabel.TextColor = Color.FromHex("FFDB201D");
                    break;
                case "Başarılı":
                    break;
                default:
                    break;
            }
            TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += async (s, e) =>
            {
                await PopupNavigation.Instance.PopAsync();
            };
            myBtn.GestureRecognizers.Add(tapGestureRecognizer);
            myBtn.HorizontalOptions = LayoutOptions.EndAndExpand;
            stackLayout.Children.Add(myBtn);
            layout.Children.Add(frm1, 0, 0);
            Content = layout;
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}
