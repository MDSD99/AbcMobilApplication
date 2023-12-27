using Xamarin.Forms;

namespace AbcMobil.Views
{
    public class SplashScreenPage : ContentPage
    {
        Image splashImage;
        public SplashScreenPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            AbsoluteLayout layout = new AbsoluteLayout();
            splashImage = new Image
            {
                Source = ImageSource.FromResource("AbcMobil.Assets.Images.logo.png"),
                WidthRequest = 400,
                HeightRequest = 400,
            };
            AbsoluteLayout.SetLayoutFlags(splashImage, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(splashImage, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            layout.Children.Add(splashImage);
            Content= layout;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await splashImage.ScaleTo(1, 600);
            await splashImage.ScaleTo(0.4, 500, Easing.Linear);
            await splashImage.ScaleTo(1.1, 200, Easing.Linear);
            //await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
            Application.Current.MainPage = new ShellPage();
        }
    }
}
