using AbcMobil.Models;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AbcMobil.PopupViews
{
    public class BarcodePopup : PopupPage
    {
        private bool Readed = false;
        private bool Waiting = false;
        public BarcodePopup(int type = 0)
        {
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
                Text = "Barkodunuzu okutunuz!",
                TextColor = Color.White,
                FontSize = 30,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };

            //Ismail
            var manuelEntry = new Entry
            {
                Placeholder = "Barkode Numarasını Giriniz!",
                IsReadOnly = false,
                TextColor = Color.White,
                FontSize = 20,
                PlaceholderColor = Color.White,
                BackgroundColor = Color.FromHex("1C658C"),
                IsVisible = false
            };

            //Ismail
            Switch sw = new Switch
            {
                OnColor = Color.White,
                ThumbColor = Color.DeepSkyBlue,
                IsToggled = false,
            };

            //Ismail
            Button manuelEntryBtn = new Button()
            {
                IsVisible = false,
                Text = "Ekle",
                BackgroundColor = Color.White,
                TextColor = Color.Black,
            };

            //Ismail
            layout.Children.Add(sw);
            layout.Children.Add(manuelEntry);
            //layout.Children.Add(manuelEntryBtn);

            layout.Children.Add(labelTop);

            sw.Toggled += (sender, e) =>
            {
                // Perform an action after examining e.Value
                if (e.Value)
                {
                    manuelEntry.IsVisible = true;
                    manuelEntryBtn.IsVisible = true;
                    labelTop.IsVisible = false;
                }
                else
                {
                    manuelEntry.IsVisible = false;
                    manuelEntryBtn.IsVisible = false;
                    labelTop.IsVisible = true;
                }
            };

            //ImageButton image = new ImageButton
            //{
            //    WidthRequest = 250,
            //    HeightRequest = 350,
            //    BorderColor = Color.DarkGray,
            //    IsEnabled = true,
            //};
            //image.Source = ImageSource.FromResource("AbcMobil.Assets.Icons.barcodeIcon.png");
            //layout.Children.Add(image);
            Entry entry = new Entry { Text = "", Margin = 100 };
            layout.Children.Add(entry);

            var timeSpan = TimeSpan.FromMilliseconds(500);
            Device.StartTimer(timeSpan, () =>
            {
                //cts.Token
                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (!Readed && !Waiting)
                    {
                        if (labelTop.IsVisible)
                        {
                            entry.Focus();
                        }
                        else
                        {
                            // Ismail: zaman sürekli 500 e set ediliyor.
                            // Böylelikle bekleme süresi ortadan kaldırılmış oluyor.
                            manuelEntry.Focus();
                            timeSpan = TimeSpan.FromMilliseconds(500);
                            if (manuelEntry.Text != null && manuelEntry.Text.Length == 13)
                                entry = manuelEntry;
                        }

                        if (entry.Text != "" )
                        {
                            if ((entry.Text.Trim().Length == 13 /*|| entry.Text.Length == 14*/ && entry.Text.Trim().StartsWith("2")) && type == 0)
                            {
                                Waiting = true;
                                RfidWritePopup popupPage = new RfidWritePopup(entry.Text.Substring(0, 8) + entry.Text.Substring(9), type);
                                entry.Text = "";
                                await PopupNavigation.Instance.PushAsync(popupPage);
                                TerminalResult result = await popupPage.CompSource;
                                if (result.Result)
                                {
                                    Waiting = false;
                                }
                                else
                                {
                                    Waiting = false;
                                }
                            }
                          
                            else if (type == 0)
                            {
                                await DisplayAlert("Alert", "Lütfen geçerli barkodu okutunuz!", "TAMAM");
                                
                                entry.Text = "";
                            }
                            if ((entry.Text.Length == 6 || entry.Text.Length == 7) && type == 2)
                            {
                                Waiting = true;
                                RfidWritePopup popupPage = new RfidWritePopup(entry.Text.Substring(0, 2) + entry.Text.Substring(3, 3), type);
                                entry.Text = "";
                                await PopupNavigation.Instance.PushAsync(popupPage);
                                TerminalResult result = await popupPage.CompSource;
                                if (result.Result)
                                {
                                    Waiting = false;
                                }
                                else
                                {
                                    Waiting = false;
                                }
                            }
                            else if (type == 2)
                            {
                                entry.Text = "";
                            }
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
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        protected override void OnDisappearing()
        {
            Readed = true;
            base.OnDisappearing();
        }
    }

}
