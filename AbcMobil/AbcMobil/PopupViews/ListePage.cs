using Rg.Plugins.Popup.Animations;
using Rg.Plugins.Popup.Enums;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AbcMobil.PopupViews
{
    public class ListePage : PopupPage
    {
        private TaskCompletionSource<string> taskCompletionSource;
        public Task<string> PopupClosedTask
        {
            get { return taskCompletionSource.Task; }
        }
        private ObservableCollection<string> Liste { get;  set; }
        public ListePage(List<string> liste,string name)//int pozisyonX,int pozisyonY,int widthX,int gosterilecekData,bool arama
        {
            Liste = new ObservableCollection<string>(liste);
            Animation = new ScaleAnimation()
            {
                DurationIn = 600,
                DurationOut = 400,
                EasingIn = Easing.SinIn,
                EasingOut = Easing.SinOut,
                HasBackgroundAnimation = true,
                PositionIn = MoveAnimationOptions.Right,
                PositionOut = MoveAnimationOptions.Right,
                ScaleIn = 0.6,
                ScaleOut = 0.6
            };
            StackLayout layout = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions= LayoutOptions.End,
                WidthRequest=500
            };
            StackLayout ekran = new StackLayout();
            Frame frm = new Frame { Content = ekran,Padding=0,Margin=0 };
            StackLayout ustKisim = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
            };
            ImageButton closeButton = new ImageButton
            {
                Source = ImageSource.FromResource("AbcMobil.Assets.Icons.rigthIcon.png"),
                HeightRequest = 40,
                WidthRequest = 40,
                BackgroundColor = Color.Transparent,
                HorizontalOptions = LayoutOptions.EndAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            CollectionView collectionView = new CollectionView
            {
                SelectionMode = SelectionMode.Single, ItemsSource = Liste
            };
            collectionView.SelectionChanged += async (sender, e) => {
                taskCompletionSource.SetResult(e.CurrentSelection.FirstOrDefault().ToString());
                await PopupNavigation.Instance.PopAsync();
            };
            DataTemplate dataTemplate = new DataTemplate(() =>
            {
                Label lbl = new Label();
                lbl.SetBinding(Label.TextProperty, ".");
                return lbl;
            });
            SearchBar searchBar = new SearchBar
            {
                Placeholder = name,
                PlaceholderColor = Color.Blue,
                TextColor = Color.Blue,
                HorizontalOptions = LayoutOptions.Start,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(SearchBar)),
                FontAttributes = FontAttributes.None
            };
            closeButton.Clicked += async (sender, e) => {
                await PopupNavigation.Instance.PopAsync();
            };
            searchBar.TextChanged += (sender,e) => {
                var searchItem = e.NewTextValue;
                if (string.IsNullOrWhiteSpace(searchItem))
                    searchItem = string.Empty;
                var filteredItems = liste.Where(value => value.ToLowerInvariant().Contains(searchItem)).ToList();
                if(string.IsNullOrWhiteSpace(searchItem))
                    filteredItems=liste.ToList();
                foreach (string item in liste)
                    if (!filteredItems.Contains(item))
                        Liste.Remove(item);
                if (searchBar.Text == "")
                    Liste = new ObservableCollection<string>(liste);
            };
            layout.Children.Add(frm);
            ustKisim.Children.Add(searchBar);
            ustKisim.Children.Add(closeButton);
            ekran.Children.Add(ustKisim);
            ekran.Children.Add(collectionView);
            collectionView.ItemTemplate = dataTemplate;
            Content = layout;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (taskCompletionSource == null)
                taskCompletionSource = new TaskCompletionSource<string>();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}
