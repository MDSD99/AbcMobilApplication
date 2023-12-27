using AbcMobil.Models;
using AbcMobil.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AbcMobil.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CensusPage : ContentPage
    {
        private readonly CensusViewModel viewModel;
        public CensusPage()
        {
            InitializeComponent();
            viewModel = new CensusViewModel();
            BindingContext = viewModel;
            #region Eski
            //StackLayout layout = new StackLayout
            //{
            //    Margin = new Thickness(12, 10, 12, 10),
            //    Spacing = 10
            //};
            //StackLayout stack = new StackLayout
            //{
            //    Orientation = StackOrientation.Horizontal
            //};
            //Button button1 = new Button
            //{
            //    BackgroundColor = Color.FromHex("1C658C"),
            //    Command = viewModel.CensusStartCommand,
            //    CornerRadius = 5,
            //    HorizontalOptions = LayoutOptions.FillAndExpand
            //};
            //button1.SetBinding(Button.TextProperty, viewModel.CensusStart);
            //Button button2 = new Button
            //{
            //    BackgroundColor = Color.FromHex("1C658C"),
            //    Command = viewModel.CensusFinishCommand,
            //    Text = "SAYIMI BİTİR",
            //    CornerRadius = 5,
            //    HorizontalOptions = LayoutOptions.FillAndExpand
            //};
            //stack.Children.Add(button1);
            //stack.Children.Add(button2);
            //CollectionView listData = new CollectionView
            //{
            //    IsGrouped = true,
            //    SelectionMode = SelectionMode.None,
            //    ItemsLayout = new GridItemsLayout(0, ItemsLayoutOrientation.Vertical),
            //    GroupHeaderTemplate = new DataTemplate(() =>
            //    {
            //        TapGestureRecognizer headerTapped = new TapGestureRecognizer()
            //        {
            //            Command = viewModel.TitleCommand,
            //        };
            //        headerTapped.SetBinding(TapGestureRecognizer.CommandParameterProperty, "Id");
            //        Grid groupTitle = new Grid
            //        {
            //            RowDefinitions =
            //            {
            //                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
            //            },
            //            Margin = new Thickness(0, 10, 0, 0),
            //            GestureRecognizers = { headerTapped }
            //        };
            //        BoxView boxView = new BoxView
            //        {
            //            Color = Color.DarkSlateGray,
            //            HeightRequest = 1,
            //            HorizontalOptions = LayoutOptions.Fill,
            //            VerticalOptions = LayoutOptions.EndAndExpand
            //        };
            //        Label label1 = new Label
            //        {
            //            BackgroundColor = Color.White,
            //            Margin = new Thickness(10, 0, 0, 0),
            //            VerticalOptions = LayoutOptions.EndAndExpand,
            //            HorizontalOptions = LayoutOptions.StartAndExpand
            //        };
            //        Label label2 = new Label
            //        {
            //            BackgroundColor = Color.White,
            //            Margin = new Thickness(10, 0, 0, 0),
            //            VerticalOptions = LayoutOptions.EndAndExpand,
            //            HorizontalOptions = LayoutOptions.StartAndExpand
            //        };
            //        Label label3 = new Label
            //        {
            //            BackgroundColor = Color.White,
            //            Margin = new Thickness(0, 0, 10, 0),
            //            VerticalOptions = LayoutOptions.EndAndExpand,
            //            HorizontalOptions = LayoutOptions.EndAndExpand
            //        };
            //        label1.SetBinding(Label.TextProperty, "Id");
            //        label2.SetBinding(Label.TextProperty, "Title");
            //        label3.SetBinding(Label.TextProperty, "Amount");
            //        groupTitle.Children.Add(boxView, 0, 0);
            //        groupTitle.Children.Add(label1, 0, 0);
            //        groupTitle.Children.Add(label2, 0, 0);
            //        groupTitle.Children.Add(label3, 0, 0);
            //        return groupTitle;
            //    }),
            //    EmptyView = new StackLayout
            //    {
            //        Children ={
            //            new Label
            //            {
            //                Text="Sayım henüz başlamadı!",
            //                HorizontalOptions=LayoutOptions.Center,
            //                VerticalOptions=LayoutOptions.End,
            //                FontSize=20
            //            }
            //        }
            //    },
            //    GroupFooterTemplate = new DataTemplate(() =>
            //      {
            //          return new BoxView
            //          {
            //              BackgroundColor = Color.DarkSlateGray,
            //              HeightRequest = 1,
            //              HorizontalOptions = LayoutOptions.EndAndExpand,
            //              VerticalOptions = LayoutOptions.EndAndExpand
            //          };
            //      }),
            //    ItemTemplate = new DataTemplate(() =>
            //      {
            //          BoxView boxView = new BoxView
            //          {
            //              BackgroundColor=Color.DarkSlateGray,
            //              WidthRequest=3,
            //              HorizontalOptions = LayoutOptions.StartAndExpand,
            //              VerticalOptions = LayoutOptions.FillAndExpand
            //          };
            //          Label label = new Label
            //          {
            //              HorizontalOptions = LayoutOptions.StartAndExpand,
            //              VerticalOptions=LayoutOptions.Center,
            //              Text = "Stok adı : "
            //          };
            //          Label label1 = new Label
            //          {
            //              HorizontalOptions = LayoutOptions.Center,
            //              VerticalOptions = LayoutOptions.Center,
            //          };
            //          label1.SetBinding(Label.TextProperty, "StokAdi");
            //          StackLayout layout1 = new StackLayout
            //          {
            //              Orientation = StackOrientation.Horizontal
            //          };
            //          StackLayout layout2 = new StackLayout
            //          {
            //              Orientation = StackOrientation.Horizontal,
            //              Margin = 0,
            //              Padding = 0
            //          };
            //          Grid expanderHeader = new Grid
            //          {
            //              Margin = 0,
            //              Padding=0,
            //              ColumnDefinitions =
            //              {
            //                      new ColumnDefinition{Width=new GridLength(2,GridUnitType.Star) },
            //                      new ColumnDefinition{Width=new GridLength(3,GridUnitType.Star) },
            //                      new ColumnDefinition{Width=new GridLength(2,GridUnitType.Star) },
            //                      new ColumnDefinition{Width=new GridLength(3,GridUnitType.Star) }
            //              }
            //          };                   
            //          expanderHeader.Children.Add(boxView, 0, 0);
            //          expanderHeader.Children.Add(label, 0, 0);
            //          expanderHeader.Children.Add(label1, 1, 0,1,3);

            //          Grid expanderContent = new Grid
            //          {
            //              Margin=0,
            //              Padding=0,
            //              ColumnDefinitions =
            //              {
            //                      new ColumnDefinition{Width=new GridLength(2,GridUnitType.Star) },
            //                      new ColumnDefinition{Width=new GridLength(4,GridUnitType.Star) },
            //                      new ColumnDefinition{Width=new GridLength(3,GridUnitType.Star) },
            //                      new ColumnDefinition{Width=new GridLength(3,GridUnitType.Star) }
            //              }
            //          };
            //          Expander expander = new Expander
            //          {
            //              Margin = 0,
            //              Padding = 0,
            //              Header = expanderHeader,
            //              Content = expanderContent
            //          };
            //          BoxView boxView1 = new BoxView
            //          {
            //              BackgroundColor = Color.DarkSlateGray,
            //              WidthRequest = 1,
            //              VerticalOptions = LayoutOptions.FillAndExpand,
            //              HorizontalOptions = LayoutOptions.StartAndExpand,
            //          };
            //          layout1.Children.Add(boxView1);
            //          layout1.Children.Add(layout2);
            //          return layout1;
            //      }),
            //};
            //listData.SetBinding(CollectionView.ItemsSourceProperty, "CensusList");
            //Grid footer = new Grid
            //{
            //    HeightRequest=30,
            //    ColumnDefinitions =
            //    {
            //        new ColumnDefinition{ Width=new GridLength(4, GridUnitType.Star) },
            //        new ColumnDefinition{ Width=new GridLength(2, GridUnitType.Star) },
            //        new ColumnDefinition{ Width=new GridLength(4, GridUnitType.Star) },
            //        new ColumnDefinition{ Width=new GridLength(2, GridUnitType.Star) }
            //    }
            //};
            //Label lbl1 = new Label
            //{
            //    Text="Raf Sayısı : ",
            //    FontSize=18
            //};
            //Label lbl2 = new Label
            //{
            //    HorizontalOptions = LayoutOptions.Center,
            //    FontSize = 18
            //};
            //lbl2.SetBinding(Label.TextProperty, "ShelfCount", BindingMode.TwoWay);
            //Label lbl3 = new Label
            //{
            //    Text = "Etiket sayısı :",
            //    FontSize = 18
            //};
            //Label lbl4 = new Label
            //{
            //    HorizontalOptions = LayoutOptions.Center,
            //    FontSize = 18
            //};
            //lbl4.SetBinding(Label.TextProperty, "TagCount", BindingMode.TwoWay);
            //ImageButton image = new ImageButton
            //{
            //    BackgroundColor = Color.FromHex("1C658C")
            //};
            //image.SetBinding(ImageButton.CommandProperty, "DetailCensust", BindingMode.TwoWay);
            //footer.Children.Add(lbl1, 0, 0);
            //footer.Children.Add(lbl2, 1, 0);
            //footer.Children.Add(lbl3, 2, 0);
            //footer.Children.Add(lbl4, 3, 0);
            //footer.Children.Add(image, 4, 0);
            //layout.Children.Add(stack);
            //layout.Children.Add(listData);
            //layout.Children.Add(footer);
            //this.Content = layout;
            /*
                                    </xct:Expander.Header>
                                    <Grid ColumnDefinitions="2*,4*,3*,3*" Margin="0" Padding="0">
                                        <BoxView Grid.Column="0" BackgroundColor="DarkSlateGray" WidthRequest="3"  HorizontalOptions="StartAndExpand" VerticalOptions="FillAndExpand" />
                                        <Label   Grid.Column="0" HorizontalOptions="StartAndExpand"  VerticalOptions="Center" Text="Seri No : "/>
                                        <Label   Grid.Column="1" HorizontalOptions="StartAndExpand"  VerticalOptions="Center" Text="{Binding SeriNo}"/>
                                        <Label   Grid.Column="2" HorizontalOptions="StartAndExpand"  VerticalOptions="Center" Text="Stok kodu : "/>
                                        <Label   Grid.Column="3" HorizontalOptions="StartAndExpand"  VerticalOptions="Center" Text="{Binding StokKodu}"/>
                                        <!--<Label   Grid.Column="3" BindingContext="{Binding Source={x:Reference Title}, Path=BindingContext}" Text="{Binding Amount}"/>-->
                                    </Grid >
                                </xct:Expander>
                            </StackLayout>
                            <BoxView BackgroundColor="DarkSlateGray" WidthRequest="1"  HorizontalOptions="EndAndExpand" VerticalOptions="FillAndExpand" />
         
         */
            #endregion
        }
    }
}