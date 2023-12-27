using AbcMobil.Models;
using Rg.Plugins.Popup.Animations;
using Rg.Plugins.Popup.Enums;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AbcMobil.PopupViews
{
    public class RfidPopup : PopupPage
    {
        private TaskCompletionSource<TerminalResult> taskCompletionSource;
        public Task<TerminalResult> TaskCompletionSource=>taskCompletionSource.Task;
        private bool Readed = false;
        private bool Waiting = false;
        TerminalResult tResult;
        public RfidPopup(int type)//0 : SerialNumber 1: Stok code 2: Shelf 3:Read Tag not popup
        {
            tResult = new TerminalResult { Result = false, Data = null, ExceptionResult = true, Message = "Belirlenmemiş bir işlemi seçtiniz!" };
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
            try
            {
                Device.StartTimer(TimeSpan.FromMilliseconds(500), () =>
                {
                    //cts.Token
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        if (!Readed && !Waiting)
                        {
                            if (type == 0 || type == 3)
                                tResult = App.uhfService.ReadSerialNumber(RfidSettings.Instance.TicketReadPower, RfidSettings.Instance.TicketWritePower);
                            else if (type == 1 || type == 4)
                                tResult = App.uhfService.ReadStockCode(RfidSettings.Instance.TicketReadPower, RfidSettings.Instance.TicketWritePower);
                            else if (type == 2 || type == 5)
                                tResult = App.uhfService.ReadTagg(RfidSettings.Instance.TagReadPower, RfidSettings.Instance.TagWritePower);
                            else if (type == 6)
                                tResult = await App.uhfService.InventorySerialNumber(300);
                            else if (type == 7)
                                tResult = await App.uhfService.InventoryStockCode(300);
                            else if (type == 8)
                                tResult = await App.uhfService.TagInventory(300);
                            else
                                tResult = new TerminalResult { Result = false, Data = null, ExceptionResult = true, Message = "Belirlenmemiş bir işlemi seçtiniz!" };
                            if (tResult.Result)
                            {
                                if (type < 3)
                                {
                                    Waiting = true;
                                    App.audioService.playCensus(1);
                                    //labelTop.Text = "Devam etmek için dokunun!";
                                    //labelTop.FontSize = 25;

                                    // Popup Görünmesi engellendi.
                                    RfidReadPopup popupPage = new RfidReadPopup(tResult.Data.ToString());
                                    await PopupNavigation.Instance.PushAsync(popupPage);
                                    Waiting = !(await popupPage.taskCompletionSource);
                                }
                                else
                                {
                                    App.audioService.playCensus(1);
                                    await PopupNavigation.Instance.PopAsync();

                                    //Readed = true;
                                    //Waiting = true;
                                }
                            }
                            //else if(!tResult.ExceptionResult)
                            //{
                            //    if (type < 3)
                            //    {
                            //        Waiting = true;
                            //        App.audioService.playCensus(1);
                            //        RfidReadPopup popupPage = new RfidReadPopup(tResult.Data.ToString());
                            //        await PopupNavigation.Instance.PushAsync(popupPage);
                            //        Waiting = !(await popupPage.taskCompletionSource);
                            //    }
                            //}
                        }
                    });
                    if (Readed)
                        return false;
                    else
                        return true;
                });
            }
            catch 
            {
            }
            this.Content = frm;
        }
        protected override void OnDisappearing()
        {
            Readed = true;
            base.OnDisappearing();
            taskCompletionSource.SetResult(tResult);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Readed = false;
            if (taskCompletionSource == null)
                taskCompletionSource = new TaskCompletionSource<TerminalResult>();
        }
    }
}
