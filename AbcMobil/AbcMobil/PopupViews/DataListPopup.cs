using AbcMobil.Models;
using Rg.Plugins.Popup.Animations;
using Rg.Plugins.Popup.Enums;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AbcMobil.PopupViews
{
    public class DataListPopup : PopupPage
    {
        private TaskCompletionSource<MobileResult> _taskCompletionSource;
        public Task<MobileResult> TaskCompletionSource => _taskCompletionSource.Task;
        MobileResult _terminalResult;
        bool Readed = false;
        public DataListPopup(object dataList, int selectedType = 0, string title = "")
        {
            _terminalResult = new MobileResult { Data = -1, ExceptionResult = false, Message = "", Result = false };
            Animation = new ScaleAnimation()
            {
                DurationIn = 400,
                DurationOut = 300,
                EasingIn = Easing.SinOut,
                EasingOut = Easing.SinIn,
                HasBackgroundAnimation = true,
                PositionIn = MoveAnimationOptions.Bottom,
                PositionOut = MoveAnimationOptions.Bottom,
            };
            Grid layout;

            if (dataList == null)
            {
                layout = new Grid
                {
                    VerticalOptions = LayoutOptions.EndAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    RowDefinitions =
                    {
                        new RowDefinition { Height = new GridLength(6, GridUnitType.Star) },
                        new RowDefinition { Height = new GridLength(6, GridUnitType.Star) },
                        new RowDefinition { Height = new GridLength(0.25, GridUnitType.Star) },
                        new RowDefinition { Height = new GridLength(2, GridUnitType.Star) }
                    }
                };

                CollectionView listLayout = new CollectionView
                {
                    ItemsSource = new List<string> { "Stok Adı", "Stok Kodu" },
                    ItemsLayout = new GridItemsLayout(orientation: ItemsLayoutOrientation.Vertical) { VerticalItemSpacing = 15 },
                    SelectionMode = SelectionMode.None,
                    ItemTemplate = new DataTemplate(() =>
                      {
                          Grid stack = new Grid { ColumnDefinitions = { new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) } } };
                          TapGestureRecognizer tapped = new TapGestureRecognizer()
                          {
                              Command = new Command(async (data) =>
                              {
                                  _terminalResult = new MobileResult { Data = data, Result = true, Message = "Başarılı", ExceptionResult = false };
                                  if (data.ToString() == "Stok Adı")
                                      _terminalResult.Data = 0;
                                  else
                                      _terminalResult.Data = 1;
                                  await PopupNavigation.Instance.PopAllAsync();
                                  _taskCompletionSource.SetResult(_terminalResult);
                              }),
                          };
                          tapped.SetBinding(TapGestureRecognizer.CommandParameterProperty, ".");
                          stack.GestureRecognizers.Add(tapped);
                          Label label = new Label { FontSize = 19, HorizontalOptions = LayoutOptions.Center, TextColor = Color.FromHex("1C658C") };
                          label.SetBinding(Label.TextProperty, ".");
                          stack.Children.Add(label, 0, 0);
                          return stack;
                      }),
                };
                StackLayout inLayout = new StackLayout
                {
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                };
                Label lbl1 = new Label
                {
                    Text = title,
                    FontSize = 20,
                };
                if (title != "")
                    inLayout.Children.Add(lbl1);
                Frame frm1 = new Frame
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    HeightRequest = 200,
                    CornerRadius = 20,
                    Margin = new Thickness(0, 0, 0, -23),
                    Content = inLayout
                };
                Frame frm2 = new Frame
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    HeightRequest = 200,
                    Content = listLayout
                };
                layout.Children.Add(frm1, 0, 2);
                layout.Children.Add(frm2, 0, 3);
            }
            else
            {
                IList<StockUI> data = new List<StockUI>();
                foreach (StockUI item in dataList as ShelfInfo)
                {
                    data.Add(item);
                }
                int sayi = 1 + data.Count;

                if (sayi > 5)
                    sayi = 5;
                layout = new Grid
                {
                    VerticalOptions = LayoutOptions.EndAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    RowDefinitions =
                    {
                        new RowDefinition { Height = new GridLength(6, GridUnitType.Star) },
                        new RowDefinition { Height = new GridLength(6, GridUnitType.Star) },
                        new RowDefinition { Height = new GridLength(2, GridUnitType.Star) },
                        new RowDefinition { Height = new GridLength(sayi, GridUnitType.Star) }
                    }
                };
                CollectionView listLayout = new CollectionView
                {
                    ItemsSource = data,
                    ItemsLayout = new GridItemsLayout(orientation: ItemsLayoutOrientation.Vertical) { VerticalItemSpacing = 15 },
                    SelectionMode = SelectionMode.None,
                    ItemTemplate = new DataTemplate(() =>
                    {
                        Grid stack = new Grid
                        {
                            ColumnDefinitions =
                            {
                                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)},
                                new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star)}
                            }
                        };
                        //TapGestureRecognizer tapped = new TapGestureRecognizer()
                        //{
                        //    Command = new Command(async () =>
                        //    {
                        //        _terminalResult = new MobileResult { Data = data, Result = true, Message = "Başarılı", ExceptionResult = false };
                        //        if (data.ToString() == "Stok Adı")
                        //            _terminalResult.Data = 0;
                        //        else
                        //            _terminalResult.Data = 1;
                        //        await PopupNavigation.Instance.PopAllAsync();
                        //        _taskCompletionSource.SetResult(_terminalResult);
                        //    }),
                        //};
                        //tapped.SetBinding(TapGestureRecognizer.CommandParameterProperty, ".");
                        //stack.GestureRecognizers.Add(tapped);
                        Label label1 = new Label { FontSize = 18, HorizontalOptions = LayoutOptions.Center, TextColor = Color.FromHex("1C658C") };
                        label1.SetBinding(Label.TextProperty, "SeriNo");
                        Label label2 = new Label { FontSize = 18, HorizontalOptions = LayoutOptions.Center, TextColor = Color.FromHex("1C658C") };
                        label2.SetBinding(Label.TextProperty, "StokKodu");
                        stack.Children.Add(label1, 0, 0);
                        stack.Children.Add(label2, 1, 0);
                        return stack;
                    }),
                };
                StackLayout inLayout = new StackLayout
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    Orientation = StackOrientation.Horizontal
                };
                Label lbl1 = new Label
                {
                    Text = title,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    FontSize = 20,
                };
                if (title != "")
                    inLayout.Children.Add(lbl1);
                ImageButton img = new ImageButton { BackgroundColor = Color.Transparent, HorizontalOptions = LayoutOptions.End, WidthRequest = 25, HeightRequest = 25 };
                img.Source = ImageSource.FromResource("AbcMobil.Assets.Icons.okeyIcon.png");
                img.Clicked += async (s, e) =>
                {
                    Readed = true;
                    await PopupNavigation.Instance.PopAsync();
                };
                inLayout.Children.Add(img);
                Frame frm1 = new Frame
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    HeightRequest = 200,
                    CornerRadius = 20,
                    Margin = new Thickness(0, 0, 0, -23),
                    Content = inLayout
                };
                Frame frm2 = new Frame
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    HeightRequest = 200,
                    Content = listLayout
                };
                layout.Children.Add(frm1, 0, 2);
                layout.Children.Add(frm2, 0, 3);
                int hiz = 0;
                Device.StartTimer(TimeSpan.FromMilliseconds(0), () =>
                {
                    //cts.Token
                    Device.BeginInvokeOnMainThread( () =>
                   {
                       if (!Readed)
                       {
                           TerminalResult tResult =  App.uhfService.SearchingSerialNumber(data.Select(s => new Stock { RafKodu = s.RafKodu, SeriNo = s.SeriNo.Substring(0, 8) + s.SeriNo.Substring(9, 4), StokAdi = s.StokAdi, StokKodu = s.StokKodu }).ToList(),RfidSettings.Instance.SearchReadPower,RfidSettings.Instance.SearchReadPower);
                           if (tResult.Result)
                           {
                               hiz = int.Parse(tResult.Message) + 100;
                               if (hiz < 50)
                               {
                                   hiz = 1;
                               }
                               else if (50 <= hiz && hiz < 75)
                               {
                                   hiz = 2;
                               }
                               else if (75 <= hiz && hiz < 100)
                               {
                                   hiz = 3;
                               }
                               else
                               {
                                   hiz = 4;
                               }
                           }
                           else
                               hiz = 0;
                       }
                   });
                    if (Readed)
                        return false;
                    else
                        return true;
                });
                Device.StartTimer(TimeSpan.FromMilliseconds(5000), () =>
                {
                    //cts.Token
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        if (!Readed)
                        {
                            if (hiz == 0)
                                App.audioService.playCensus(5);
                        }
                    });
                    if (Readed)
                        return false;
                    else
                        return true;
                });
                Device.StartTimer(TimeSpan.FromMilliseconds(500), () =>
                {
                    //cts.Token
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        if (!Readed)
                        {
                            if (hiz == 1)
                                for(int i = 0; i < hiz; i++)
                                App.audioService.playCensus(5);
                        }
                    });
                    if (Readed)
                        return false;
                    else
                        return true;
                });
                Device.StartTimer(TimeSpan.FromMilliseconds(200), () =>
                {
                    //cts.Token
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        if (!Readed)
                        {
                            if (hiz == 2)
                                for (int i = 0; i < hiz; i++)
                                    App.audioService.playCensus(5);
                        }
                    });
                    if (Readed)
                        return false;
                    else
                        return true;
                });
                Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
                {
                    //cts.Token
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        if (!Readed)
                        {
                            if (hiz == 3)
                                for (int i = 0; i < hiz; i++)
                                    App.audioService.playCensus(5);
                        }
                    });
                    if (Readed)
                        return false;
                    else
                        return true;
                });
                Device.StartTimer(TimeSpan.FromMilliseconds(20), () =>
                {
                    //cts.Token
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        if (!Readed)
                        {
                            if (hiz == 4)
                                for (int i = 0; i < hiz; i++)
                                    App.audioService.playCensus(5);
                        }
                    });
                    if (Readed)
                        return false;
                    else
                        return true;
                });
            }
            Content = layout;
        }
        protected override void OnDisappearing()
        {
            Readed = true;
            base.OnDisappearing();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (_taskCompletionSource == null)
                _taskCompletionSource = new TaskCompletionSource<MobileResult>();
        }
    }
}
